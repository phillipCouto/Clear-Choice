﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using ClearChoice;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for InventoryRecordReport.xaml
    /// </summary>
    public partial class CityReport : UserControl
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public CityReport()
        {
            this.Name = "CityReportView";
            InitializeComponent();
            LoadCities();
        }

        private void LoadCities()
        {
            try
            {
                cmboCities.Items.Clear();
                DataSet data = db.Select("DISTINCT(" + Lot.Fields.City.ToString() + ")", Lot.Table);
                while (data.Read())
                {
                    String city = data.getString(Lot.Fields.City.ToString());
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = city;
                    cmboCities.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Citys - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgLots_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (dgLots.SelectedIndex > -1)
                {
                    LotBinding obj = (LotBinding)dgLots.SelectedCells[0].Item;
                    itemRecords.SeekToPrimaryKey(obj.lotID);
                    Lot mLot = new Lot(itemRecords.GetRecordDataSet());
                    MainWindow.OpenTab(new LotView(mLot), (Image)App.iconSet["home"], mLot.LotDisplayName());
                }
            }
        }

        private void dgLots_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotBinding.getdisplayTextMap();
            foreach (DataGridColumn column in dgLots.Columns)
            {
                int index = dgLots.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgLots.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgLots.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void cmboCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmboCities.SelectedIndex > -1)
            {
                try
                {

                    String city = ((ComboBoxItem)cmboCities.Items[cmboCities.SelectedIndex]).Content.ToString();
                    DataSet data = db.Select("*", Lot.Table, Lot.Fields.City.ToString()+" = '" + city + "'");

                    data.BuildPrimaryKeyIndex(Lot.PrimaryKey);
                    Collection<LotBinding> gridData = data.getBindableCollection<LotBinding>();
                    dgLots.ItemsSource = gridData;
                    itemRecords = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loading Lots - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}
