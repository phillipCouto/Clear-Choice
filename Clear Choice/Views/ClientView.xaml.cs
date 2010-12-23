using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Clear_Choice.Views;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using Stemstudios.UIControls;

namespace ClearChoice.Views
{
    /// <summary>
    /// Interaction logic for ClientInfo.xaml
    /// </summary>
    public partial class ClientView : UserControl,ISTabView
    {
        private Client mClient;
        private Boolean modified = true;
        private Boolean newClient = false;
        private Hashtable dataGridDisplayText = new Hashtable();
        private String btnSaveText = "Save Changes";
        private String btnUnlockText = "Unlock Form";
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private DataSet dataGridViewData;

        /// <summary>
        /// Initializes the display and loads an empty client object
        /// </summary>
        public ClientView()
        {
            InitializeComponent();
            this.Name = "ClientViewNewClient";

            try
            {
                mClient = new Client(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Client Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            unlockFields();
            cmdSaveEdit.IsEnabled = false;
            newClient = true;
            modified = false;
        }
        /// <summary>
        /// Takes the provided client object and loads it into the display
        /// </summary>
        /// <param name="pClient"></param>
        public ClientView(Client pClient)
        {
            InitializeComponent();
            mClient = pClient;
            PopulateAllFields();
            lockFields();
            cmdSaveEdit.Content = "Unlock Form";
            this.Name = "ClientView" + mClient.GetClientID();
        }
        /// <summary>
        /// Creates the Thread to Async load the associated data for the client.
        /// </summary>
        private void loadClientAssociatedLotsOrSites()
        {
            Thread dataGridLoadingThread = new Thread(threadLoadClientSiteOrLots);
            dataGridLoadingThread.SetApartmentState(ApartmentState.STA);
            dataGridLoadingThread.Start();
        }
        /// <summary>
        /// Unlocks all fields for editing.
        /// </summary>
        private void unlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            txtName.IsReadOnly = false;
            txtName.Foreground = foreGround;
            txtName.Background = backGround;

            txtContactNumber.IsReadOnly = false;
            txtContactNumber.Foreground = foreGround;
            txtContactNumber.Background = backGround;

            txtFaxNumber.IsReadOnly = false;
            txtFaxNumber.Foreground = foreGround;
            txtFaxNumber.Background = backGround;

            txtEmail.IsReadOnly = false;
            txtEmail.Foreground = foreGround;
            txtEmail.Background = backGround;

            txtStreet.IsReadOnly = false;
            txtStreet.Foreground = foreGround;
            txtStreet.Background = backGround;

            txtCity.IsReadOnly = false;
            txtCity.Foreground = foreGround;
            txtCity.Background = backGround;

            txtPostalCode.IsReadOnly = false;
            txtPostalCode.Foreground = foreGround;
            txtPostalCode.Background = backGround;

            cmdCancel.IsEnabled = true;
        }
        /// <summary>
        /// Locks the fields to prevent unintentional editing.
        /// </summary>
        private void lockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));

            txtName.IsReadOnly = true;
            txtName.Foreground = foreGround;
            txtName.Background = backGround;

            txtContactNumber.IsReadOnly = true;
            txtContactNumber.Foreground = foreGround;
            txtContactNumber.Background = backGround;

            txtFaxNumber.IsReadOnly = true;
            txtFaxNumber.Foreground = foreGround;
            txtFaxNumber.Background = backGround;

            txtEmail.IsReadOnly = true;
            txtEmail.Foreground = foreGround;
            txtEmail.Background = backGround;

            txtStreet.IsReadOnly = true;
            txtStreet.Foreground = foreGround;
            txtStreet.Background = backGround;

            txtCity.IsReadOnly = true;
            txtCity.Foreground = foreGround;
            txtCity.Background = backGround;

            txtPostalCode.IsReadOnly = true;
            txtPostalCode.Foreground = foreGround;
            txtPostalCode.Background = backGround;

            cmbTypeOfClient.Foreground = foreGround;
            cmbTypeOfClient.Background = backGround;

            cmbTypeOfClient.IsReadOnly = true;
            cmdCancel.IsEnabled = false;
        }
        /// <summary>
        /// Populates all the fields with the data from the client object.
        /// </summary>
        private void PopulateAllFields()
        {
            txtStreet.Text = mClient.GetAddress();
            txtCity.Text = mClient.GetCity();
            txtContactNumber.Text = mClient.GetPhoneNumber();
            txtEmail.Text = mClient.GetEmailAddress();
            txtName.Text = mClient.GetName();
            txtPostalCode.Text = mClient.GetPostalCode();
            txtFaxNumber.Text = mClient.GetFaxNumber();
            this.Name = "ClientView" + mClient.GetClientID();
            this.cmbTypeOfClient.SelectedIndex = mClient.GetClientType();
            modified = false;
        }
        /// <summary>
        /// Handles the Click event on the Unlock Form / Save Changes button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(btnSaveText))
            {
                MessageBoxResult result;
                if (newClient)
                {
                    result = MessageBox.Show("New Client - "+msgCodes.GetString("M3201"), "Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    result = MessageBox.Show("Client Modifications - " + msgCodes.GetString("M3202"), "Warning - 3202", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                //Saving changes and Locking form.
                if (result == MessageBoxResult.Yes)
                {
                    if (SaveChanges())
                    {
                        String oldName = this.Name;
                        this.Name = "ClientView" + mClient.GetClientID();
                        MainWindow.UpdateTabTitle(oldName, mClient.GetName(), this.Name);
                        PopulateAllFields();
                        lockFields();
                        newClient = false;
                        modified = false;
                        cmdSaveEdit.Content = btnUnlockText;
                    }
                }
            }
            else
            {
                PopulateAllFields();
                unlockFields();
                cmdSaveEdit.IsEnabled = false;
                cmdSaveEdit.Content = btnSaveText;
            }
            if (!newClient)
            {
                loadClientAssociatedLotsOrSites();
            }
            TabIsGainingFocus();
        }
        /// <summary>
        /// This method takes all the data and checks if the pass the format checks
        /// and if they save to the database successfully.
        /// </summary>
        /// <returns></returns>
        private Boolean SaveChanges()
        {
            if (!CheckAndSaveFields())
            {
                return false;
            }
            String where = "LOWER(" + Client.Fields.Name.ToString() + ") = '" + this.txtName.Text.ToLower() + "'";
            where += " AND " + Client.PrimaryKey + " != '" + mClient.GetClientID() + "'";
            try
            {
                DataSet checkForClient = db.Select(Client.PrimaryKey, Client.Table, where);
                if (checkForClient.NumberOfRows() > 0)
                {
                    MessageBox.Show("Client may exist - "+msgCodes.GetString("M3203")+" Name", "Warning - 3203", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                db.BeginTransaction();
                mClient.SaveObject(db);
                db.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                db.RollbackTransaction();
                MessageBox.Show("Saving Client - "+msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                //Log error
            }
            return false;
        }
        /// <summary>
        /// Checks the fields formatting if they are empty and optional.
        /// </summary>
        /// <returns></returns>
        private bool CheckAndSaveFields()
        {
            if (txtName.Text.Length < 1)
            {
                MessageBox.Show("Field 'Name' - " + msgCodes.GetString("M1101"), "ERROR - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mClient.SetName(txtName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Name' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtStreet.Text.Length > 0)
            {
                int code = mClient.SetAddress(txtStreet.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Address' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.Address.ToString());
            }
            if (txtCity.Text.Length > 0)
            {
                int code = mClient.SetCity(txtCity.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'City' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.City.ToString());
            }
            if (txtPostalCode.Text.Length > 0)
            {
                int code = mClient.SetPostalCode(txtPostalCode.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Postal Code' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.PostalCode.ToString());
            }
            if (txtContactNumber.Text.Length > 0)
            {
                int code = mClient.SetPhoneNumber(txtContactNumber.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Phone Number' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.PhoneNumber.ToString());
            }
            if (txtFaxNumber.Text.Length > 0)
            {
                int code = mClient.SetFaxNumber(txtFaxNumber.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Fax Number' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.FaxNumber.ToString());
            }
            if (txtEmail.Text.Length > 0)
            {
                int code = mClient.SetEmailAddress(txtEmail.Text.ToLower());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Email Address' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newClient)
            {
                mClient.ClearField(Client.Fields.EmailAddress.ToString());
            }
            mClient.SetClientType(cmbTypeOfClient.SelectedIndex);
            return true;
        }
        /// <summary>
        /// Handles the click event of the Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!cmdSaveEdit.Content.Equals("Unlock Form"))
            {
                if (modified & !newClient)
                {
                    MessageBoxResult res = MessageBox.Show("Cancel Client Modifications - "+msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                    PopulateAllFields();
                }
                else if (newClient)
                {
                    if (modified)
                    {
                        MessageBoxResult res = MessageBox.Show("Cancel New Client - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (res == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    MainWindow.RemoveTab(this.Name);
                }
                lockFields();
                cmdSaveEdit.IsEnabled = true;
                cmdSaveEdit.Content = "Unlock Form";
            }
            TabIsGainingFocus();
        }
        /// <summary>
        /// This event is triggered when a text field is changed and notifies the form
        /// that fields have been modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fieldValueChanged(object sender, TextChangedEventArgs e)
        {
            if (!modified)
            {
                modified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }
        }
        /// <summary>
        /// This event is fired when a cmbobox changes selection and therefore is dirty.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!modified)
            {
                modified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }
        }
        /// <summary>
        /// This method is called when the DataGrid view is generated the first time. This method
        /// then formats the title for user readability.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siteLotsView_AutoGeneratedColumns(object sender, EventArgs e)
        {
            updateColumnTitles();
        }
        /// <summary>
        /// Returns an action list with actions for a client that already exists.
        /// </summary>
        /// <returns></returns>
        private ArrayList getExistingClientActions()
        {
            ArrayList actions = new ArrayList();
            if (cmdSaveEdit.Content.Equals("Save Changes"))
            {
                if (modified)
                {
                    IconButton saveClientButton = new IconButton();
                    saveClientButton.Text = "Save Client Changes";
                    saveClientButton.Source = (Image)App.iconSet["symbol-save"];
                    saveClientButton.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(saveClientButton);

                    IconButton cancelChangesButton = new IconButton();
                    cancelChangesButton.Text = "Cancel Client Changes";
                    cancelChangesButton.Source = (Image)App.iconSet["symbol-delete"];
                    cancelChangesButton.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(cancelChangesButton);
                }
                else
                {
                    IconButton lockFormButton = new IconButton();
                    lockFormButton.Text = "Lock Form";
                    lockFormButton.Source = (Image)App.iconSet["symbol-lock"];
                    lockFormButton.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(lockFormButton);
                }
            }
            else
            {
                IconButton unlockFormButton = new IconButton();
                unlockFormButton.Text = "Unlock Form";
                unlockFormButton.Source = (Image)App.iconSet["symbol-unlock"];
                unlockFormButton.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(unlockFormButton);

                if (txtCity.Text.Length > 0 && txtStreet.Text.Length > 0)
                {
                    IconButton gmapsButton = new IconButton();
                    gmapsButton.Text = "View Address";
                    gmapsButton.Source = (Image)App.iconSet["symbol-gmaps"];
                    gmapsButton.MouseDown += new MouseButtonEventHandler(lnkGMaps_MouseDown);
                    actions.Add(gmapsButton);
                }

                if (mClient.GetClientType() == 0)
                {
                    IconButton addSiteButton = new IconButton();
                    addSiteButton.Text = "Add New Site";
                    addSiteButton.Source = (Image)App.iconSet["symbol-add"];
                    addSiteButton.MouseDown += new MouseButtonEventHandler(addSiteButton_MouseDown);
                    actions.Add(addSiteButton);
                }
                else
                {
                    IconButton addLotButton = new IconButton();
                    addLotButton.Text = "Add New Lot";
                    addLotButton.Source = (Image)App.iconSet["symbol-add"];
                    addLotButton.MouseDown += new MouseButtonEventHandler(addLotButton_MouseDown);
                    actions.Add(addLotButton);
                }
            }
            return actions;
        }
        /// <summary>
        /// The event handler for when add a new lot button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotView(mClient), (Image)App.iconSet["home"], "New Lot");
        }
        /// <summary>
        /// This is the event handler for when a client clicks Add new Site.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSiteButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new SiteView(mClient));
        }
        /// <summary>
        /// Returns a List of Actions for a New Client.
        /// </summary>
        /// <returns></returns>
        private ArrayList getNewClientActions()
        {
            ArrayList actions = new ArrayList();

            if (modified)
            {
                IconButton saveNewClientBtn = new IconButton();
                saveNewClientBtn.Text = "Save New Client";
                saveNewClientBtn.Source = (Image)App.iconSet["symbol-save"];
                saveNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(saveNewClientBtn);
            }
            IconButton cancelNewClientBtn = new IconButton();
            cancelNewClientBtn.Text = "Cancel New Client";
            cancelNewClientBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewClientBtn);
            return actions;
        }
        /// <summary>
        /// Event that is triggered and the View in Google Maps link is hovered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkGMaps_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        /// <summary>
        /// Triggered when mouse exits the google maps link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkGMaps_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Triggered when the Button is double Clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkGMaps_MouseDown(object sender, MouseButtonEventArgs e)
        {

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            String address = mClient.GetAddress().Replace(" ", "+");
            String city = mClient.GetCity().Replace(" ", "+");
            proc.StartInfo.FileName = "http://maps.google.ca/maps?f=q&source=s_q&hl=en&geocode=&q=" + address + "," + city + ",+Ontario";
            proc.Start();
        }
        /// <summary>
        /// Updates the Column Titles of the Grid view with the Display Text provided by the binding.
        /// </summary>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void updateColumnTitles()
        {
            foreach (DataGridColumn column in siteLotsView.Columns)
            {
                int index = siteLotsView.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    siteLotsView.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (dataGridDisplayText.ContainsKey(headerText))
                    {
                        siteLotsView.Columns[index].Header = dataGridDisplayText[headerText];
                    }
                }
            }
        }
        /// <summary>
        /// Thread method called by the loading associated information thread.
        /// </summary>
        private void threadLoadClientSiteOrLots()
        {
            try
            {
                DispatcherOperation dataOp;
                DispatcherOperation titleOp;
                if (mClient.GetClientType() == 1)
                {
                    DataSet data = db.Select("*", Lot.Table, Lot.Fields.assocID.ToString() + " = '" + mClient.GetClientID() + "'",Lot.Fields.LotNumber.ToString());
                    
                    Collection<LotBinding> gridData = data.getBindableCollection<LotBinding>();
                    Hashtable gridTitles = LotBinding.getdisplayTextMap();
                    dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<LotBinding>>(setDataGridLot), gridData);
                    titleOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Hashtable>(setDataGridTitles), gridTitles);
                    dataGridViewData = data;
                    dataGridViewData.BuildPrimaryKeyIndex(Lot.PrimaryKey);
                }
                else
                {
                    DataSet data = db.Select("*", Site.Table, Client.PrimaryKey + " = '" + mClient.GetClientID() + "'",Site.Fields.SiteName.ToString());
                    Collection<SiteBinding> gridData = data.getBindableCollection<SiteBinding>();
                    Hashtable gridTitles = SiteBinding.getdisplayTextMap();
                    dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<SiteBinding>>(setDataGridSite), gridData);
                    titleOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Hashtable>(setDataGridTitles), gridTitles);
                    dataGridViewData = data;
                    dataGridViewData.BuildPrimaryKeyIndex(Site.PrimaryKey);
                }


                DispatcherOperationStatus status = dataOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = dataOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }

                status = titleOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = titleOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Sites or Lots - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                dataGridViewData = null;
            }
        }
        /// <summary>
        /// This is the dispatch method for a thread to update the UI with grid view data.
        /// </summary>
        /// <param name="gridData"></param>
        private void setDataGridLot(Collection<LotBinding> gridData)
        {
            this.siteLotsView.ItemsSource = gridData;
        }
        /// <summary>
        /// This is the dispatch method for a thread to update the UI with grid view data.
        /// </summary>
        /// <param name="gridData"></param>
        private void setDataGridSite(Collection<SiteBinding> gridData)
        {
            this.siteLotsView.ItemsSource = gridData;
        }
        /// <summary>
        /// This is the dispatch method for a thread to update the UI with grid view data.
        /// </summary>
        /// <param name="columnTitles"></param>
        private void setDataGridTitles(Hashtable columnTitles)
        {
            dataGridDisplayText = columnTitles;
            updateColumnTitles();
        }
        /// <summary>
        /// Handles the mouse up events for the site lot grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void siteLotsView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.ClickCount == 2)
                {
                    if (siteLotsView.SelectedCells.Count > 0)
                    {
                        try
                        {
                            if (mClient.GetClientType() == 1)
                            {
                                LotBinding obj = (LotBinding)siteLotsView.SelectedCells[0].Item;
                                dataGridViewData.SeekToPrimaryKey(obj.lotID);
                                Lot lotObj = new Lot(dataGridViewData.GetRecordDataSet());
                                MainWindow.OpenTab(new LotView(lotObj));
                            }
                            else
                            {
                                SiteBinding obj = (SiteBinding)siteLotsView.SelectedCells[0].Item;
                                dataGridViewData.SeekToPrimaryKey(obj.siteID);
                                Site siteObj = new Site(dataGridViewData.GetRecordDataSet());
                                MainWindow.OpenTab(new SiteView(siteObj, mClient));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Loading Site or Lot Record - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                //Handle right clicks
            }
        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            if (!cmdSaveEdit.Content.Equals("Unlock Form"))
            {
                if (modified & !newClient)
                {
                    MessageBoxResult res = MessageBox.Show("Modifications were made to this client. Are you sure you want to cancel this New Client?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (res == MessageBoxResult.No)
                    {
                        return false;
                    }
                    PopulateAllFields();
                }
                else if (newClient)
                {
                    if (modified)
                    {
                        MessageBoxResult res = MessageBox.Show("Modifications were made to this client. Are you sure you want to cancel and close the tab?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (res == MessageBoxResult.No)
                        {
                            return false;
                        }
                    }
                    MainWindow.RemoveTab(this.Name);
                }
            }
            return true;
        }

        public bool TabIsLosingFocus()
        {
            if (!this.cmdSaveEdit.Content.Equals(btnSaveText))
            {
                this.dataGridViewData = null;
                siteLotsView.ItemsSource = null;
            }
            return true;
        }

        public void TabIsGainingFocus()
        {
            if (newClient)
            {
                MainWindow.setActionList(getNewClientActions());
            }
            else
            {
                MainWindow.setActionList(getExistingClientActions());
                if (!this.cmdSaveEdit.Content.Equals(btnSaveText))
                {
                    loadClientAssociatedLotsOrSites();
                }
            }
        }

        public string TabTitle()
        {
            if (newClient)
            {
                return "New Client";
            }
            return this.mClient.GetName();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["customer1"];
        }

        #endregion
    }
}