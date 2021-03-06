﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Clear_Choice.Windows;
using ClearChoice;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using Stemstudios.UIControls;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for InventoryRecordReport.xaml
    /// </summary>
    public partial class LotSiteMaterialCostReport : UserControl,ISTabView
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private ComboBoxItem builderItem = new ComboBoxItem();

        public LotSiteMaterialCostReport()
        {
            this.Name = "LotSiteMaterialCostReport";
            InitializeComponent();
            builderItem = (ComboBoxItem)cmboType.SelectedItem;
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                DataSet data;
                if (cmboType.SelectedItem.Equals(builderItem))
                {
                    data = db.Select("*", "site_material_costs");
                    Collection<SiteMaterialCostsBinding> gridData = data.getBindableCollection<SiteMaterialCostsBinding>();
                    this.dgLabourHours.ItemsSource = gridData;
                }
                else
                {
                    data = db.Select("*","lot_material_costs");
                        Collection<LotMaterialCostsBinding> gridData = data.getBindableCollection<LotMaterialCostsBinding>();
                        this.dgLabourHours.ItemsSource = gridData;
                    
                }
                itemRecords = data;
                LoadAverageTotalStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Transactions - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }

        private void dgLots_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap;
            if (cmboType.SelectedItem.Equals(builderItem))
            {
                textMap = SiteMaterialCostsBinding.GetDisplayTextMap();
            }
            else
            {
                textMap = LotMaterialCostsBinding.GetDisplayTextMap();
            }
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

        private ArrayList Print()
        {
            ArrayList actions = new ArrayList();
            IconButton savenewRepairBtn = new IconButton();
            savenewRepairBtn.Text = "Print";
            savenewRepairBtn.Source = (Image)App.iconSet["printer-icon"];
            savenewRepairBtn.MouseDown += new MouseButtonEventHandler(button1_Click);
            actions.Add(savenewRepairBtn);

            return actions;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String title;
                ArrayList hideFields = new ArrayList();
                ArrayList currenyField = new ArrayList();
                currenyField.Add("TotalValue");
                hideFields.Add(Lot.Fields.lotID.ToString());
                hideFields.Add(Site.Fields.siteID.ToString());
                FlowDocument doc;
                if(cmboType.SelectedItem.Equals(builderItem))
                {
                    title = "Site Material Costs";
                    doc = itemRecords.GetFlowDocument(title, hideFields, SiteMaterialCostsBinding.GetDisplayTextMap(), currenyField);
                }
                else
                {
                    title = "Lot Material Costs";
                    doc = itemRecords.GetFlowDocument(title, hideFields, LotMaterialCostsBinding.GetDisplayTextMap(), currenyField);
                }

                DocumentPreviewer preview = new DocumentPreviewer(doc, title);
                preview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to print", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                LoadGrid();
            }
        }

        private void LoadAverageTotalStats()
        {
            float totalQty = 0;
            float totalCost = 0;

            while (itemRecords.Read())
            {
                totalQty += Single.Parse(itemRecords.getObject("Quantity").ToString());
                totalCost += Single.Parse(itemRecords.getObject("TotalValue").ToString());
            }
            itemRecords.ResetDataSet();
            txtTotalQty.Text = "" + totalQty;
            txtAvgQty.Text = "" + Single.Parse("" + (totalQty / itemRecords.NumberOfRows())).ToString("#0.00");
            amtTotalCost.Amount = totalCost;
            Math.Round(totalCost);
            amtAvgCost.Amount = (float)Math.Round(totalCost / itemRecords.NumberOfRows(), 2);
        }

        private void dgLabourHours_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgLabourHours.SelectedItem != null)
            {
                try
                {
                    if (cmboType.SelectedItem.Equals(builderItem))
                    {
                        SiteMaterialCostsBinding obj = (SiteMaterialCostsBinding)dgLabourHours.SelectedItem;
                        DataSet data = db.Select("*", Site.Table, Site.Fields.siteID.ToString() + " = '" + obj.siteID + "'");
                        if (data.NumberOfRows() == 1)
                        {
                            data.Read();
                            Site site = new Site(data.GetRecordDataSet());

                            data = db.Select("*", Client.Table, Client.Fields.clientID.ToString() + " = '" + site.GetClientID() + "'");
                            data.Read();
                            Client client = new Client(data.GetRecordDataSet());

                            MainWindow.OpenTab(new SiteView(site, client), (Image)App.iconSet["symbol-site"], site.GetSiteName());
                        }
                    }
                    else
                    {
                        LotMaterialCostsBinding obj = (LotMaterialCostsBinding)dgLabourHours.SelectedItem;
                        DataSet data = db.Select("*", Lot.Table, Lot.Fields.lotID.ToString() + " = '" + obj.lotID + "'");
                        if (data.NumberOfRows() == 1)
                        {
                            data.Read();
                            Lot lot = new Lot(data.GetRecordDataSet());
                            MainWindow.OpenTab(new LotView(lot), (Image)App.iconSet["home"], lot.LotDisplayName());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading selected lot/site - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
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
            MainWindow.setActionList(Print());
        }

        public string TabTitle()
        {
            return "Lot/Site Material Costs";
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["symbol-transactions"];
        }

        #endregion
    }
}

