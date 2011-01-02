using System;
using System.Collections;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ClearChoice;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.UIControls;
using Clear_Choice.Windows;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for LotView.xaml
    /// </summary>
    public partial class LotView : UserControl, ISTabView
    {
        private bool modified = true;
        private bool isNew = true;
        private Lot mLot;
        private String saveBtnTxt = "Save Changes";
        private String unlockBtnTxt = "Unlock Form";
        private LotService mRoughIn = null;
        private LotService mService = null;
        private LotService mFinal = null;
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private float[] totalPortions = new float[3];

        public LotView(Client client)
        {
            //This represents if a Client is directly creating a Lot Obj
            InitializeComponent();
            try
            {
                mLot = new Lot(db);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lot Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            unlockForm();
            this.Name = "NewLotView";
            mLot.SetAssociationID(client.GetClientID());
            mLot.SetRoughInValue(client.GetRoughInValue());
            mLot.SetServiceValue(client.GetServiceValue());
            mLot.SetFinalValue(client.GetFinalValue());

            totalPortions[0] = client.GetRoughInValue();
            totalPortions[1] = client.GetServiceValue();
            totalPortions[2] = client.GetFinalValue();
        }
        public LotView(Site site)
        {
            //This represents if a new Lot is being created for a Site Object
            InitializeComponent();
            try
            {
                mLot = new Lot(db);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lot Object - " + msgCodes.GetString("M2101") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            txtAddress.Text = site.GetAddress();
            txtCity.Text = site.GetCity();
            txtServiceSize.Text = ((site.GetServiceSize() == 0) ? "" : "" + site.GetServiceSize());
            unlockForm();
            this.Name = "NewLotView";
            mLot.SetAssociationID(site.GetSiteID());
            DataSet data = db.Select("*", Client.Table, Client.Fields.clientID.ToString() + " = '" + site.GetClientID() + "'");
            data.Read();
            Client client = new Client(data.GetRecordDataSet());
            mLot.SetRoughInValue(client.GetRoughInValue());
            mLot.SetServiceValue(client.GetServiceValue());
            mLot.SetFinalValue(client.GetFinalValue());

            totalPortions[0] = client.GetRoughInValue();
            totalPortions[1] = client.GetServiceValue();
            totalPortions[2] = client.GetFinalValue();
        }
        public LotView(Lot lot)
        {
            //This represents if the lot is being provided
            InitializeComponent();
            mLot = lot;
            this.Name = "LotView" + mLot.GetLotID();
            lockForm();
            populateFields();
            isNew = false;

            totalPortions[0] = mLot.GetRoughInValue();
            totalPortions[1] = mLot.GetServiceValue();
            totalPortions[2] = mLot.GetFinalValue();

        }

        private void unlockForm()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtAddress);
            boxes.Add(txtBlockNumber);
            boxes.Add(txtCity);
            boxes.Add(txtFinalNotes);
            boxes.Add(txtHoodColour);
            boxes.Add(txtLotNumber);
            boxes.Add(txtNotes);
            boxes.Add(txtPermitNumber);
            boxes.Add(txtPlanInfo);
            boxes.Add(txtRoughNotes);
            boxes.Add(txtServiceNotes);
            boxes.Add(txtServiceSize);
            boxes.Add(txtSPColour);
            boxes.Add(txtSPType);
            boxes.Add(txtType);
            boxes.Add(txtLotSize);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = false;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            txtJobTotal.IsReadOnly = false;
            txtJobTotal.Foreground = foreGround;
            txtJobTotal.Background = backGround;

            cmboCompleted.IsEnabled = true;

            dpClosedDate.IsReadOnly = false;
            dpFinalBilled.IsReadOnly = false;
            dpFinalCalledIn.IsReadOnly = false;
            dpFinalPassed.IsReadOnly = false;
            dpJobBC.IsReadOnly = false;
            dpPermitDate.IsReadOnly = false;
            dpRoughBilled.IsReadOnly = false;
            dpRoughCalledIn.IsReadOnly = false;
            dpRoughPassed.IsReadOnly = false;
            dpServiceBilled.IsReadOnly = false;
            dpServiceCalledIn.IsReadOnly = false;
            dpServicePassed.IsReadOnly = false;

            cmdCancel.IsEnabled = true;
            cmdSaveEdit.IsEnabled = false;
            cmdSaveEdit.Content = saveBtnTxt;
            modified = false;
        }

        private void lockForm()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtAddress);
            boxes.Add(txtBlockNumber);
            boxes.Add(txtCity);
            boxes.Add(txtFinalNotes);
            boxes.Add(txtHoodColour);
            boxes.Add(txtLotNumber);
            boxes.Add(txtNotes);
            boxes.Add(txtPermitNumber);
            boxes.Add(txtPlanInfo);
            boxes.Add(txtRoughNotes);
            boxes.Add(txtServiceNotes);
            boxes.Add(txtServiceSize);
            boxes.Add(txtSPColour);
            boxes.Add(txtSPType);
            boxes.Add(txtType);
            boxes.Add(txtLotSize);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = true;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            txtRoughAmount.IsReadOnly = true;
            txtRoughAmount.Foreground = foreGround;
            txtRoughAmount.Background = backGround;

            txtServiceAmount.IsReadOnly = true;
            txtServiceAmount.Foreground = foreGround;
            txtServiceAmount.Background = backGround;

            txtFinalAmount.IsReadOnly = true;
            txtFinalAmount.Foreground = foreGround;
            txtFinalAmount.Background = backGround;

            txtJobTotal.IsReadOnly = true;
            txtJobTotal.Foreground = foreGround;
            txtJobTotal.Background = backGround;

            cmboCompleted.IsEnabled = false;

            dpClosedDate.IsReadOnly = true;
            dpFinalBilled.IsReadOnly = true;
            dpFinalCalledIn.IsReadOnly = true;
            dpFinalPassed.IsReadOnly = true;
            dpJobBC.IsReadOnly = true;
            dpPermitDate.IsReadOnly = true;
            dpRoughBilled.IsReadOnly = true;
            dpRoughCalledIn.IsReadOnly = true;
            dpRoughPassed.IsReadOnly = true;
            dpServiceBilled.IsReadOnly = true;
            dpServiceCalledIn.IsReadOnly = true;
            dpServicePassed.IsReadOnly = true;

            cmdCancel.IsEnabled = false;
            cmdSaveEdit.IsEnabled = true;
            cmdSaveEdit.Content = unlockBtnTxt;

        }

        private void populateFields()
        {
            txtAddress.Text = mLot.GetAddress();
            txtCity.Text = mLot.GetCity();
            txtHoodColour.Text = mLot.GetHoodColour();
            txtNotes.Text = mLot.GetNotes();
            txtPlanInfo.Text = mLot.GetPlanInfo();
            txtSPColour.Text = mLot.GetSPColour();
            txtSPType.Text = mLot.GetSPType();
            txtType.Text = mLot.GetLotType();
            txtJobTotal.Amount = mLot.GetJobTotal();

            txtBlockNumber.Text = ((mLot.GetBlockNumber() == 0) ? "" : "" + mLot.GetBlockNumber());
            txtServiceSize.Text = ((mLot.GetServiceSize() == 0) ? "" : "" + mLot.GetServiceSize());
            txtPermitNumber.Text = ((mLot.GetPermitNumber() == 0) ? "" : "" + mLot.GetPermitNumber());
            txtLotNumber.Text = ((mLot.GetLotNumber() == 0) ? "" : "" + mLot.GetLotNumber());
            txtLotSize.Text = ((mLot.GetLotSize() == 0) ? "" : "" + mLot.GetLotSize());

            dpClosedDate.Text = ((mLot.GetClosedDate().Equals(DateTime.MinValue)) ? "" : mLot.GetClosedDate().ToShortDateString());
            dpJobBC.Text = ((mLot.getJobBC().Equals(DateTime.MinValue)) ? "" : mLot.getJobBC().ToShortDateString());
            dpPermitDate.Text = ((mLot.GetPermitDate().Equals(DateTime.MinValue)) ? "" : mLot.GetPermitDate().ToShortDateString());

            cmboCompleted.SelectedIndex = mLot.IsCompleted() ? 1 : 0;

            //Load the Services
            //Rough In
            LotService service = mLot.GetLotService(db, Lot.LotServices.RoughIn);
            if (service != null)
            {
                mRoughIn = service;
                dpRoughCalledIn.Text = ((service.GetCalledIn().Equals(DateTime.MinValue)) ? "" : service.GetCalledIn().ToShortDateString());
                dpRoughPassed.Text = ((service.GetPassed().Equals(DateTime.MinValue)) ? "" : service.GetPassed().ToShortDateString());
                dpRoughBilled.Text = ((service.GetBilled().Equals(DateTime.MinValue)) ? "" : service.GetBilled().ToShortDateString());
                txtRoughAmount.Amount = service.GetAmount();
                txtRoughNotes.Text = service.GetNotes();
            }
            //Service
            service = mLot.GetLotService(db, Lot.LotServices.Service);
            if (service != null)
            {
                mService = service;
                dpServiceCalledIn.Text = ((service.GetCalledIn().Equals(DateTime.MinValue)) ? "" : service.GetCalledIn().ToShortDateString());
                dpServicePassed.Text = ((service.GetPassed().Equals(DateTime.MinValue)) ? "" : service.GetPassed().ToShortDateString());
                dpServiceBilled.Text = ((service.GetBilled().Equals(DateTime.MinValue)) ? "" : service.GetBilled().ToShortDateString());
                txtServiceAmount.Amount = service.GetAmount();
                txtServiceNotes.Text = service.GetNotes();
            }
            //Service
            service = mLot.GetLotService(db, Lot.LotServices.Final);
            if (service != null)
            {
                mFinal = service;
                dpFinalCalledIn.Text = ((service.GetCalledIn().Equals(DateTime.MinValue)) ? "" : service.GetCalledIn().ToShortDateString());
                dpFinalPassed.Text = ((service.GetPassed().Equals(DateTime.MinValue)) ? "" : service.GetPassed().ToShortDateString());
                dpFinalBilled.Text = ((service.GetBilled().Equals(DateTime.MinValue)) ? "" : service.GetBilled().ToShortDateString());
                txtFinalAmount.Amount = service.GetAmount();
                txtFinalNotes.Text = service.GetNotes();
            }
        }

        private void txtFieldChanged(object sender, TextChangedEventArgs e)
        {
            if (!modified)
            {
                modified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }
            if (sender.Equals(txtJobTotal))
            {
                txtRoughAmount.Amount = txtJobTotal.Amount * totalPortions[0];
                txtServiceAmount.Amount = txtJobTotal.Amount * totalPortions[1];
                txtFinalAmount.Amount = txtJobTotal.Amount * totalPortions[2];
            }
        }

        private void lnkGMaps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            String address = mLot.GetAddress().Replace(" ", "+");
            String city = mLot.GetCity().Replace(" ", "+");
            proc.StartInfo.FileName = "http://maps.google.ca/maps?f=q&source=s_q&hl=en&geocode=&q=" + address + "," + city + ",+Ontario";
            proc.Start();
        }

        private ArrayList getExistingLotActions()
        {
            ArrayList actions = new ArrayList();
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                if (modified)
                {
                    IconButton savelotBtn = new IconButton();
                    savelotBtn.Text = "Save Changes";
                    savelotBtn.Source = (Image)App.iconSet["symbol-save"];
                    savelotBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(savelotBtn);

                    IconButton cancelLotChangesBtn = new IconButton();
                    cancelLotChangesBtn.Text = "Cancel Changes";
                    cancelLotChangesBtn.Source = (Image)App.iconSet["symbol-delete"];
                    cancelLotChangesBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(cancelLotChangesBtn);
                }
                else
                {
                    IconButton lockFormBtn = new IconButton();
                    lockFormBtn.Text = "Lock Form";
                    lockFormBtn.Source = (Image)App.iconSet["symbol-lock"];
                    lockFormBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(lockFormBtn);
                }
                IconButton setPriceOptionsBtn = new IconButton();
                setPriceOptionsBtn.Text = "Set Price Portions";
                setPriceOptionsBtn.Source = (Image)App.iconSet["control-panel"];
                setPriceOptionsBtn.MouseDown += new MouseButtonEventHandler(setPriceOptionsBtn_MouseDown);
                actions.Add(setPriceOptionsBtn);

            }
            else
            {
                IconButton unlockFormBtn = new IconButton();
                unlockFormBtn.Text = "Unlock Form";
                unlockFormBtn.Source = (Image)App.iconSet["symbol-unlock"];
                unlockFormBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(unlockFormBtn);

                IconButton gmapsBtn = new IconButton();
                gmapsBtn.Text = "View Address";
                gmapsBtn.Source = (Image)App.iconSet["symbol-gmaps"];
                gmapsBtn.MouseDown += new MouseButtonEventHandler(lnkGMaps_MouseDown);
                actions.Add(gmapsBtn);

                IconButton viewExtrasBtn = new IconButton();
                viewExtrasBtn.Text = "View Extras";
                viewExtrasBtn.Source = (Image)App.iconSet["symbol-extras"];
                viewExtrasBtn.MouseDown += new MouseButtonEventHandler(viewExtrasBtn_MouseDown);
                actions.Add(viewExtrasBtn);

                IconButton viewRepairsBtn = new IconButton();
                viewRepairsBtn.Text = "View Repairs";
                viewRepairsBtn.Source = (Image)App.iconSet["symbol-repair"];
                viewRepairsBtn.MouseDown += new MouseButtonEventHandler(viewRepairsBtn_MouseDown);
                actions.Add(viewRepairsBtn);

                IconButton viewTimeSheetsBtn = new IconButton();
                viewTimeSheetsBtn.Text = "View Time Sheets";
                viewTimeSheetsBtn.Source = (Image)App.iconSet["symbol-timesheets"];
                viewTimeSheetsBtn.MouseDown += new MouseButtonEventHandler(viewTimeSheetsBtn_MouseDown);
                actions.Add(viewTimeSheetsBtn);


                IconButton viewSummaryBtn = new IconButton();
                viewSummaryBtn.Text = "View Lot Summary";
                viewSummaryBtn.Source = (Image)App.iconSet["reports-icon"];
                viewSummaryBtn.MouseDown += new MouseButtonEventHandler(viewSummaryBtn_MouseDown);
                actions.Add(viewSummaryBtn);
            }
            return actions;
        }

        private void setPriceOptionsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPricePortions pricePortions = new SetPricePortions(totalPortions);
            pricePortions.ValuesModified += new RoutedEventHandler(pricePortions_ValuesModified);
            pricePortions.ShowDialog();
        }

        private void pricePortions_ValuesModified(object sender, RoutedEventArgs e)
        {
            totalPortions = ((SetPricePortions)sender).Values;
            txtRoughAmount.Amount = txtJobTotal.Amount * totalPortions[0];
            txtServiceAmount.Amount = txtJobTotal.Amount * totalPortions[1];
            txtFinalAmount.Amount = txtJobTotal.Amount * totalPortions[2];

            mLot.SetRoughInValue(totalPortions[0]);
            mLot.SetServiceValue(totalPortions[1]);
            mLot.SetFinalValue(totalPortions[2]);
        }

        void viewSummaryBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotSummaryView(mLot));
        }

        private void viewTimeSheetsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new TimeSheetView(mLot));
        }

        private void viewRepairsBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotRepairsView(mLot));
        }

        private void viewExtrasBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotExtraView(mLot));
        }

        private ArrayList getNewLotActions()
        {
            ArrayList actions = new ArrayList();

            if (modified)
            {
                IconButton saveNewSiteBtn = new IconButton();
                saveNewSiteBtn.Text = "Save New Lot";
                saveNewSiteBtn.Source = (Image)App.iconSet["symbol-save"];
                saveNewSiteBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(saveNewSiteBtn);
            }
            IconButton cancelNewClientBtn = new IconButton();
            cancelNewClientBtn.Text = "Cancel New Lot";
            cancelNewClientBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewClientBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewClientBtn);
            return actions;
        }

        private void dateSelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!modified)
            {
                modified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }
        }

        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                MessageBoxResult res;
                if (isNew)
                {
                    res = MessageBox.Show("You are about to add a new lot to the system. Are you sure you want to continue?", "Adding a New Lot", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                }
                else
                {
                    res = MessageBox.Show("You are about to save modifications to this lot to the system. Are you sure you want to continue?", "Lot Modifications", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                }
                if (res == MessageBoxResult.No)
                {
                    return;
                }
                if (SaveChanges())
                {
                    if (isLotUnique())
                    {
                        try
                        {
                            db.BeginTransaction();
                            mLot.SaveObject(db);
                            mRoughIn.SaveObject(db);
                            mService.SaveObject(db);
                            mFinal.SaveObject(db);
                            db.CommitTransaction();
                            lockForm();
                            populateFields();
                            String oldName = this.Name;
                            isNew = false;
                            this.Name = "LotView" + mLot.GetLotID();
                            MainWindow.UpdateTabTitle(oldName, mLot.LotDisplayName(), this.Name);

                        }
                        catch (Exception ex)
                        {
                            db.RollbackTransaction();
                            MessageBox.Show("Database error. Please contact Administrator. Message: " + ex.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            //Log event
                        }
                    }
                    else
                    {
                        MessageBox.Show("This lot already exists. Please make sure that either Display Name, Address, City, or Lot Number are different.", "Lot Possibly Exists", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            else
            {
                unlockForm();
            }
            TabIsGainingFocus();
        }

        private bool isLotUnique()
        {
            try
            {
                StringBuilder where = new StringBuilder();
                where.Append(Lot.Fields.assocID.ToString() + " = '" + mLot.GetAssociationID() + "'");
                where.Append(" AND " + Lot.Fields.LotNumber.ToString() + " = '" + txtLotNumber.Text + "'");
                if (!isNew)
                {
                    where.Append(" AND " + Lot.Fields.lotID.ToString() + " != '" + mLot.GetLotID() + "'");
                }
                if (db.Select(Lot.PrimaryKey, Lot.Table, where.ToString()).NumberOfRows() > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checking Lot Uniqueness - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool SaveChanges()
        {
            if (txtLotNumber.Text.Length == 0)
            {
                MessageBox.Show("Field 'Lot Number' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                try
                {
                    mLot.SetLotNumber(Int32.Parse(txtLotNumber.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Lot Number' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            if (txtAddress.Text.Length > 0)
            {
                int code = mLot.SetAddress(txtAddress.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Address' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.Address.ToString());
            }
            if (txtCity.Text.Length > 0)
            {
                int code = mLot.SetCity(txtCity.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'City' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.City.ToString());
            }
            if (txtPlanInfo.Text.Length > 0)
            {
                int code = mLot.SetPlanInfo(txtPlanInfo.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Plan Info' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.PlanInfo.ToString());
            }
            if (txtHoodColour.Text.Length > 0)
            {
                int code = mLot.SetHoodColour(txtHoodColour.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Hood Colour' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.HoodColour.ToString());
            }
            if (txtType.Text.Length > 0)
            {
                int code = mLot.SetLotType(txtType.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Type' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.Type.ToString());
            }
            if (txtSPColour.Text.Length > 0)
            {
                int code = mLot.SetSPColour(txtSPColour.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'SP Colour' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.SPColour.ToString());
            }
            if (txtSPType.Text.Length > 0)
            {
                int code = mLot.SetSPType(txtSPType.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'SP Type' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.SPType.ToString());
            }

            mLot.SetCompleted(cmboCompleted.SelectedIndex == 1);
            if (txtNotes.Text.Length > 0)
            {
                mLot.SetNotes(txtNotes.Text);
            }
            if (!SaveNumberChanges())
            {
                return false;
            }
            if (!SaveDateChanges())
            {
                return false;
            }
            if (!SaveServiceChanges())
            {
                return false;
            }
            return true;
        }

        private bool SaveNumberChanges()
        {
            if (txtLotSize.Text.Length > 0)
            {
                try
                {
                    mLot.SetLotSize(Int32.Parse(txtLotSize.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Square Feet' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.LotSize.ToString());
            }
            if (txtPermitNumber.Text.Length > 0)
            {
                try
                {
                    mLot.SetPermitNumber(Int32.Parse(txtPermitNumber.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Permit Number' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.PermitNumber.ToString());
            }
            if (txtServiceSize.Text.Length > 0)
            {
                try
                {
                    mLot.SetServiceSize(Int32.Parse(txtServiceSize.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Service Size' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.ServiceSize.ToString());
            }
            if (txtBlockNumber.Text.Length > 0)
            {
                try
                {
                    mLot.SetBlockNumber(Int32.Parse(txtBlockNumber.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Field 'Block' - " + msgCodes.GetString("M1108"), "Error - 1108", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.BlockNumber.ToString());
            }
            if (txtJobTotal.Amount > 0)
            {
                mLot.SetJobTotal(txtJobTotal.Amount);
            }
            else if (!isNew)
            {
                mLot.ClearField(Lot.Fields.JobTotal.ToString());
            }
            return true;
        }

        private bool SaveDateChanges()
        {
            if (dpPermitDate.Text.Length > 0)
            {
                mLot.SetPermitDate(dpPermitDate.SelectedDate);
            }
            if (dpClosedDate.Text.Length > 0)
            {
                mLot.SetClosedDate(dpClosedDate.SelectedDate);
            }
            if (dpJobBC.Text.Length > 0)
            {
                mLot.setJobBC(dpJobBC.SelectedDate);
            }
            return true;
        }

        private bool SaveServiceChanges()
        {
            //RoughIn
            if (mRoughIn == null)
            {
                mRoughIn = new LotService(db, Lot.LotServices.RoughIn, mLot.GetLotID());
            }
            if (dpRoughCalledIn.Text.Length > 0)
            {
                mRoughIn.SetCalledIn(dpRoughCalledIn.SelectedDate);
            }
            else if (!isNew)
            {
                mRoughIn.ClearField(LotService.Fields.CalledIn.ToString());
            }
            if (dpRoughPassed.Text.Length > 0)
            {
                mRoughIn.SetPassed(dpRoughPassed.SelectedDate);
            }
            else if (!isNew)
            {
                mRoughIn.ClearField(LotService.Fields.Passed.ToString());
            }
            if (dpRoughBilled.Text.Length > 0)
            {
                mRoughIn.SetBilled(dpRoughBilled.SelectedDate);
            }
            else if (!isNew)
            {
                mRoughIn.ClearField(LotService.Fields.Billed.ToString());
            }
            if (txtRoughAmount.Amount > 0)
            {
                try
                {
                    mRoughIn.SetAmount(txtRoughAmount.Amount);
                }
                catch (Exception)
                {
                    MessageBox.Show("Rough In Amount is formatted incorrectly. Please review. Numbers with a single decimal only.", "Formatting Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mRoughIn.ClearField(LotService.Fields.Amount.ToString());
            }
            if (txtRoughNotes.Text.Length > 0)
            {
                mRoughIn.SetNotes(txtRoughNotes.Text);
            }
            else if (!isNew)
            {
                mRoughIn.ClearField(LotService.Fields.Notes.ToString());
            }
            //Service
            if (mService == null)
            {
                mService = new LotService(db, Lot.LotServices.Service, mLot.GetLotID());
            }
            if (dpServiceCalledIn.Text.Length > 0)
            {
                mService.SetCalledIn(dpServiceCalledIn.SelectedDate);
            }
            else if (!isNew)
            {
                mService.ClearField(LotService.Fields.CalledIn.ToString());
            }
            if (dpServicePassed.Text.Length > 0)
            {
                mService.SetPassed(dpServicePassed.SelectedDate);
            }
            else if (!isNew)
            {
                mService.ClearField(LotService.Fields.Passed.ToString());
            }
            if (dpServiceBilled.Text.Length > 0)
            {
                mService.SetBilled(dpServiceBilled.SelectedDate);
            }
            else if (!isNew)
            {
                mService.ClearField(LotService.Fields.Billed.ToString());
            }
            if (txtServiceAmount.Amount > 0)
            {
                try
                {
                    mService.SetAmount(txtServiceAmount.Amount);
                }
                catch (Exception)
                {
                    MessageBox.Show("Service Amount is formatted incorrectly. Please review. Numbers with a single decimal only.", "Formatting Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mService.ClearField(LotService.Fields.Amount.ToString());
            }
            if (txtServiceNotes.Text.Length > 0)
            {
                mService.SetNotes(txtServiceNotes.Text);
            }
            else if (!isNew)
            {
                mService.ClearField(LotService.Fields.Notes.ToString());
            }
            //Final
            if (mFinal == null)
            {
                mFinal = new LotService(db, Lot.LotServices.Final, mLot.GetLotID());
            }
            if (dpFinalCalledIn.Text.Length > 0)
            {
                mFinal.SetCalledIn(dpFinalCalledIn.SelectedDate);
            }
            else if (!isNew)
            {
                mFinal.ClearField(LotService.Fields.CalledIn.ToString());
            }
            if (dpFinalPassed.Text.Length > 0)
            {
                mFinal.SetPassed(dpFinalPassed.SelectedDate);
            }
            else if (!isNew)
            {
                mFinal.ClearField(LotService.Fields.Passed.ToString());
            }
            if (dpFinalBilled.Text.Length > 0)
            {
                mFinal.SetBilled(dpFinalBilled.SelectedDate);
            }
            else if (!isNew)
            {
                mFinal.ClearField(LotService.Fields.Billed.ToString());
            }
            if (txtFinalAmount.Amount > 0)
            {
                try
                {
                    mFinal.SetAmount(txtFinalAmount.Amount);
                }
                catch (Exception)
                {
                    MessageBox.Show("Service Amount is formatted incorrectly. Please review. Numbers with a single decimal only.", "Formatting Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mFinal.ClearField(LotService.Fields.Amount.ToString());
            }
            if (txtFinalNotes.Text.Length > 0)
            {
                mFinal.SetNotes(txtFinalNotes.Text);
            }
            else if (!isNew)
            {
                mFinal.ClearField(LotService.Fields.Notes.ToString());
            }
            return true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;
            if (modified && isNew)
            {
                res = MessageBox.Show("Cancel new Lot  - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else if (modified)
            {
                res = MessageBox.Show("Cancel Lot Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else
            {
                res = MessageBoxResult.Yes;
            }
            if (res == MessageBoxResult.No)
            {
                return;
            }
            if (isNew)
            {
                MainWindow.RemoveTab(this.Name);
                return;
            }
            else if (modified)
            {
                populateFields();
            }
            lockForm();
            TabIsGainingFocus();
        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            MessageBoxResult res;
            if (modified && isNew && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                res = MessageBox.Show("Cancel new Lot  - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            }
            else if (modified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                res = MessageBox.Show("Cancel Lot Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
            return true;
        }

        public void TabIsGainingFocus()
        {
            if (isNew)
            {
                MainWindow.setActionList(getNewLotActions());
            }
            else
            {
                MainWindow.setActionList(getExistingLotActions());
            }
        }

        public string TabTitle()
        {
            if (isNew)
            {
                return "New Lot";
            }
            return mLot.LotDisplayName();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["home"];
        }

        #endregion
    }
}
