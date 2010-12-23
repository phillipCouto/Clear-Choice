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
    public partial class LotExtrasBilledReport : UserControl,ISTabView
    {
        private Database db = Database.Instance;
        private DataSet itemRecords = null;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public LotExtrasBilledReport()
        {
            this.Name = "LotExtrasBilledReport";
            InitializeComponent();
            LoadGrid();
        }

        private void LoadGrid()
        {
            try
            {
                DataSet data = db.Select("*", "lot_extras_billed");
                Collection<LotExtrasBilledBinding> gridData = data.getBindableCollection<LotExtrasBilledBinding>();
                this.dgExtrabill.ItemsSource = gridData;

                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading info - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
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
                String title = "Lot Extras Billed Report";
                ArrayList currenyField = new ArrayList();
                ArrayList hiddenColumns = new ArrayList();
                hiddenColumns.Add(Lot.Fields.lotID.ToString());
                currenyField.Add("Amount");

                FlowDocument doc = itemRecords.GetFlowDocument(title, hiddenColumns, LotExtrasBilledBinding.GetDisplayTextMap(), currenyField);

                DocumentPreviewer preview = new DocumentPreviewer(doc, title);
                preview.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show("Nothing to print", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgExtrabill_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotExtrasBilledBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgExtrabill.Columns)
            {
                int index = dgExtrabill.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgExtrabill.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgExtrabill.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void dgExtrabill_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgExtrabill.SelectedItem != null)
            {
                LotExtrasBilledBinding lot = (LotExtrasBilledBinding)dgExtrabill.SelectedItem;
                try
                {
                    DataSet data = db.Select("*", Lot.Table, Lot.Fields.lotID.ToString() + " = '" + lot.lotID + "'");
                    if (data.NumberOfRows() == 1)
                    {
                        data.Read();
                        Lot obj = new Lot(data.GetRecordDataSet());
                        MainWindow.OpenTab(new LotView(obj),(Image)App.iconSet["home"],obj.LotDisplayName());
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
            return "Lot Extras Billed Report";
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["check-icon"];
        }

        #endregion
    }
}

