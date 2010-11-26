﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Clear_Choice.Windows;
using ClearChoice;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for InventoryRecordReport.xaml
    /// </summary>
    public partial class TotalBilling : UserControl
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public TotalBilling()
        {
            this.Name = "TotalBilling";
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                DataSet data = db.Select("lots.lotID, lots.LotNumber,lots.Address, lots.City, SUM(lot_services.Amount) as Amount", Lot.Table + "," + LotService.Table, "lots.lotID = lot_services.lotID AND lot_services.Billed Is Not Null Group By lots.lotID", "lots.City, lots.Address,lots.LotNumber");
                Collection<TotalBillingBinding> gridData = data.getBindableCollection<TotalBillingBinding>();
                this.dgHours.ItemsSource = gridData;

                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading info - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgLots_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = TotalBillingBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgHours.Columns)
            {
                int index = dgHours.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgHours.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgHours.Columns[index].Header = textMap[headerText];
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
            String title = "Total Lot Billing";
            ArrayList hideFields = new ArrayList();
            ArrayList currenyField = new ArrayList();
            hideFields.Add("lotID");
            currenyField.Add("Amount");

            FlowDocument doc = itemRecords.GetFlowDocument(title, hideFields, TotalBillingBinding.GetDisplayTextMap(), currenyField);

            DocumentPreviewer preview = new DocumentPreviewer(doc, title);
            preview.ShowDialog();


        }
    }
}

