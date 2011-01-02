﻿using System;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using ExceptionLogging;
using Stemstudios.UIControls;
using ClearChoice;
using System.Collections.ObjectModel;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using System.Collections;
using System.Windows.Media;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for LotSummaryView.xaml
    /// </summary>
    public partial class LotSummaryView : UserControl, ISTabView
    {
        private Lot mLot;
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private DataSet repairs;

        public LotSummaryView(Lot pLot)
        {
            InitializeComponent();
            mLot = pLot;
            LoadSummary();
            LoadRepairs();
        }

        private void LoadSummary()
        {
            try
            {
                DataSet data = db.Select("SUM(" + LotService.Fields.Amount.ToString() + ")", LotService.Table, LotService.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                if (data.NumberOfRows() > 0)
                {
                    data.Read();
                    amtServices.Amount = Single.Parse(data.getString(0));
                }

                data = db.Select("SUM(" + LotExtra.Fields.TotalPrice.ToString() + ")", LotExtra.Table, LotExtra.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                data.Read();
                if (data.getString(0).Equals(DBNull.Value))
                {
                    amtExtras.Amount = Single.Parse(data.getString(0));
                }

                data = db.Select("SUM(" + TimeSheet.Fields.Hours.ToString() + " * " + TimeSheet.Fields.Wage.ToString() + ")", TimeSheet.Table, TimeSheet.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                data.Read();
                if (data.getString(0).Equals(DBNull.Value))
                {
                    amtLabour.Amount = Single.Parse(data.getString(0));
                }

                data = db.Select("*", Site.Table, Site.Fields.siteID.ToString() + " = '" + mLot.GetAssociationID() + "'");
                if (data.NumberOfRows() > 0)
                {
                    data.Read();
                    Site assocSite = new Site(data.GetRecordDataSet());
                    data = db.Select("Average", "lot_material_average", "assocID = '" + assocSite.GetSiteID() + "'");
                }
                else
                {
                    data = db.Select("Average", "lot_material_average", "assocID = '" + mLot.GetLotID() + "'");

                }
                data.Read();
                if (data.getString(0).Equals(DBNull.Value))
                {
                    amtMaterials.Amount = Single.Parse(data.getString(0));
                }

                if ((amtServices.Amount + amtExtras.Amount) > (amtLabour.Amount + amtMaterials.Amount))
                {
                    amtProfit.Amount = (amtServices.Amount + amtExtras.Amount) - (amtLabour.Amount + amtMaterials.Amount);
                    amtProfit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1b9b0a"));
                }
                else
                {
                    amtProfit.Amount = (amtLabour.Amount + amtMaterials.Amount) - (amtServices.Amount + amtExtras.Amount);
                    amtProfit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#cc3333"));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Summary - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRepairs()
        {
            try
            {
                DataSet data = db.Select("*", "incomplete_repairs", LotRepair.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                data.BuildPrimaryKeyIndex(LotRepair.PrimaryKey);
                Collection<LotRepairBinding> gridData = data.getBindableCollection<LotRepairBinding>();
                this.dgRepairs.ItemsSource = gridData;

                repairs = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Summary - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dgRepairs_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgRepairs_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotRepairBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgRepairs.Columns)
            {
                int index = dgRepairs.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgRepairs.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgRepairs.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            return true;
        }

        public bool TabIsLosingFocus()
        {
            return true;
        }

        public void TabIsGainingFocus()
        {
            MainWindow.setActionList(new System.Collections.ArrayList());
        }

        public string TabTitle()
        {
            return mLot.LotDisplayName();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["reports-icon"];
        }

        #endregion
    }
}