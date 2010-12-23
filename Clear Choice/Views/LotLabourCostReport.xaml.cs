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
    public partial class LotLabourCostReport : UserControl,ISTabView
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public LotLabourCostReport()
        {
            this.Name = "LotLabourCostReport";
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {

                DataSet data = db.Select("*","lot_labour_costs");
                Collection<LotLabourCostsBinding> gridData = data.getBindableCollection<LotLabourCostsBinding>();
                this.dgLabourHours.ItemsSource = gridData;

                itemRecords = data;
                LoadAverageTotalStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Lots - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgLots_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotLabourCostsBinding.GetDisplayTextMap();
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
                String title = "Lot Labour Cost Report";
                ArrayList currenyField = new ArrayList();
                currenyField.Add("LabourCost");

                FlowDocument doc = itemRecords.GetFlowDocument(title, null, LotLabourCostsBinding.GetDisplayTextMap(), currenyField);

                DocumentPreviewer preview = new DocumentPreviewer(doc, title);
                preview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to print", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadAverageTotalStats()
        {
            float totalHours = 0;
            float totalCost = 0;

            while (itemRecords.Read())
            {
                totalHours += Single.Parse(itemRecords.getObject("Hours").ToString());
                totalCost += Single.Parse(itemRecords.getObject("LabourCost").ToString());
            }
            itemRecords.ResetDataSet();
            txtTotalHours.Text = "" + totalHours;
            txtAvgHours.Text = ""+ Single.Parse(""+(totalHours/itemRecords.NumberOfRows())).ToString("#0.00");
            amtTotalCost.Amount = totalCost;
            Math.Round(totalCost);
            amtAvgCost.Amount = (float)Math.Round(totalCost / itemRecords.NumberOfRows(),2);
        }

        private void dgLabourHours_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgLabourHours.SelectedItem != null)
            {
                LotLabourCostsBinding obj = (LotLabourCostsBinding)dgLabourHours.SelectedItem;
                try
                {
                    DataSet data = db.Select("*", Lot.Table, Lot.Fields.lotID.ToString() + " = '" + obj.lotID + "'");
                    if (data.NumberOfRows() == 1)
                    {
                        data.Read();
                        Lot lot = new Lot(data.GetRecordDataSet());
                        MainWindow.OpenTab(new LotView(lot),(Image)App.iconSet["home"],lot.LotDisplayName());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading selected lot - "+msgCodes.GetString("M2102")+" "+ex.Message,"Error - 2102",MessageBoxButton.OK,MessageBoxImage.Error);
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
            return "Lot Labour Costs";
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["clock-icon"];
        }

        #endregion
    }
}

