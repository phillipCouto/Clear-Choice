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
using Clear_Choice.Windows;
using ClearChoice;
using ClearChoice.Views;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using Stemstudios.UIControls;

namespace Clear_Choice.Views
{
    public partial class SiteView : UserControl,ISTabView
    {
        private Hashtable siteTable = new Hashtable();
        private Site mSite;
        private Client mClient;
        private Boolean newSite = false;
        private bool isModified = true;
        private String SaveBtnTxt = "Save Changes";
        private String UnlockBtnTxt = "Unlock Form";
        private Database db = Database.Instance;
        private SiteContact foreman;
        private SiteContact SuperVisor1;
        private SiteContact SuperVisor2;
        private SiteContact SupplyAuth;
        private int selectedSiteContact = 0;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private DataSet dataGridViewData;

        /// <summary>
        /// Creates a site referencing the provided client.
        /// </summary>
        /// <param name="pClient"></param>
        public SiteView(Client pClient)
        {
            try
            {
                mSite = new Site(db);
                foreman = new SiteContact(SiteContact.ContactTypes.Foreman, mSite.GetSiteID());
                SuperVisor1 = new SiteContact(SiteContact.ContactTypes.Supervisor1, mSite.GetSiteID());
                SuperVisor2 = new SiteContact(SiteContact.ContactTypes.Supervisor2, mSite.GetSiteID());
                SupplyAuth = new SiteContact(SiteContact.ContactTypes.SupplyAuth, mSite.GetSiteID());
                mSite.SetClientID(pClient.GetClientID());
                mClient = pClient;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Site Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
            isModified = false;
            this.Name = "SiteNewSite";
            newSite = true;
        }
        /// <summary>
        /// Loads an existing site and allows for reference to client.
        /// </summary>
        /// <param name="pSite"></param>
        /// <param name="pClient"></param>
        public SiteView(Site pSite, Client pClient)
        {
            mSite = pSite;
            mClient = pClient;
            try
            {
                foreman = mSite.GetSiteContact(SiteContact.ContactTypes.Foreman);
                SuperVisor1 = mSite.GetSiteContact(SiteContact.ContactTypes.Supervisor1);
                SuperVisor2 = mSite.GetSiteContact(SiteContact.ContactTypes.Supervisor2);
                SupplyAuth = mSite.GetSiteContact(SiteContact.ContactTypes.SupplyAuth);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Site Contacts - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            InitializeComponent();
            lockFields();
            this.Name = "SiteView" + mSite.GetSiteID();
            PopulateAllFields();
            cmdSaveEdit.IsEnabled = true;
            isModified = false;
        }
        /// <summary>
        /// This confirms that all formatting is met on the fields.
        /// </summary>
        /// <returns></returns>
        private bool SaveChanges()
        {
            if (txtAddress.Text.Length == 0)
            {
                MessageBox.Show("Field 'Address' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mSite.SetAddress(txtAddress.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Address' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtCity.Text.Length == 0)
            {
                MessageBox.Show("Field 'City' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mSite.SetCity(txtCity.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'City' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtSiteName.Text.Length == 0)
            {
                MessageBox.Show("Field 'Site Name' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                int code = mSite.SetSiteName(txtSiteName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Site Name' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtServiceSize.Text.Length > 0)
            {
                try
                {
                    mSite.SetServiceSize(Int32.Parse(txtServiceSize.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Service Size' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.ServiceSize.ToString());
            }
            if (txtSiteEmail.Text.Length > 0)
            {
                int code = mSite.SetSiteEmail(txtSiteEmail.Text.ToLower());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Site Email' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                try
                {
                    DataSet data = db.Select("*", Site.Table, Site.Fields.SiteEmail.ToString() + " = '" + txtSiteEmail.Text + "' AND "+Site.Fields.siteID.ToString()+" != '"+mSite.GetSiteID()+"'");
                    if (data.NumberOfRows() > 0)
                    {
                        MessageBox.Show("Field 'Site Email' - " + msgCodes.GetString("M1109"), "Error - 1109", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Checking Site Email - " + msgCodes.GetString("M2102")+" "+ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.SiteEmail.ToString());
                mSite.RemovePortalAccess();
            }
            if (txtInspectorName.Text.Length > 0)
            {
                int code = mSite.SetInspectorName(txtInspectorName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Inspector Name' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.InspectorName.ToString());
            }
            if (txtInspectorOffice.Text.Length > 0)
            {
                int code = mSite.SetInspectorOffice(txtInspectorOffice.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Inspector Office' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.InspectorOffice.ToString());
            }
            if (txtInspectorOfficeNumber.PhoneNumber.Length > 0)
            {
                int code = mSite.SetInspectorOfficeNumber(txtInspectorOfficeNumber.PhoneNumber);
                if (code > 0)
                {
                    MessageBox.Show("Field 'inspector Office Number' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.InspectorOfficePhone.ToString());
            }
            if (txtInspectorCellNumber.PhoneNumber.Length > 0)
            {
                int code = mSite.SetInspectorCellNumber(txtInspectorCellNumber.PhoneNumber);
                if (code > 0)
                {
                    MessageBox.Show("Field 'Inspector Cell Number' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.InspectorCellPhone.ToString());
            }
            if (txtInspectorEmail.Text.Length > 0)
            {
                int code = mSite.SetInspectorEmail(txtInspectorEmail.Text.ToLower());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Inspector Email' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.InspectorEmail.ToString());
            }
            SiteContact oldContact = GetReferencedSiteContact(selectedSiteContact);
            if (txtContactName.Text.Length > 0)
            {
                int code = oldContact.SetName(txtContactName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Name' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                oldContact.ClearField(SiteContact.Fields.Name.ToString());
                oldContact.ClearFieldUpdate(SiteContact.Fields.Name);
            }
            if (txtContactPhone.PhoneNumber.Length > 0)
            {
                int code = oldContact.SetPhone(txtContactPhone.PhoneNumber);
                if (code > 0)
                {
                    MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Phone' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                oldContact.ClearField(SiteContact.Fields.Phone.ToString());
                oldContact.ClearFieldUpdate(SiteContact.Fields.Phone);
            }
            if (txtContactEmail.Text.Length > 0)
            {
                int code = oldContact.SetEmail(txtContactEmail.Text.ToLower());
                if (code > 0)
                {
                    MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Email' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!newSite)
            {
                oldContact.ClearField(SiteContact.Fields.Email.ToString());
                oldContact.ClearFieldUpdate(SiteContact.Fields.Email);
            }
            if (txtNotes.Text.Length > 0)
            {
                mSite.SetNotes(txtNotes.Text);
            }
            else if (!newSite)
            {
                mSite.ClearField(Site.Fields.Notes.ToString());
            }
            return true;
        }
        /// <summary>
        /// Populates all the fields using the provided Site object.
        /// </summary>
        private void PopulateAllFields()
        {
            txtSiteName.Text = mSite.GetSiteName();
            txtAddress.Text = mSite.GetAddress();
            txtCity.Text = mSite.GetCity();
            txtServiceSize.Text = ((mSite.GetServiceSize() == 0) ? "" : "" + mSite.GetServiceSize());

            txtSiteEmail.Text = mSite.GetSiteEmail();

            txtInspectorName.Text = mSite.GetInspectorName();
            txtInspectorOffice.Text = mSite.GetInspectorOffice();
            txtInspectorOfficeNumber.PhoneNumber = mSite.GetInspectorOfficeNumber();
            txtInspectorCellNumber.PhoneNumber = mSite.GetInspectorCellNumber();
            txtInspectorEmail.Text = mSite.GetInspectorEmail();

            txtNotes.Text = mSite.GetNotes();

            isModified = false;
        }
        /// <summary>
        /// Locks the fields to prevent accidental modification.
        /// </summary>
        private void lockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));
            ArrayList boxes = new ArrayList();
            ArrayList phones = new ArrayList();

            boxes.Add(txtAddress);
            boxes.Add(txtCity);
            boxes.Add(txtSiteEmail);
            boxes.Add(txtInspectorName);
            boxes.Add(txtInspectorOffice);
            boxes.Add(txtInspectorEmail);
            boxes.Add(txtServiceSize);
            boxes.Add(txtSiteName);
            boxes.Add(txtContactEmail);
            boxes.Add(txtContactName);
            boxes.Add(txtNotes);

            phones.Add(txtContactPhone);
            phones.Add(txtInspectorOfficeNumber);
            phones.Add(txtInspectorCellNumber);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = true;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            foreach (SPhoneField phone in phones)
            {
                phone.IsReadOnly = true;
                phone.Foreground = foreGround;
                phone.Background = backGround;
            }

            cmdCancel.IsEnabled = false;
            cmdSaveEdit.IsEnabled = true;
            cmdSaveEdit.Content = UnlockBtnTxt;
        }
        /// <summary>
        /// Unlocks the fields for allowing modification.
        /// </summary>
        private void unlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            ArrayList boxes = new ArrayList();
            ArrayList phones = new ArrayList();

            boxes.Add(txtAddress);
            boxes.Add(txtCity);
            boxes.Add(txtSiteEmail);
            boxes.Add(txtInspectorName);
            boxes.Add(txtInspectorOffice);
            boxes.Add(txtInspectorEmail);
            boxes.Add(txtServiceSize);
            boxes.Add(txtSiteName);
            boxes.Add(txtContactEmail);
            boxes.Add(txtContactName);
            boxes.Add(txtNotes);

            phones.Add(txtContactPhone);
            phones.Add(txtInspectorOfficeNumber);
            phones.Add(txtInspectorCellNumber);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = false;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            foreach(SPhoneField phone in phones)
            {
                phone.IsReadOnly = false;
                phone.Foreground = foreGround;
                phone.Background = backGround;
            }

            cmdCancel.IsEnabled = true;
            cmdSaveEdit.Content = SaveBtnTxt;
        }
        /// <summary>
        /// Handles the Google maps button client and opens a web browser.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GMapsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            String address = mSite.GetAddress().Replace(" ", "+");
            String city = mSite.GetCity().Replace(" ", "+");
            proc.StartInfo.FileName = "http://maps.google.ca/maps?f=q&source=s_q&hl=en&geocode=&q=" + address + "," + city + ",+Ontario";
            proc.Start();
        }
        /// <summary>
        /// Handles the event when a text field is modified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFieldChanged(object sender, TextChangedEventArgs e)
        {
            if (!isModified)
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }
        }
        /// <summary>
        /// Returns the action list when viewing an existing client.
        /// </summary>
        /// <returns></returns>
        private ArrayList getExistingSiteActionMenu()
        {
            ArrayList actions = new ArrayList();
            if (cmdSaveEdit.Content.Equals(SaveBtnTxt))
            {
                if (isModified)
                {
                    IconButton saveClientButton = new IconButton();
                    saveClientButton.Text = "Save Site Changes";
                    saveClientButton.Source = (Image)App.iconSet["symbol-save"];
                    saveClientButton.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(saveClientButton);

                    IconButton cancelChangesButton = new IconButton();
                    cancelChangesButton.Text = "Cancel Site Changes";
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

                IconButton addLotButton = new IconButton();
                addLotButton.Text = "Add New Lot";
                addLotButton.Source = (Image)App.iconSet["symbol-add"];
                addLotButton.MouseDown += new MouseButtonEventHandler(addLotButton_MouseDown);
                actions.Add(addLotButton);

                if (txtAddress.Text.Length > 0 && txtCity.Text.Length > 0)
                {
                    IconButton gmapsButton = new IconButton();
                    gmapsButton.Text = "View Address";
                    gmapsButton.Source = (Image)App.iconSet["symbol-gmaps"];
                    gmapsButton.MouseDown += new MouseButtonEventHandler(GMapsBtn_MouseDown);
                    actions.Add(gmapsButton);
                }

                IconButton viewClientButton = new IconButton();
                viewClientButton.Text = "View " + mClient.GetName();
                viewClientButton.Source = (Image)App.iconSet["customer1"];
                viewClientButton.MouseDown += new MouseButtonEventHandler(viewClientButton_MouseDown);
                actions.Add(viewClientButton);

                IconButton viewTransactions = new IconButton();
                viewTransactions.Text = "View Transactions";
                viewTransactions.Source = (Image)App.iconSet["symbol-transactions"];
                viewTransactions.MouseDown += new MouseButtonEventHandler(viewTransactions_Click);
                actions.Add(viewTransactions);

                if (mSite.GetPassword().Length > 0)
                {
                    IconButton btnResetPassword = new IconButton();
                    btnResetPassword.Text = "Reset Portal Password";
                    btnResetPassword.Source = (Image)App.iconSet["symbol-Refresh"];
                    btnResetPassword.MouseDown += new MouseButtonEventHandler(btnResetPassword_MouseDown);
                    actions.Add(btnResetPassword);

                    IconButton btnRemoveAccess = new IconButton();
                    btnRemoveAccess.Text = "Remove Portal Access";
                    btnRemoveAccess.Source = (Image)App.iconSet["symbol-Restricted"];
                    btnRemoveAccess.MouseDown += new MouseButtonEventHandler(btnRemoveAccess_MouseDown);
                    actions.Add(btnRemoveAccess);
                }
                else if(mSite.GetSiteEmail().Length > 0)
                {
                   IconButton btnGrantAccess = new IconButton();
                    btnGrantAccess.Text = "Grant Portal Access";
                    btnGrantAccess.Source = (Image)App.iconSet["symbol-Check"];
                    btnGrantAccess.MouseDown += new MouseButtonEventHandler(btnGrantAccess_MouseDown);
                    actions.Add(btnGrantAccess);
                }

            }
            return actions;
        }

        private void btnGrantAccess_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show(msgCodes.GetString("M4202"),"Warning - 4202",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                String password = mSite.GenerateTempPassword();
                try
                {
                    mSite.SaveObject(db);
                    new GeneratedPasswordDisplay(password,mSite.GetSiteEmail()).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Granting Portal Access - "+msgCodes.GetString("M2102")+ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            TabIsGainingFocus();
        }

        private void btnRemoveAccess_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show(msgCodes.GetString("M4203"), "Warning - 4203", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    mSite.RemovePortalAccess();
                    mSite.SaveObject(db);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Removing Portal Access - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            TabIsGainingFocus();
        }

        private void btnResetPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show(msgCodes.GetString("M4201"), "Warning - 4201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (res == MessageBoxResult.Yes)
            {
                String password = mSite.GenerateTempPassword();
                try
                {
                    mSite.SaveObject(db);
                    new GeneratedPasswordDisplay(password, mSite.GetSiteEmail()).ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resting Portal Password - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            TabIsGainingFocus();
        }

        private void viewTransactions_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new InventoryTransactionsView());
        }

        /// <summary>
        /// Handles the eevent when the View client button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewClientButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new ClientView(mClient));
        }
        /// <summary>
        /// Handles the event when the add lot button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLotButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotView(mSite));
        }
        /// <summary>
        /// Returns the list of actions in regards to a new client.
        /// </summary>
        /// <returns></returns>
        private ArrayList getNewSiteActionMenu()
        {
            ArrayList actions = new ArrayList();

            if (isModified)
            {
                IconButton saveNewSiteBtn = new IconButton();
                saveNewSiteBtn.Text = "Save New Site";
                saveNewSiteBtn.Source = (Image)App.iconSet["symbol-save"];
                saveNewSiteBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(saveNewSiteBtn);
            }
            IconButton cancelNewClientBtn = new IconButton();
            cancelNewClientBtn.Text = "Cancel New Site";
            cancelNewClientBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewClientBtn);
            return actions;
        }
        /// <summary>
        /// Handles the click event of the save or unlock button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(SaveBtnTxt) & isModified)
            {
                MessageBoxResult result;
                if (newSite)
                {
                    result = MessageBox.Show("You are about to save this new Site to the database. Are you sure you want to continue?", "Saving New Site", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    result = MessageBox.Show("You are about to save changes made to this Site to the database. Are you sure you want to continue?", "Saving Site Modification", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                //Saving changes and Locking form.
                if (result == MessageBoxResult.Yes)
                {
                    if (SaveChanges())
                    {
                        if (siteIsUnique())
                        {
                            try
                            {
                                db.BeginTransaction();
                                mSite.SaveObject(db);
                                foreman.SaveObject(db);
                                SuperVisor1.SaveObject(db);
                                SuperVisor2.SaveObject(db);
                                SupplyAuth.SaveObject(db);
                                db.CommitTransaction();
                                lockFields();
                                newSite = false;
                                PopulateAllFields();
                                PopulateSiteContactFields();
                                String newName = "SiteView" + mSite.GetSiteID();
                                isModified = false;
                                MainWindow.UpdateTabTitle(this.Name, mSite.GetSiteName(), newName);
                                this.Name = newName;

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("There was an error when committing changes to the database. Please contact administrator." + ex.Message, "Database Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                                //Log Event
                                db.RollbackTransaction();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The site information provided must not match any other site in the system. Please confirm the information is correct.", "Information is not unique!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                unlockFields();
                cmdSaveEdit.IsEnabled = false;
                cmdSaveEdit.Content = SaveBtnTxt;
                isModified = false;
            }
            TabIsGainingFocus();
        }
        /// <summary>
        /// Checks if the Site is unique.
        /// </summary>
        /// <returns></returns>
        private bool siteIsUnique()
        {
            try
            {
                String where;
                if (newSite)
                {
                    where = "LOWER(" + Site.Fields.Address + ") = '" + txtAddress.Text.ToLower() + "'";
                    where += " AND LOWER(" + Site.Fields.City + ") = '" + txtCity.Text.ToLower() + "'";
                    if (db.Select(Site.Fields.siteID.ToString(), Site.Table, where).NumberOfRows() > 1)
                    {
                        return false;
                    }
                }
                else if (!txtAddress.Text.Equals(mSite.GetAddress()) | txtCity.Text.Equals(mSite.GetCity()))
                {
                    where = "LOWER(" + Site.Fields.Address + ") = '" + txtAddress.Text.ToLower() + "'";
                    where += " AND LOWER(" + Site.Fields.City + ") = '" + txtCity.Text.ToLower() + "'";
                    if (db.Select(Site.Fields.siteID.ToString(), Site.Table, where).NumberOfRows() > 1)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checking Site Uniqueness - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            
                return false;
            }
        }
        /// <summary>
        /// Handles the event when the cancel button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(SaveBtnTxt))
            {
                MessageBoxResult res;
                if (isModified && newSite)
                {
                    res = MessageBox.Show("Cancel new Site  - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else if (isModified)
                {
                    res = MessageBox.Show("Cancel Site Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBoxResult.Yes;
                }
                if (res == MessageBoxResult.No)
                {
                    return;
                }
                if (newSite)
                {
                    MainWindow.RemoveTab(this.Name);
                    return;
                }
                else if (isModified)
                {
                    foreman.ClearUpdates();
                    SuperVisor1.ClearUpdates();
                    SuperVisor2.ClearUpdates();
                    SupplyAuth.ClearUpdates();
                    PopulateAllFields();
                    PopulateSiteContactFields();
                }
                lockFields();
                TabIsGainingFocus();
            }
        }
        /// <summary>
        /// This runs the thread to load lots assocaited with this Site.
        /// </summary>
        private void loadLots()
        {
            if (cmdSaveEdit.Content.Equals(UnlockBtnTxt))
            {
                Thread newLotLoadThread = new Thread(threadLoadLotData);
                newLotLoadThread.Start();
            }
        }
        /// <summary>
        /// Thread entry point to load data from database.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void threadLoadLotData()
        {
            try
            {
                DataSet data = db.Select("*", Lot.Table, Lot.Fields.assocID.ToString() + " = '" + mSite.GetSiteID() + "'",Lot.Fields.LotNumber.ToString());
                Collection<LotBinding> gridData = data.getBindableCollection<LotBinding>();
                DispatcherOperation dataOp = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Collection<LotBinding>>(setLotData), gridData);
                DispatcherOperationStatus status = dataOp.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = dataOp.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
                dataGridViewData = data;
                dataGridViewData.BuildPrimaryKeyIndex(Lot.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Lots - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            
            }
        }
        /// <summary>
        /// Call back method to set database data to gridview
        /// </summary>
        /// <param name="gridData"></param>
        private void setLotData(Collection<LotBinding> gridData)
        {
            lotGridView.ItemsSource = gridData;
        }
        /// <summary>
        /// Handles the event when a lot is double clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lotGridView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                try
                {
                    if (lotGridView.SelectedCells.Count > 0)
                    {
                        LotBinding obj = (LotBinding)lotGridView.SelectedCells[0].Item;
                        dataGridViewData.SeekToPrimaryKey(obj.lotID);
                        Lot lotObj = new Lot(dataGridViewData.GetRecordDataSet());
                        MainWindow.OpenTab(new LotView(lotObj));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Loading Clients - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Handles the event when the columns are generated so they can be formatted properly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lotGridView_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotBinding.getdisplayTextMap();
            foreach (DataGridColumn column in lotGridView.Columns)
            {
                int index = lotGridView.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    lotGridView.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        lotGridView.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void cmboContactSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                if (selectedSiteContact != cmboContactSelection.SelectedIndex)
                {
                    if (cmdSaveEdit.Content.Equals(SaveBtnTxt))
                    {
                        SiteContact oldContact = GetReferencedSiteContact(selectedSiteContact);
                        if (txtContactName.Text.Length > 0)
                        {
                            int code = oldContact.SetName(txtContactName.Text.ToUpper());
                            if (code > 0)
                            {
                                MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Name' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        else if (!newSite)
                        {
                            oldContact.ClearField(SiteContact.Fields.Name.ToString());
                            oldContact.ClearFieldUpdate(SiteContact.Fields.Name);
                        }
                        if (txtContactPhone.PhoneNumber.Length > 0)
                        {
                            int code = oldContact.SetPhone(txtContactPhone.PhoneNumber);
                            if (code > 0)
                            {
                                MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Phone' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        else if (!newSite)
                        {
                            oldContact.ClearField(SiteContact.Fields.Phone.ToString());
                            oldContact.ClearFieldUpdate(SiteContact.Fields.Phone);
                        }
                        if (txtContactEmail.Text.Length > 0)
                        {
                            int code = oldContact.SetEmail(txtContactEmail.Text.ToLower());
                            if (code > 0)
                            {
                                MessageBox.Show("Field '" + oldContact.GetContactType().ToString() + " Email' - " + msgCodes.GetString("M" + code), "ERROR - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        else if (!newSite)
                        {
                            oldContact.ClearField(SiteContact.Fields.Email.ToString());
                            oldContact.ClearFieldUpdate(SiteContact.Fields.Email);
                        }
                    }
                    PopulateSiteContactFields();
                    if (cmdSaveEdit.Content.Equals(UnlockBtnTxt))
                    {
                        isModified = false;
                    }
                    selectedSiteContact = cmboContactSelection.SelectedIndex;
                }
            }
        }
        
        private void PopulateSiteContactFields()
        {
            SiteContact selectedContact = GetReferencedSiteContact(cmboContactSelection.SelectedIndex);
            txtContactEmail.Text = selectedContact.GetEmail();
            txtContactName.Text = selectedContact.GetName();
            txtContactPhone.PhoneNumber = selectedContact.GetPhone();
        }
        
        private SiteContact GetReferencedSiteContact(int num)
        {
            switch (num)
            {
                case 0:
                    return foreman;
                case 1:
                    return SuperVisor1;
                case 2:
                    return SuperVisor2;
                default:
                    return SupplyAuth;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateSiteContactFields();
            if (cmdSaveEdit.Content.Equals(UnlockBtnTxt))
            {
                isModified = false;
            }
        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            MessageBoxResult res;
            if (isModified && newSite)
            {
                res = MessageBox.Show("Cancel new Site  - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else if (isModified)
            {
                res = MessageBox.Show("Cancel Site Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else
            {
                res = MessageBoxResult.Yes;
            }
            if (res == MessageBoxResult.No)
            {
                return false;
            }
            return true;
        }

        public bool TabIsLosingFocus()
        {
           dataGridViewData = null;
                lotGridView.ItemsSource = null;
            return true;
        }

        public void TabIsGainingFocus()
        {
                            loadLots();
                if (newSite)
                {
                    MainWindow.setActionList(getNewSiteActionMenu());
                }
                else
                {
                    MainWindow.setActionList(getExistingSiteActionMenu());
                };
        }

        public string TabTitle()
        {
            if(newSite)
            {
                return "New Site";
            }
            return mSite.GetSiteName();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["symbol-site"];
        }

        #endregion
    }
}