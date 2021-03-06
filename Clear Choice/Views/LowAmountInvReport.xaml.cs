﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using ClearChoice;
using Stemstudios.UIControls;
using System.Windows.Documents;
using Clear_Choice.Windows;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for LowAmountInvReport.xaml
    /// </summary>
    public partial class LowAmountInvReport : UserControl, ISTabView
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public LowAmountInvReport()
        {
            InitializeComponent();
            LoadItems();
            this.Name = "LowStockReport";
        }

        private void LoadItems()
        {
            try
            {
                DataSet data = db.Select("*", InventoryItem.Table, InventoryItem.Fields.Quantity.ToString()+" < 5");

                data.BuildPrimaryKeyIndex(InventoryItem.PrimaryKey);
                Collection<InventoryItemBinding> gridData = data.getBindableCollection<InventoryItemBinding>();
                DispatcherOperation dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<InventoryItemBinding>>(setDataGridViewData), gridData);

                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Inventory Items - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void setDataGridViewData(Collection<InventoryItemBinding> data)
        {
            this.dgInventory.ItemsSource = data;
        }

        private void txtOkay_Click(object sender, RoutedEventArgs e)
        {
try
            {
                DataSet data = db.Select("*", InventoryItem.Table, InventoryItem.Fields.Quantity.ToString()+" < " + txtQuantity.Text + " ");

                data.BuildPrimaryKeyIndex(InventoryItem.PrimaryKey);
                Collection<InventoryItemBinding> gridData = data.getBindableCollection<InventoryItemBinding>();
                DispatcherOperation dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<InventoryItemBinding>>(setDataGridViewData), gridData);

                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Inventory Items - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgInventory_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = InventoryItemBinding.getDisplayTextMap();
            foreach (DataGridColumn column in dgInventory.Columns)
            {
                int index = dgInventory.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (textMap.ContainsKey(headerText))
                {
                    dgInventory.Columns[index].Header = textMap[headerText];
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
                String title = "Low Amount Report";
                ArrayList hideFields = new ArrayList();
                ArrayList currenyField = new ArrayList();
                hideFields.Add("ItemDescription");

                //works but wrong binding:S
                FlowDocument doc = itemRecords.GetFlowDocument(title, hideFields, InventoryItemBinding.getDisplayTextMap(), currenyField);

                DocumentPreviewer preview = new DocumentPreviewer(doc, title);
                preview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to print", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            return "Low Stock Report";
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["symbol-emptycart"];
        }

        #endregion
    }
}
