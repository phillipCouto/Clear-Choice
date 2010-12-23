using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
    /// Interaction logic for InventoryTransactionView.xaml
    /// </summary>
    public partial class InventoryTransactionView : UserControl, ISTabView
    {
        private bool isNewTransaction = true;
        private bool isTransactionModified = false;
        private bool isItemModified = true;
        private bool isItemNew = false;
        private bool isFormHidden = false;
        private bool isRestock = false;
        private bool isTransactionLocked = false;
        private Hashtable mTransactionItems = new Hashtable();


        private String saveBtnTxt = "Save Changes";
        private String addBtnTxt = "Add Item";
        private String unlockBtnTxt = "Unlock Form";
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        private InventoryTransaction mTransaction = null;
        private InventoryTransactionItem mTransactionItem = null;


        public InventoryTransactionView(Site site)
        {
            InitializeComponent();
            this.Name = "TransactionViewNewTransaction";
            isNewTransaction = true;
            txtReciever.Text = site.GetSiteName();
            displayOrHideForm();
            lockItemFields();
            ClearFields();
            db.BeginTransaction();
            mTransaction = new InventoryTransaction(site.GetSiteID());
            mTransaction.SetClientType(InventoryTransaction.ClientType.Builder);
        }
        public InventoryTransactionView(Lot lot)
        {
            InitializeComponent();
            this.Name = "TransactionViewNewTransaction";
            isNewTransaction = true;
            txtReciever.Text = lot.LotDisplayName();
            displayOrHideForm();
            lockItemFields();
            ClearFields();
            db.BeginTransaction();
            mTransaction = new InventoryTransaction(lot.GetLotID());
            mTransaction.SetClientType(InventoryTransaction.ClientType.Client);
        }
        public InventoryTransactionView()
        {
            InitializeComponent();
            this.Name = "TransactionViewNewTransaction";
            isNewTransaction = true;
            displayOrHideForm();
            lockItemFields();
            ClearFields();
            db.BeginTransaction();
            isRestock = true;
            txtReciever.Text = "Inventory";
            mTransaction = new InventoryTransaction("0");
            mTransaction.SetClientType(InventoryTransaction.ClientType.Inventory);
        }

        public InventoryTransactionView(InventoryTransaction transaction)
        {
            InitializeComponent();
            this.Name = "TransactionViewNewTransaction";
            isNewTransaction = false;
            displayOrHideForm();
            lockItemFields();
            mTransaction = transaction;
            LoadTransaction();

        }
        private void LoadTransaction()
        {
            ClearFields();
            try
            {
                if (mTransaction.GetClientType().Equals(InventoryTransaction.ClientType.Inventory))
                {
                    isRestock = true;
                    txtReciever.Text = "Inventory";
                }
                else
                {
                    DataSet data = db.Select("*", Site.Table, Site.Fields.siteID.ToString() + " = '" + mTransaction.GetAssocID() + "'");
                    if (data.NumberOfRows() > 0)
                    {
                        data.Read();
                        Site site = new Site(data.GetRecordDataSet());
                        txtReciever.Text = site.GetSiteName();
                    }
                    else
                    {
                        data = db.Select("*", Lot.Table, Lot.Fields.lotID.ToString() + " = '" + mTransaction.GetAssocID() + "'");
                        if (data.NumberOfRows() > 0)
                        {
                            data.Read();
                            Lot lot = new Lot(data.GetRecordDataSet());
                            txtReciever.Text = lot.LotDisplayName();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Transaction Failed - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            isTransactionLocked = true;
            dpTransactionDate.Text = mTransaction.GetDateOfTransaction().ToShortDateString();
            dpTransactionDate.IsReadOnly = true;
            LoadDateGrid();
        }
        /// <summary>
        /// This handles the hiding and showing of the form
        /// </summary>
        private void displayOrHideForm()
        {
            if (!isFormHidden)
            {
                BeginStoryboard((Storyboard)FindResource("ItemFormShrink"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-show"]).Source;
                isFormHidden = true;
            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("ItemFormExpand"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-hide"]).Source;
                isFormHidden = false;
            }
        }

        private void fieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isItemModified && txtLatestCost.Amount > 0 && txtQuantity.Text.Length > 0)
            {
                isItemModified = true;
                cmdSaveEditItem.IsEnabled = true;
            }
        }

        private ArrayList getnewTransactionActions()
        {
            ArrayList actions = new ArrayList();

            if (isTransactionModified)
            {
                IconButton saveNewTransaction = new IconButton();
                saveNewTransaction.Text = "Save New Transaction";
                saveNewTransaction.Source = (Image)App.iconSet["symbol-save"];
                saveNewTransaction.MouseDown += new MouseButtonEventHandler(saveNewTransaction_MouseDown);
                actions.Add(saveNewTransaction);
            }
            IconButton addNewItem = new IconButton();
            addNewItem.Text = "Add Item to Transaction";
            addNewItem.Source = (Image)App.iconSet["symbol-add"];
            addNewItem.MouseDown += new MouseButtonEventHandler(addNewItem_MouseDown);
            actions.Add(addNewItem);

            IconButton cancelNewTransaction = new IconButton();
            cancelNewTransaction.Text = "Cancel New Transaction";
            cancelNewTransaction.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewTransaction.MouseDown += new MouseButtonEventHandler(cancelNewTransaction_MouseDown);
            actions.Add(cancelNewTransaction);

            return actions;
        }

        private ArrayList getExistingTransactionActions()
        {
            ArrayList actions = new ArrayList();
            if (!isTransactionLocked)
            {
                if (isTransactionModified)
                {
                    IconButton saveTransaction = new IconButton();
                    saveTransaction.Text = "Save Transaction Changes";
                    saveTransaction.Source = (Image)App.iconSet["symbol-save"];
                    saveTransaction.MouseDown += new MouseButtonEventHandler(saveNewTransaction_MouseDown);
                    actions.Add(saveTransaction);
                }
                IconButton addNewItem = new IconButton();
                addNewItem.Text = "Add Item to Transaction";
                addNewItem.Source = (Image)App.iconSet["symbol-add"];
                addNewItem.MouseDown += new MouseButtonEventHandler(addNewItem_MouseDown);
                actions.Add(addNewItem);
                if (isTransactionModified)
                {
                    IconButton cancelTransaction = new IconButton();
                    cancelTransaction.Text = "Cancel Transaction Changes";
                    cancelTransaction.Source = (Image)App.iconSet["symbol-delete"];
                    cancelTransaction.MouseDown += new MouseButtonEventHandler(cancelNewTransaction_MouseDown);
                    actions.Add(cancelTransaction);
                }
                else
                {
                    IconButton lockTransaction = new IconButton();
                    lockTransaction.Text = "Lock Transaction";
                    lockTransaction.Source = (Image)App.iconSet["symbol-lock"];
                    lockTransaction.MouseDown += new MouseButtonEventHandler(cancelNewTransaction_MouseDown);
                    actions.Add(lockTransaction);
                }
            }
            else
            {
                IconButton unlockTransaction = new IconButton();
                unlockTransaction.Text = "Unlock Transaction";
                unlockTransaction.Source = (Image)App.iconSet["symbol-unlock"];
                unlockTransaction.MouseDown += new MouseButtonEventHandler(unlockTransaction_MouseDown);
                actions.Add(unlockTransaction);
            }

            return actions;
        }

        private void unlockTransaction_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isTransactionLocked = false;
            isTransactionModified = false;
            try
            {
                db.BeginTransaction();
                dpTransactionDate.IsReadOnly = false;
            }
            catch (Exception ex)
            {
                isTransactionLocked = true;
                MessageBox.Show("Unlocking Transaction - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.TabIsGainingFocus();
        }

        private void addNewItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            InventoryItemSelector selector = new InventoryItemSelector();
            selector.ObjectSelected += new RoutedEventHandler(selector_ObjectSelected);
            selector.ShowDialog();
            isItemNew = true;
        }

        private void selector_ObjectSelected(object sender, RoutedEventArgs e)
        {
            InventoryItem item;
            if (((InventoryItemSelector)sender).SelectedItem != null)
            {
                if (isFormHidden)
                {
                    displayOrHideForm();
                }
                item = (InventoryItem)((InventoryItemSelector)sender).SelectedItem;
                ClearFields();
                if (mTransactionItems.ContainsKey(item.getItemID()))
                {
                    mTransactionItem = (InventoryTransactionItem)mTransactionItems[item.getItemID()];
                    if (mTransactionItem.GetInventoryItem() == null)
                    {
                        mTransactionItem.SetInventoryItem(item);
                    }
                    cmdSaveEditItem.Content = unlockBtnTxt;
                    cmdSaveEditItem.IsEnabled = false;
                    cmdCancelItem.IsEnabled = false;
                    cmdRemoveItem.IsEnabled = true;
                }
                else
                {
                    mTransactionItem = new InventoryTransactionItem(mTransaction.GetTransactionID(), item.getItemID());
                    mTransactionItem.SetInventoryItem(item);
                    mTransactionItems.Add(item.getItemID(), mTransactionItem);
                    cmdSaveEditItem.IsEnabled = false;
                    cmdCancelItem.IsEnabled = true;
                    cmdRemoveItem.IsEnabled = false;
                    cmdSaveEditItem.Content = addBtnTxt;
                }
                SelectedItem();
            }
        }

        private void SelectedItem()
        {
            InventoryItem item = mTransactionItem.GetInventoryItem();
            txtLatestCost.Amount = item.getCurrentCost();
            txtItemName.Text = item.getItemName();
            txtItemID.Text = item.getItemID();
            txtCategory.Text = item.getCategory();
            txtAverageCost.Amount = item.getAverageCost();
            unlockItemFields();
        }

        private void ClearFields()
        {
            txtAverageCost.Amount = 0;
            txtCategory.Text = "";
            txtItemID.Text = "";
            txtItemName.Text = "";
            txtLatestCost.Amount = 0;
            txtQuantity.Text = "" + 0;
            cmdCancelItem.IsEnabled = false;
            cmdRemoveItem.IsEnabled = false;
            cmdSaveEditItem.IsEnabled = false;
            isItemModified = false;
        }

        private void lockItemFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));

            txtQuantity.IsReadOnly = true;
            txtLatestCost.IsReadOnly = true;
            txtAverageCost.IsReadOnly = true;


            txtQuantity.Foreground = foreGround;
            txtQuantity.Background = backGround;

            txtLatestCost.Foreground = foreGround;
            txtLatestCost.Background = backGround;

            txtAverageCost.Foreground = foreGround;
            txtAverageCost.Background = backGround;

            amtTotalValue.Foreground = foreGround;
            amtTotalValue.Background = backGround;

            isItemNew = false;

        }

        private void unlockItemFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            if (isRestock)
            {
                txtLatestCost.IsReadOnly = false;
                txtLatestCost.Foreground = foreGround;
                txtLatestCost.Background = backGround;
            }
            txtQuantity.IsReadOnly = false;

            txtQuantity.Foreground = foreGround;
            txtQuantity.Background = backGround;


        }

        private void cancelNewTransaction_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isNewTransaction)
            {
                if (isTransactionModified)
                {
                    MessageBoxResult res = MessageBox.Show("Cancel New Transaction - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        Database.Instance.RollbackTransaction();
                        MainWindow.RemoveTab(this.Name);

                    }
                }
                else
                {
                    MainWindow.RemoveTab(this.Name);
                    Database.Instance.RollbackTransaction();

                }
            }
            else
            {
                Database.Instance.RollbackTransaction();
                LoadTransaction();
                lockItemFields();
                displayOrHideForm();
                this.TabIsGainingFocus();
            }
        }

        private void saveNewTransaction_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (dpTransactionDate.SelectedDate.Equals(DateTime.MinValue) || dgTransactionItems.ItemsSource == null ||  dpTransactionDate.Text.Length < 1)
            {
                MessageBox.Show("Missing Items or Date - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                if (dgTransactionItems.Items.Count == 0)
                {
                                    MessageBox.Show("Missing Items or Date - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
                }
            MessageBoxResult res;
            if (isNewTransaction)
            {
                res = MessageBox.Show("Saving New Transaction - " + msgCodes.GetString("M3201"), "Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else
            {
                res = MessageBox.Show("Saving Transaction Modifications - " + msgCodes.GetString("M3202"), "Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    SetTransactionData();
                    mTransaction.SaveObject(db);
                    db.CommitTransaction();
                    dpTransactionDate.IsReadOnly = true;
                    isTransactionLocked = true;
                    isNewTransaction = false;
                    ClearFields();
                    if (!isFormHidden)
                    {
                        displayOrHideForm();
                    }
                    String oldName = this.Name;
                    this.Name = "TransactionView" + mTransaction.GetTransactionID();
                    MainWindow.UpdateTabTitle(oldName, mTransaction.GetTransactionID() + ": " + dpTransactionDate.SelectedDate.ToShortDateString(), this.Name);
                }
                catch (Exception ex)
                {
                    db.RollbackTransaction();
                    MessageBox.Show("Committing Transaction - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            this.TabIsGainingFocus();
        }

        private void SetTransactionData()
        {
            float totalPrice = 0;
            int totalQuantity = 0;
            foreach (Object key in mTransactionItems.Keys)
            {
                InventoryTransactionItem transItem = (InventoryTransactionItem)mTransactionItems[key];
                totalPrice += transItem.GetUnitPrice() * transItem.GetQuantity();
                totalQuantity += transItem.GetQuantity();
            }
            mTransaction.SetDateOfTransaction(dpTransactionDate.SelectedDate);
            mTransaction.SetTotalQuantity(totalQuantity);
            mTransaction.SetTotalValue(totalPrice);
        }

        private void dpTransactionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isTransactionModified && !dpTransactionDate.SelectedDate.Equals(DateTime.MinValue) && dgTransactionItems.ItemsSource != null)
            {
                isTransactionModified = true;
                TabIsGainingFocus();
            }
        }

        private void TabShowHideButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            displayOrHideForm();
        }

        private void LoadDateGrid()
        {
            mTransactionItems.Clear();
            try
            {
                DataSet data = db.Select("inventory_item_transactions.*,inventory_items.ItemName", "inventory_items LEFT JOIN inventory_item_transactions ON inventory_items.itemID = inventory_item_transactions.itemID", InventoryTransactionItem.Fields.transactionID.ToString() + " = '" + mTransaction.GetTransactionID() + "'");
                Collection<InventoryTransactionItemBinding> gridData = data.getBindableCollection<InventoryTransactionItemBinding>();
                dgTransactionItems.ItemsSource = gridData;
                while (data.Read())
                {
                    InventoryTransactionItem itemObj = new InventoryTransactionItem(data.GetRecordDataSet());
                    if (!mTransactionItems.ContainsKey(itemObj.GetItemID()))
                    {
                        mTransactionItems.Add(itemObj.GetItemID(), itemObj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading Transaction Items - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool SaveChanges()
        {
            if (txtQuantity.Text.Length > 0)
            {
                try
                {
                    int quantity = Int32.Parse(txtQuantity.Text);
                    if (quantity <= 0)
                    {
                        MessageBox.Show("Field 'Quantity' Lower Then Zero - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    mTransactionItem.SetQuantity(quantity);
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Quantity' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Field 'Quantity' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (isRestock)
            {
                if (txtLatestCost.Amount == 0)
                {
                    MessageBox.Show("Field 'Quantity' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    mTransactionItem.SetUnitPrice(txtLatestCost.Amount);
                }
            }
            else
            {
                mTransactionItem.SetUnitPrice(txtAverageCost.Amount);
            }
            return true;
        }

        private void cmdSaveEditItem_Click(object sender, RoutedEventArgs e)
        {
            if (!cmdSaveEditItem.Content.Equals(unlockBtnTxt))
            {
                if (!SaveChanges())
                {
                    return;
                }
                try
                {
                    
                    InventoryItem item = mTransactionItem.GetInventoryItem();

                    if (isRestock)
                    {
                        if (!isItemNew)
                        {
                            RecalculateAverage(item);
                        }
                        float avg = (item.getAverageCost() * item.getQuantity()) + (txtLatestCost.Amount * Convert.ToInt32(txtQuantity.Text));
                        avg = avg / (item.getQuantity() + Convert.ToInt32(txtQuantity.Text));
                        item.setQuantity(item.getQuantity() + Convert.ToInt32(txtQuantity.Text));
                        item.setCurrentCost(txtLatestCost.Amount);
                        item.setAverageCost(avg);
                        item.SaveObject(Database.Instance);
                    }
                    else
                    {
                        if (!isItemNew)
                        {
                            item.setQuantity(item.getQuantity() + mTransactionItem.GetQuantity());
                            item.SaveObject(db);
                        }
                        if (item.getQuantity() - mTransactionItem.GetQuantity() <= 0)
                        {
                            MessageBox.Show("Quantity lower then Transaction Value - " + msgCodes.GetString("M3206"), "Warning - 3206", MessageBoxButton.OK, MessageBoxImage.Warning);
                            item.setQuantity(0);
                            item.setAverageCost(item.getCurrentCost());
                            item.SaveObject(Database.Instance);
                        }
                        else
                        {
                            item.setQuantity(item.getQuantity() - mTransactionItem.GetQuantity());
                            item.SaveObject(Database.Instance);
                        }
                    }
                    mTransactionItem.SaveObject(db);
                    cmdSaveEditItem.Content = unlockBtnTxt;
                    SelectedItem();
                    txtQuantity.Text = "" + mTransactionItem.GetQuantity();
                    lockItemFields();
                    cmdCancelItem.IsEnabled = false;
                    cmdRemoveItem.IsEnabled = true;
                    if (!dpTransactionDate.SelectedDate.Equals(DateTime.MinValue))
                    {
                        isTransactionModified = true;
                    }
                    this.TabIsGainingFocus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to Save Item in Transaction - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadDateGrid();
            }
            else if (!isTransactionLocked)
            {
                unlockItemFields();
                cmdSaveEditItem.IsEnabled = false;
                cmdRemoveItem.IsEnabled = false;
                cmdCancelItem.IsEnabled = true;
                cmdSaveEditItem.Content = saveBtnTxt;
                isItemModified = false;
            }
            else
            {
                MessageBox.Show("Transaction is locked. Please unlock the transaction to modify it.", "Transaction Locked", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RecalculateAverage(InventoryItem item)
        {
            try
            {
                DataSet dataSet = db.Select("inventory_item_transactions.*", "inventory_transactions, inventory_item_transactions", "inventory_transactions.transactionID = inventory_item_transactions.transactionID AND inventory_transactions.ClientType = 2 AND inventory_item_transactions.itemID = '" + item.getItemID() + "'",InventoryTransaction.Fields.DateOfTransaction.ToString());
                item.setQuantity(0);
                item.setCurrentCost(0);
                item.setAverageCost(0);
                item.SaveObject(db);
                float qty = 0;
                float avg = 0;
                while (dataSet.Read())
                {
                    InventoryTransactionItem transItem = new InventoryTransactionItem(dataSet.GetRecordDataSet());
                    if (!transItem.GetTransactionID().Equals(mTransaction.GetTransactionID()))
                    {
                        qty += transItem.GetQuantity();
                        avg = (avg * qty) + (transItem.GetUnitPrice() * transItem.GetQuantity());
                        avg = avg / (qty + transItem.GetQuantity());
                        item.setCurrentCost(transItem.GetUnitPrice());
                    }
                }
                item.setAverageCost(avg);
                item.setQuantity(qty);
                item.SaveObject(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Recalculate new average of item - " + msgCodes.GetString("M2102") + " " + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmdCancelItem_Click(object sender, RoutedEventArgs e)
        {
            if (isItemNew)
            {
                if (isItemModified)
                {
                    MessageBoxResult res = MessageBox.Show("Cancel New Item - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                ClearFields();
                displayOrHideForm();
                mTransactionItems.Remove(mTransactionItem.GetItemID());

            }
            else
            {
                if (isItemModified)
                {
                    MessageBoxResult res = MessageBox.Show("Cancel Item Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                ClearFields();
                SelectedItem();
                lockItemFields();
                txtQuantity.Text = "" + mTransactionItem.GetQuantity();
                
            }
        }

        private void cmdRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (!isTransactionLocked)
            {
                MessageBoxResult res = MessageBox.Show("Are you sure you want to remove this item from the Transaction?", "Removal of Item", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (res == MessageBoxResult.Yes)
                {
                    mTransactionItems.Remove(mTransactionItem.GetItemID());
                    InventoryItem item = mTransactionItem.GetInventoryItem();
                    if (item.getQuantity() > 0)
                    {
                        float newAvg = (item.getAverageCost() * item.getQuantity()) - (mTransactionItem.GetUnitPrice() * mTransactionItem.GetQuantity());
                        newAvg = newAvg / (item.getQuantity() - mTransactionItem.GetQuantity());
                        if (item.getQuantity() - mTransactionItem.GetQuantity() > 0)
                        {
                            item.setAverageCost(newAvg);
                            item.setQuantity(item.getQuantity() - mTransactionItem.GetQuantity());
                        }
                        else
                        {
                            item.setQuantity(0);
                        }
                        item.SaveObject(db);
                    }
                    db.Delete(InventoryTransactionItem.Table, InventoryTransactionItem.Fields.transactionID.ToString() + " = '" + mTransactionItem.GetTransactionID() + "' AND " + InventoryTransactionItem.Fields.itemID.ToString() + " = '" + mTransactionItem.GetItemID() + "'");
                    AuditEvent aEvent = new AuditEvent(db);
                    aEvent.EventDescription(InventoryTransactionItem.Table, "Item " + mTransactionItem.GetItemID() + " with a quantity " + mTransactionItem.GetQuantity() + " has been removed from transaction " + mTransaction.GetTransactionID());
                    aEvent.SaveEvent();
                    isTransactionModified = true;
                    LoadDateGrid();
                    ClearFields();
                    lockItemFields();
                    displayOrHideForm();
                    this.TabIsGainingFocus();
                }
            }
            else
            {
                MessageBox.Show("Transaction is locked. Please unlock the transaction to modify it.", "Transaction Locked", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dgTransactionItems_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = InventoryTransactionItemBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgTransactionItems.Columns)
            {
                int index = dgTransactionItems.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains(InventoryTransactionItem.Fields.transactionID.ToString()))
                {
                    dgTransactionItems.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgTransactionItems.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void dgTransactionItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTransactionItems.SelectedIndex > -1)
            {
                InventoryTransactionItemBinding obj = (InventoryTransactionItemBinding)dgTransactionItems.SelectedCells[0].Item;
                mTransactionItem = (InventoryTransactionItem)mTransactionItems[obj.itemID];
                if (mTransactionItem.GetInventoryItem() == null)
                {
                    try
                    {
                        DataSet data = db.Select("*", InventoryItem.Table, InventoryItem.Fields.itemID.ToString() + " = '" + mTransactionItem.GetItemID() + "'");
                        data.Read();
                        mTransactionItem.SetInventoryItem(new InventoryItem(data.GetRecordDataSet()));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Loading Item Data - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                ClearFields();
                SelectedItem();
                lockItemFields();
                txtQuantity.Text = "" + mTransactionItem.GetQuantity();
                cmdSaveEditItem.IsEnabled = true;
                cmdRemoveItem.IsEnabled = true;
                cmdCancelItem.IsEnabled = false;
                cmdSaveEditItem.Content = unlockBtnTxt;
                if (isFormHidden)
                {
                    displayOrHideForm();
                }
            }
        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            if (!isTransactionLocked)
            {
                MessageBoxResult res;
                if (isTransactionModified && isNewTransaction)
                {
                    res = MessageBox.Show("Cancel new Transaction - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else if (isTransactionModified)
                {
                    res = MessageBox.Show("Cancel Transaction Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBoxResult.Yes;
                }
                if (res == MessageBoxResult.No)
                {
                    return false;
                }
                try
                {

                    db.RollbackTransaction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rolling back Transaction - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return true;
        }

        public bool TabIsLosingFocus()
        {
            if (!isTransactionLocked)
            {
                MessageBoxResult res = MessageBox.Show("Transaction not Saved - " + msgCodes.GetString("M3207"), "Error - 3207", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public void TabIsGainingFocus()
        {
            if (isNewTransaction)
            {
                MainWindow.setActionList(getnewTransactionActions());
            }
            else
            {
                MainWindow.setActionList(getExistingTransactionActions());
            }
            if (dgTransactionItems.Items.Count > 0)
            {
                DataSet qty = db.Select("SUM(" + InventoryTransactionItem.Fields.Quantity.ToString() + ")", InventoryTransactionItem.Table, InventoryTransactionItem.Fields.transactionID.ToString() + " = '" + mTransaction.GetTransactionID() + "'");
                qty.Read();
                txtTotalQuantity.Text = "" + qty.getString(0);

                qty = db.Select("SUM(" + InventoryTransactionItem.Fields.Quantity.ToString() + " * " + InventoryTransactionItem.Fields.UnitPrice.ToString() + ")", InventoryTransactionItem.Table, InventoryTransactionItem.Fields.transactionID.ToString() + " = '" + mTransaction.GetTransactionID() + "'");
                qty.Read();
                amtTotalValue.Amount = Single.Parse(qty.getString(0));
            }
        }

        public string TabTitle()
        {
            if (isNewTransaction)
            {
                if (isRestock)
                {
                    return "New Restock Transaction";
                }
                return "New Transaction";
            }
            return mTransaction.GetTransactionID() + ": " + mTransaction.GetDateOfTransaction().ToShortDateString();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["symbol-transaction"];
        }

        #endregion
    }
}
