﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ExceptionLogging;
using Microsoft.Windows.Controls;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using ClearChoice;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for InventoryRecordReport.xaml
    /// </summary>
    public partial class AvgLabourHoursPerHouse : UserControl
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public AvgLabourHoursPerHouse()
        {
            this.Name = "AvgHoursPerHouse";
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                DataSet data = db.Select("lotID, SUM(AVG(Hours))", TimeSheet.Table);
                data.BuildPrimaryKeyIndex(TimeSheet.PrimaryKey);
                Collection<Time_SheetBinding> gridData = data.getBindableCollection<Time_SheetBinding>();
                this.dgLabourHours.ItemsSource = gridData;

                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Lots - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void dgLots_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (e.ClickCount == 2)
        //    {
        //        if (dgLots.SelectedIndex > -1)
        //        {
        //            LotBinding obj = (LotBinding)dgLots.SelectedCells[0].Item;
        //            itemRecords.SeekToPrimaryKey(obj.lotID);
        //            Lot mLot = new Lot(itemRecords.GetRecordDataSet());
        //            MainWindow.OpenTab(new LotView(mLot), (Image)App.iconSet["home"], mLot.LotDisplayName());
        //        }
        //    }
        //}

        private void dgLots_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotBinding.getdisplayTextMap();
            foreach (DataGridColumn column in dgLabourHours.Columns)
            {
                int index = dgLabourHours.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgLabourHours.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgLabourHours.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                MainWindow.setActionList(new ArrayList());
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(dgLabourHours, "Grid Printing.");
        }
    }
}

