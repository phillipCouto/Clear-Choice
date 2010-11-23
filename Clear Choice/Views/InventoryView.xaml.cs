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
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : UserControl, ISTabContent
    {
        private bool isFormHidden = false;
        private String saveBtnTxt = "Save Changes";
        private String unlockBtnTxt = "Unlock Form";
        private bool isFormDirty = true;
        private bool isNewItem = false;
        private InventoryItem mSelectedItem = null;
        private DataSet itemRecords = null;
        private String itemIDChanged = null;
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        /// <summary>
        /// Opens the InventoryView with the form hidden and the items loaded.
        /// </summary>
        public InventoryView()
        {
            this.Name = "InventoryView";
            InitializeComponent();
            displayOrHideForm();
            lockFields();
            clearFields();
        }
        /// <summary>
        /// Opens the inventory view with the new item form ready.
        /// </summary>
        /// <param name="newItem"></param>
        public InventoryView(bool newItem)
        {
            this.Name = "InventoryView";
            InitializeComponent();
            TabShowHideButton.Source = ((Image)App.iconSet["tab-hide"]).Source;
            clearFields();
            isNewItem = true;
            try
            {
                mSelectedItem = new InventoryItem(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inventory Item Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cmdSaveEdit.IsEnabled = false;
            cmdCancel.IsEnabled = true;
            loadInventory();
        }
        /// <summary>
        /// Unlocks the fields for modification.
        /// </summary>
        private void unlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

            txtItemID.IsReadOnly = false;
            txtItemID.Foreground = foreGround;
            txtItemID.Background = backGround;

            txtItemName.IsReadOnly = false;
            txtItemName.Foreground = foreGround;
            txtItemName.Background = backGround;

            txtItemDescription.IsReadOnly = false;
            txtItemDescription.Foreground = foreGround;
            txtItemDescription.Background = backGround;

            cmboCategory.IsReadOnly = false;
            cmboCategory.Foreground = foreGround;
            cmboCategory.Background = backGround;

            cmdSaveEdit.IsEnabled = false;
            cmdSaveEdit.Content = saveBtnTxt;
            cmdCancel.IsEnabled = true;
        }
        /// <summary>
        /// Locks the fields to prevent modification.
        /// </summary>
        private void lockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));

            txtItemID.IsReadOnly = true;
            txtItemID.Foreground = foreGround;
            txtItemID.Background = backGround;

            txtItemName.IsReadOnly = true;
            txtItemName.Foreground = foreGround;
            txtItemName.Background = backGround;

            txtItemDescription.IsReadOnly = true;
            txtItemDescription.Foreground = foreGround;
            txtItemDescription.Background = backGround;

            cmboCategory.IsReadOnly = true;
            cmboCategory.Foreground = foreGround;
            cmboCategory.Background = backGround;

            txtAverageCost.IsReadOnly = true;
            txtAverageCost.Foreground = foreGround;
            txtAverageCost.Background = backGround;

            txtLatestCost.IsReadOnly = true;
            txtLatestCost.Foreground = foreGround;
            txtLatestCost.Background = backGround;

            cmdSaveEdit.IsEnabled = true;
            cmdSaveEdit.Content = unlockBtnTxt;
            cmdCancel.IsEnabled = false;
        }
        /// <summary>
        /// Sets the values of the fields in regards to the item object.
        /// </summary>
        private void populateFields()
        {
            txtItemID.Text = mSelectedItem.getItemID();
            txtItemName.Text = mSelectedItem.getItemName();
            txtItemDescription.Text = mSelectedItem.getItemDescription();
            cmboCategory.Text = mSelectedItem.getCategory();
            txtQuantity.Text = "" + mSelectedItem.getQuantity();
            txtAverageCost.Amount = mSelectedItem.getAverageCost();
            txtLatestCost.Amount = mSelectedItem.getCurrentCost();
            isFormDirty = false;
        }
        /// <summary>
        /// Cleans the fields to make a blank form.
        /// </summary>
        private void clearFields()
        {
            txtItemID.Text = "";
            txtItemName.Text = "";
            txtItemDescription.Text = "";
            cmboCategory.Text = "";
            txtQuantity.Text = "0";
            txtAverageCost.Amount = 0;
            txtLatestCost.Amount = 0;
            cmboCategory.Text = "";
            isFormDirty = false;
            mSelectedItem = null;
            cmdSaveEdit.IsEnabled = false;
            cmdCancel.IsEnabled = false;
        }
        /// <summary>
        /// Event used to handle the hiding and showing of a form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabShowHideButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            displayOrHideForm();
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
        /// <summary>
        /// Starts the thread to load the inventory.
        /// </summary>
        private void loadInventory()
        {
            Thread inventoryThread = new Thread(threadLoadIventoryData);
            inventoryThread.SetApartmentState(ApartmentState.STA);
            inventoryThread.Start();
        }
        /// <summary>
        /// The thread entrypoint for loading the inventory data from the database.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void threadLoadIventoryData()
        {
            try
            {
                DataSet data = db.Select("*", InventoryItem.Table);
                DataSet cats = db.Select("DISTINCT " + InventoryItem.Fields.Category.ToString(), InventoryItem.Table);

                data.BuildPrimaryKeyIndex(InventoryItem.PrimaryKey);

                Collection<InventoryItemBinding> gridData = data.getBindableCollection<InventoryItemBinding>();
                DispatcherOperation dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<InventoryItemBinding>>(setDataGridViewData), gridData);
                DispatcherOperationStatus status = dataOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = dataOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
                dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<DataSet>(setCategoryData), cats);
                status = dataOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = dataOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
                itemRecords = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Inventory Items - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// This is the call back method so the thread can set the items source for the grid view.
        /// </summary>
        /// <param name="data"></param>
        private void setDataGridViewData(Collection<InventoryItemBinding> data)
        {
            this.inventoryGridView.ItemsSource = data;
        }
        /// <summary>
        /// Populates the Category combo box with the categories in the database.
        /// </summary>
        /// <param name="cats"></param>
        private void setCategoryData(DataSet cats)
        {
            cmboCategory.Items.Clear();
            while (cats.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = cats.getString(InventoryItem.Fields.Category.ToString());
                cmboCategory.Items.Add(item);
            }
            if (mSelectedItem != null & !isNewItem)
            {
                cmboCategory.Text = mSelectedItem.getCategory();
            }
        }
        /// <summary>
        /// Handles the columns generated event to format column headers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryGridView_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = InventoryItemBinding.getDisplayTextMap();
            foreach (DataGridColumn column in inventoryGridView.Columns)
            {
                int index = inventoryGridView.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (textMap.ContainsKey(headerText))
                {
                    inventoryGridView.Columns[index].Header = textMap[headerText];
                }
            }
        }
        /// <summary>
        /// Handles the event when a field is modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isFormDirty)
            {
                cmdSaveEdit.IsEnabled = true;
                isFormDirty = true;
            }
        }
        /// <summary>
        /// Handles the event when the Category is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFormDirty)
            {
                cmdSaveEdit.IsEnabled = true;
                isFormDirty = true;
            }
        }
        /// <summary>
        /// Handles the event when the save or unlock button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSaveEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                MessageBoxResult res;
                if (isNewItem)
                {
                    res = MessageBox.Show("Saving New Item - " + msgCodes.GetString("M3201"), " Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBox.Show("Saving Item Modifications - " + msgCodes.GetString("M3201"), " Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                if (res == MessageBoxResult.No)
                {
                    return;
                }
                if (SaveChanges())
                {
                    if (IsItemUnique())
                    {

                        try
                        {
                            db.BeginTransaction();
                            mSelectedItem.SaveObject(db);
                            if (itemIDChanged != null)
                            {
                                String where = InventoryItem.Fields.itemID + " = '" + itemIDChanged + "'";
                                String update = InventoryItem.Fields.itemID + " = '" + mSelectedItem.getItemID() + "'";
                                db.Update(InventoryTransactionItem.Table, update, where);
                            }
                            db.CommitTransaction();
                            itemIDChanged = null;
                            isNewItem = false;
                            lockFields();
                            loadInventory();
                        }
                        catch (Exception ex)
                        {
                            db.RollbackTransaction();
                            MessageBox.Show("Inventory Item Saving Failed - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Inventory Item may exist - " + msgCodes.GetString("M3203") + " Item ID, Item Name", "Error - 3203", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                if (mSelectedItem != null)
                {
                    unlockFields();
                    isFormDirty = false;
                }
            }
        }
        /// <summary>
        /// Checks if the item is unique in the database.
        /// </summary>
        /// <returns></returns>
        private bool IsItemUnique()
        {
            StringBuilder where = new StringBuilder();
            if (isNewItem)
            {
                where.Append(InventoryItem.Fields.itemID.ToString() + " = '" + txtItemID.Text.ToUpper() + "'");
                where.Append(" OR " + InventoryItem.Fields.ItemName.ToString() + " = '" + txtItemName.Text.ToUpper() + "'");
            }
            else
            {
                if (!txtItemID.Text.Equals(mSelectedItem.getItemID()) && !txtItemName.Text.Equals(mSelectedItem.getItemName()))
                {
                    where.Append(InventoryItem.Fields.itemID.ToString() + " = '" + txtItemID.Text.ToUpper() + "'");
                    where.Append(" OR " + InventoryItem.Fields.ItemName.ToString() + " = '" + txtItemName.Text.ToUpper() + "'");
                    where.Append(" AND " + InventoryItem.Fields.itemID.ToString() + " != '" + mSelectedItem.getItemID() + "'");
                    where.Append(" AND " + InventoryItem.Fields.ItemName.ToString() + " != '" + mSelectedItem.getItemName() + "'");
                }
                else if (!txtItemID.Text.Equals(mSelectedItem.getItemID()))
                {
                    where.Append(InventoryItem.Fields.itemID.ToString() + " = '" + txtItemID.Text.ToUpper() + "'");
                    where.Append(" AND " + InventoryItem.Fields.itemID.ToString() + " != '" + mSelectedItem.getItemID() + "'");
                }
                else if (!txtItemName.Text.Equals(mSelectedItem.getItemName()))
                {
                    where.Append(InventoryItem.Fields.ItemName.ToString() + " = '" + txtItemName.Text.ToUpper() + "'");
                    where.Append(" AND " + InventoryItem.Fields.ItemName.ToString() + " != '" + mSelectedItem.getItemName() + "'");
                }
                else
                {
                    return true;
                }
            }
            try
            {
                DataSet data = db.Select("*", InventoryItem.Table, where.ToString());
                if (data.NumberOfRows() > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inventory Item Unique Check - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sets the fields to the object and confirms formatting is correct.
        /// </summary>
        /// <returns></returns>
        private bool SaveChanges()
        {
            if (txtItemID.Text.Length < 1)
            {
                MessageBox.Show("Field 'Item ID' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                if (!isNewItem)
                {

                    if (!txtItemID.Text.Equals(mSelectedItem.getItemID()))
                    {
                        itemIDChanged = mSelectedItem.getItemID();
                    }
                }
                int code = mSelectedItem.setItemID(txtItemID.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Item ID' - " + msgCodes.GetString("M1104"), "Error - 1104", MessageBoxButton.OK, MessageBoxImage.Error);
                    itemIDChanged = null;
                    return false;
                }
            }
            if (txtItemName.Text.Length < 1)
            {
                MessageBox.Show("Field 'Item Name' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mSelectedItem.setItemName(txtItemName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Item Name' - " + msgCodes.GetString("M1102"), "Error - 1102", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (cmboCategory.Text.Length < 1)
            {
                MessageBox.Show("Field 'Item Cagetgory' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mSelectedItem.setCategory(cmboCategory.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Item Category' - " + msgCodes.GetString("M1102"), "Error - 1102", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtItemDescription.Text.Length > 0)
            {
                mSelectedItem.setItemDescription(txtItemDescription.Text);
            }
            else
            {
                mSelectedItem.ClearField(InventoryItem.Fields.ItemDescription.ToString());
            }
            return true;
        }
        /// <summary>
        /// Handles the event of the user cancelling the operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isNewItem)
            {
                if (isFormDirty)
                {
                    MessageBoxResult res = MessageBox.Show("You are about to cancel adding a new item. Do you wish to continue?", "Cancel New Item", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.Yes)
                    {
                        clearFields();
                        lockFields();
                    }
                }
                else
                {
                    lockFields();
                }
                displayOrHideForm();
            }
            else
            {
                if (isFormDirty)
                {
                    MessageBoxResult res = MessageBox.Show("You are about to cancel changes made to this item. Do you wish to continue?", "Cancel Modifications", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.Yes)
                    {
                        lockFields();
                        populateFields();
                    }
                }
                else
                {
                    lockFields();
                }
            }
        }
        /// <summary>
        /// Handles the event of when the form visibility is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                if (isNewItem)
                {
                    MainWindow.setActionList(getNewItemActions());
                }
                else
                {
                    if (cmdSaveEdit.Content.Equals(unlockBtnTxt))
                    {
                        MainWindow.setActionList(getIventoryActions());
                    }
                    else
                    {
                        MainWindow.setActionList(getExistingItemActions());
                    }
                }
                if (cmdSaveEdit.Content.Equals(unlockBtnTxt))
                {
                    loadInventory();
                }
            }
            else
            {
                if (cmdSaveEdit.Content.Equals(unlockBtnTxt))
                {
                    itemRecords = null;
                    inventoryGridView.ItemsSource = null;
                }
            }
        }
        /// <summary>
        /// Returns the list of actions in regards to the inventory Module
        /// </summary>
        /// <returns></returns>
        private ArrayList getIventoryActions()
        {
            ArrayList actions = new ArrayList();

            IconButton addItemBtn = new IconButton();
            addItemBtn.Text = "Add Item";
            addItemBtn.Source = (Image)App.iconSet["symbol-add"];
            addItemBtn.MouseDown += new System.Windows.Input.MouseButtonEventHandler(addItemBtn_MouseDown);
            actions.Add(addItemBtn);


            IconButton viewTransactions = new IconButton();
            viewTransactions.Text = "View Transactions";
            viewTransactions.Source = (Image)App.iconSet["symbol-transactions"];
            viewTransactions.MouseDown += new MouseButtonEventHandler(viewTransactions_MouseDown);
            actions.Add(viewTransactions);

            return actions;
        }

        private void viewTransactions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new InventoryTransactionsView(), (Image)App.iconSet["symbol-transactions"], "Inventory Transactions");
        }
        /// <summary>
        /// Returns the list of actions in regards to modifying an existing iventory item.
        /// </summary>
        /// <returns></returns>
        private ArrayList getExistingItemActions()
        {
            ArrayList actions = new ArrayList();
            if (isFormDirty)
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
        /// <summary>
        /// Returns the list of actions in regards to creating a new item.
        /// </summary>
        /// <returns></returns>
        private ArrayList getNewItemActions()
        {
            ArrayList actions = new ArrayList();

            if (isFormDirty)
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
        /// <summary>
        /// Event when the add Item action is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemBtn_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isFormHidden)
            {

                displayOrHideForm();
            }

            try
            {
                clearFields();
                mSelectedItem = new InventoryItem(db);
                unlockFields();
                isNewItem = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Inventory Item Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Handles the event when an item is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inventoryGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inventoryGridView.SelectedIndex != -1)
            {
                try
                {
                    InventoryItemBinding obj = (InventoryItemBinding)inventoryGridView.SelectedCells[0].Item;
                    itemRecords.SeekToPrimaryKey(obj.itemID);
                    mSelectedItem = new InventoryItem(itemRecords.GetRecordDataSet());
                    populateFields();
                    lockFields();
                    cmdSaveEdit.IsEnabled = true;
                    if (isFormHidden)
                    {
                        displayOrHideForm();
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Loading Item - " + msgCodes.GetString("M2102"), "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #region ISTabContent Members

        public bool TabIsClosingCallBack()
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt) && isFormDirty)
            {
                if (isNewItem)
                {
                    MessageBoxResult res = MessageBox.Show("Saving New Item - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("Saving Item Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool TabIsLosingFocusCallBack()
        {
            return true;
        }

        #endregion
    }
}
