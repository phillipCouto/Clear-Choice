using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using ClearChoice;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using Stemstudios.UIControls;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for WorkForm.xaml
    /// </summary>
    public partial class LotExtraView : UserControl
    {
        private bool isFormHidden = false;
        private bool isNewExtra = false;
        private String saveBtnTxt = "Save Changes";
        private String unlockBtnTxt = "Unlock Form";
        private bool isModified = true;
        private LotExtra mExtra = null;
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private Lot mLot;
        private DataSet extrasDataSet;
        private DataSet extraItemsDataSet;

        public LotExtraView(Lot lot)
        {
            mLot = lot;
            mExtra = new LotExtra(db);
            this.Name = "LotExtraView" + lot.GetLotID();
            InitializeComponent();
            displayOrHideForm();
            lockFields();
            clearFields();
            loadExtras();
        }

        private void lockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtBilledQuantity);
            boxes.Add(txtInvoice);
            boxes.Add(txtNotes);
            boxes.Add(txtLocation);
            boxes.Add(txtPO);
            boxes.Add(txtQuantity);

            txtUnitPrice.IsReadOnly = true;
            txtUnitPrice.Foreground = foreGround;
            txtUnitPrice.Background = backGround;

            txtTotalPrice.Foreground = foreGround;
            txtTotalPrice.Background = backGround;

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = true;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            dpBilledDate.IsReadOnly = true;
            cmboExtra.IsReadOnly = true;
            cmboExtra.Foreground = foreGround;
            cmboExtra.Background = backGround;

            cmdCancel.IsEnabled = false;
            cmdSaveEdit.IsEnabled = true;
            cmdSaveEdit.Content = unlockBtnTxt;
            isModified = false;

        }

        private void unlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtBilledQuantity);
            boxes.Add(txtInvoice);
            boxes.Add(txtNotes);
            boxes.Add(txtLocation);
            boxes.Add(txtPO);
            boxes.Add(txtQuantity);

            txtUnitPrice.IsReadOnly = false;
            txtUnitPrice.Foreground = foreGround;
            txtUnitPrice.Background = backGround;

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = false;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            dpBilledDate.IsReadOnly = false;
            cmboExtra.IsReadOnly = false;
            cmboExtra.Foreground = foreGround;
            cmboExtra.Background = backGround;

            cmdCancel.IsEnabled = true;
            cmdSaveEdit.IsEnabled = false;
            cmdSaveEdit.Content = saveBtnTxt;
            isModified = false;
        }
        /// <summary>
        /// Starts the thread to load the extras.
        /// </summary>
        private void loadExtras()
        {
            Thread extraThread = new Thread(threadLoadExtrasData);
            extraThread.SetApartmentState(ApartmentState.STA);
            extraThread.Start();
        }
        /// <summary>
        /// The thread entrypoint for loading the inventory data from the database.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void threadLoadExtrasData()
        {
            try
            {
                DataSet data = db.Select("*", LotExtra.Table, LotExtra.Fields.lotID + " = '" + mLot.GetLotID() + "'");

                extraItemsDataSet = db.Select("DISTINCT " + LotExtra.Fields.ExtraItem.ToString(), LotExtra.Table);

                Collection<LotExtraBinding> gridData = data.getBindableCollection<LotExtraBinding>();
                DispatcherOperation dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<LotExtraBinding>>(setDataGridViewData), gridData);
                DispatcherOperationStatus status = dataOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = dataOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
                extrasDataSet = data;
                extrasDataSet.BuildPrimaryKeyIndex(LotExtra.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Extra Items - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// This is the call back method so the thread can set the items source for the grid view.
        /// </summary>
        /// <param name="data"></param>
        private void setDataGridViewData(Collection<LotExtraBinding> data)
        {
            this.ExtraGridView.ItemsSource = data;

            cmboExtra.Items.Clear();
            while (extraItemsDataSet.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = extraItemsDataSet.getString(LotExtra.Fields.ExtraItem.ToString());
                cmboExtra.Items.Add(item);
            }
            if (mExtra != null & !isNewExtra)
            {
                cmboExtra.Text = mExtra.GetExtraItem();
            }
            extraItemsDataSet = null;
        }

        private void populateFields()
        {
            txtBilledQuantity.Text = ""+mExtra.GetBilledQuantity();
            cmboExtra.Text = mExtra.GetExtraItem();
            txtInvoice.Text = mExtra.GetInvoice();
            txtLocation.Text = mExtra.GetLocation();
            txtNotes.Text = mExtra.GetNotes();
            txtPO.Text = mExtra.GetPO();
            txtUnitPrice.Amount = mExtra.GetUnitPrice();
            txtQuantity.Text = ""+mExtra.GetQuantity();
            txtTotalPrice.Amount = mExtra.GetTotalPrice();

            dpBilledDate.Text = ((mExtra.GetBilledDate().Equals(DateTime.MinValue)) ? "" : mExtra.GetBilledDate().ToShortDateString());
        }

        private void clearFields()
        {
            txtBilledQuantity.Text = "";
            cmboExtra.Text = "";
            txtInvoice.Text = "";
            txtLocation.Text = "";
            txtNotes.Text = "";
            txtPO.Text = "";
            txtUnitPrice.Amount = 0;
            txtTotalPrice.Amount = 0;
            txtQuantity.Text = "";

            dpBilledDate.Text = "";

            mExtra = null;
        }

        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                if (isNewExtra)
                {
                    MessageBoxResult answer = MessageBox.Show("Saving New Extra - " + msgCodes.GetString("M3201"), " Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (answer == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBoxResult answer = MessageBox.Show("Saving Changes to Extra - " + msgCodes.GetString("M3202"), " Warning - 3202", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (answer == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                if (SaveChanges())
                {
                    if (isExtraUnique())
                    {
                        try
                        {
                            db.BeginTransaction();
                            mExtra.SaveObject(db);
                            db.CommitTransaction();
                            lockFields();
                            loadExtras();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Saving Extra - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                            db.RollbackTransaction();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Extra may alread exist - " + msgCodes.GetString("M3203") + " Description and Location(if set)", "Warning 3203", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else if (mExtra != null)
            {
                unlockFields();
            }

        }

        private bool isExtraUnique()
        {
            StringBuilder where = new StringBuilder();
            where.Append(LotExtra.Fields.lotID.ToString() + " = '" + mExtra.GetLotID() + "'");
            where.Append(" AND " + LotExtra.Fields.ExtraItem.ToString() + " = '" + cmboExtra.Text.ToUpper() + "'");
            if (txtLocation.Text.Length > 0)
            {
                where.Append(" AND " + LotExtra.Fields.Location.ToString() + " = '" + txtLocation.Text.ToUpper() + "'");
            }
            else
            {
                where.Append(" AND " + LotExtra.Fields.Location.ToString() + " IS NULL");
            }
            if (!isNewExtra)
            {
                where.Append(" AND " + LotExtra.PrimaryKey + " != '" + mExtra.GetExtraID() + "'");
            }
            try
            {
                DataSet results = db.Select(LotExtra.PrimaryKey, LotExtra.Table, where.ToString());
                if (results.NumberOfRows() > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checking Extra uniqueness - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool SaveChanges()
        {
            int quantity = 0;
            if (cmboExtra.Text.Length < 1)
            {
                MessageBox.Show("Field 'Description' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                mExtra.SetExtraItem(cmboExtra.Text.ToUpper());
            }
            if (txtUnitPrice.Amount > 0)
            {
                mExtra.SetUnitPrice(txtUnitPrice.Amount);
            }
            else
            {
                MessageBox.Show("Field 'Unit Price' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtQuantity.Text.Length < 1)
            {
                MessageBox.Show("Field 'Quantity' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                try
                {
                    quantity = Int32.Parse(txtQuantity.Text);
                    mExtra.SetQuantity(quantity);
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Quantity' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtBilledQuantity.Text.Length > 0)
            {
                try
                {
                    mExtra.SetBilledQuantity(Int32.Parse(txtBilledQuantity.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Billed Quantity' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.BilledQuantity.ToString());
            }
            if (txtInvoice.Text.Length > 0)
            {
                int code = mExtra.SetInvoice(txtInvoice.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Invoice' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.Invoice.ToString());
            }
            if (txtLocation.Text.Length > 0)
            {
                mExtra.SetLocation(txtLocation.Text.ToUpper());
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.Location.ToString());
            }
            if (txtNotes.Text.Length > 0)
            {
                mExtra.SetNotes(txtNotes.Text);
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.Notes.ToString());
            }
            if (txtPO.Text.Length > 0)
            {
                mExtra.SetPO(txtPO.Text.ToString());
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.PO.ToString());
            }
            if (dpBilledDate.Text.Length > 0)
            {
                mExtra.SetBilledDate(dpBilledDate.SelectedDate);
            }
            else if (!isNewExtra)
            {
                mExtra.ClearField(LotExtra.Fields.BilledDate.ToString());
            }
            mExtra.SetTotalPrice(txtUnitPrice.Amount * quantity);
            return true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isNewExtra)
            {
                if (isModified)
                {
                    MessageBoxResult answer = MessageBox.Show("Cancel new Extra - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (answer == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                lockFields();
                clearFields();
                displayOrHideForm();
            }
            else
            {
                if (isModified)
                {
                    MessageBoxResult answer = MessageBox.Show("Cancel Extra modifications - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (answer == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                lockFields();
                populateFields();
            }
        }

        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                if (cmdSaveEdit.Content.Equals(cmdSaveEdit))
                {
                    if (isNewExtra)
                    {
                        MainWindow.setActionList(getNewItemActions());
                    }
                    else
                    {
                        MainWindow.setActionList(getExistingItemActions());
                    }
                }
                else
                {
                    MainWindow.setActionList(getExtraButtons());
                }
                loadExtras();
            }
            else
            {
                extrasDataSet = null;
                ExtraGridView.ItemsSource = null;
            }
        }

        private ArrayList getExistingItemActions()
        {
            ArrayList actions = new ArrayList();
            if (isModified)
            {
                IconButton saveNewSiteBtn = new IconButton();
                saveNewSiteBtn.Text = "Save Changes";
                saveNewSiteBtn.Source = (Image)App.iconSet["symbol-save"];
                saveNewSiteBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(saveNewSiteBtn);
            }
            IconButton cancelNewClientBtn = new IconButton();
            cancelNewClientBtn.Text = "Cancel Changes";
            cancelNewClientBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewClientBtn);
            return actions;
        }

        private ArrayList getNewItemActions()
        {
            ArrayList actions = new ArrayList();

            if (isModified)
            {
                IconButton saveNewSiteBtn = new IconButton();
                saveNewSiteBtn.Text = "Save New Item";
                saveNewSiteBtn.Source = (Image)App.iconSet["symbol-save"];
                saveNewSiteBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(saveNewSiteBtn);
            }
            IconButton cancelNewClientBtn = new IconButton();
            cancelNewClientBtn.Text = "Cancel New Item";
            cancelNewClientBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewClientBtn);
            return actions;
        }

        private ArrayList getExtraButtons()
        {
            ArrayList actions = new ArrayList();

            IconButton addItemBtn = new IconButton();
            addItemBtn.Text = "Add Item";
            addItemBtn.Source = (Image)App.iconSet["symbol-add"];
            addItemBtn.MouseDown += new MouseButtonEventHandler(addItemBtn_MouseDown);
            actions.Add(addItemBtn);
            return actions;
        }

        private void addItemBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                clearFields();
                mExtra = new LotExtra(db);
                mExtra.SetLotID(mLot.GetLotID());
                if (isFormHidden)
                {
                    displayOrHideForm();
                }
                unlockFields();
                isNewExtra = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Extra Schema - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TabShowHideButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            displayOrHideForm();
        }

        private void displayOrHideForm()
        {
            if (!isFormHidden)
            {
                BeginStoryboard((Storyboard)FindResource("ExtraFormShrink"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-show"]).Source;
                isFormHidden = true;
            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("ExtraFormExpand"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-hide"]).Source;
                isFormHidden = false;
            }
        }

        private void fieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isModified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
            }
            if (sender.Equals(txtUnitPrice) || sender.Equals(txtQuantity))
            {
                if (txtQuantity.Text.Length > 0)
                {
                    try
                    {
                        int quantity = Int32.Parse(txtQuantity.Text);
                        txtTotalPrice.Amount = txtUnitPrice.Amount * quantity;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void dpBilledDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isModified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void ExtraGridView_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotExtraBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in ExtraGridView.Columns)
            {
                int index = ExtraGridView.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    ExtraGridView.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        ExtraGridView.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void ExtraGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExtraGridView.SelectedIndex != -1)
            {
                LotExtraBinding obj = (LotExtraBinding)ExtraGridView.SelectedCells[0].Item;
                extrasDataSet.SeekToPrimaryKey(obj.extraID);
                mExtra = new LotExtra(extrasDataSet.GetRecordDataSet());
                populateFields();
                lockFields();
                cmdSaveEdit.IsEnabled = true;
                isNewExtra = false;
                if (isFormHidden)
                {
                    displayOrHideForm();
                }
            }
        }
    }
}
