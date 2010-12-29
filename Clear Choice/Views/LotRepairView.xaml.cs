using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
    /// Interaction logic for LotRepairView.xaml
    /// </summary>
    public partial class LotRepairView : UserControl, ISTabView
    {
        private String unlockBtnTxt = "Unlock Form";
        private String saveBtnTxt = "Save Changes";

        private Database db = Database.Instance;
        private DataSet dataGridData;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        private bool isModified = true;
        private bool isNew = false;

        private Lot mLot;
        private LotRepair mRepair;
        private LotRepairAction mSelectedAction;


        public LotRepairView(Lot pLot)
        {
            InitializeComponent();
            txtLotNumber.Text = "" + pLot.GetLotNumber();
            txtAddress.Text = pLot.GetAddress();
            txtModel.Text = pLot.GetLotType();

            mLot = pLot;

            cmdSaveEdit.IsEnabled = false;
            cmdSaveEdit.Content = saveBtnTxt;
            isNew = true;
            this.Name = "LotRepairViewNewRepair";
            try
            {
                mRepair = new LotRepair(pLot.GetLotID());
                db.BeginTransaction();
            }
            catch (Exception ex)
            {
                LockFields();
                cmdSaveEdit.IsEnabled = false;
                MessageBox.Show("Loading New Lot Repair - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            isModified = false;
            LoadRepairActions();

        }

        public LotRepairView(LotRepair repair, Lot pLot)
        {
            InitializeComponent();
            txtLotNumber.Text = "" + pLot.GetLotNumber();
            txtAddress.Text = pLot.GetAddress();
            txtModel.Text = pLot.GetLotType();

            mLot = pLot;
            mRepair = repair;

            this.Name = "LotRepairView" + mRepair.GetRepairID();
            LockFields();
            PopulateFields();
            isModified = false;
            cmdSaveEdit.IsEnabled = true;
            cmdCancel.IsEnabled = false;
            cmdSaveEdit.Content = unlockBtnTxt;
            LoadRepairActions();
        }

        private void PopulateFields()
        {
            txtAltPhone.PhoneNumber = mRepair.GetAltNumber();
            txtEmail.Text = mRepair.GetEmail();
            txtName.Text = mRepair.GetOwnerName();
            txtNotes.Text = mRepair.GetNotes();
            txtPhone.PhoneNumber = mRepair.GetHomeNumber();
            txtRequested.Text = mRepair.GetRequestedBy();
            txtSource.Text = mRepair.GetSourceCode();
            txtWindow.Text = mRepair.GetWindowOfAppointment();
            txtWorkOrder.Text = mRepair.GetWorkOrder();

            dpDate.Text = ((mRepair.GetDateOfAppointment().Equals(DateTime.MinValue)) ? "" : mRepair.GetDateOfAppointment().ToShortDateString());
            dpInspection.Text = ((mRepair.GetInspectionPassed().Equals(DateTime.MinValue)) ? "" : mRepair.GetInspectionPassed().ToShortDateString());

        }

        private void UnlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtEmail);
            boxes.Add(txtName);
            boxes.Add(txtNotes);
            boxes.Add(txtRequested);
            boxes.Add(txtSource);
            boxes.Add(txtWindow);
            boxes.Add(txtWorkOrder);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = true;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            txtPhone.IsReadOnly = false;
            txtPhone.Foreground = foreGround;
            txtPhone.Background = backGround;

            txtAltPhone.IsReadOnly = false;
            txtAltPhone.Foreground = foreGround;
            txtAltPhone.Background = backGround;

            dpDate.IsReadOnly = false;
            dpInspection.IsReadOnly = false;
            isModified = false;

        }

        private void LockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));
            ArrayList boxes = new ArrayList();

            boxes.Add(txtEmail);
            boxes.Add(txtName);
            boxes.Add(txtNotes);
            boxes.Add(txtRequested);
            boxes.Add(txtSource);
            boxes.Add(txtWindow);
            boxes.Add(txtWorkOrder);

            foreach (TextBox box in boxes)
            {
                box.IsReadOnly = true;
                box.Foreground = foreGround;
                box.Background = backGround;
            }

            txtPhone.IsReadOnly = true;
            txtPhone.Foreground = foreGround;
            txtPhone.Background = backGround;

            txtAltPhone.IsReadOnly = true;
            txtAltPhone.Foreground = foreGround;
            txtAltPhone.Background = backGround;

            dpDate.IsReadOnly = true;
            dpInspection.IsReadOnly = true;
        }

        private void LoadRepairActions()
        {
            try
            {
                DataSet data = db.Select("*", LotRepairAction.Table, LotRepairAction.Fields.repairID.ToString() + " = '" + mRepair.GetRepairID() + "'");
                data.BuildPrimaryKeyIndex(LotRepairAction.Fields.actionID.ToString());
                Collection<LotRepairActionBinding> gridData = data.getBindableCollection<LotRepairActionBinding>();
                dgRepairActions.ItemsSource = gridData;
                dataGridData = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Repair Actions - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FieldTextChanged(object sender, TextChangedEventArgs e)
        {

            if (!isModified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
                this.TabIsGainingFocus();
            }
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isModified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
                this.TabIsGainingFocus();
            }
        }

        private bool SaveChanges()
        {
            if (txtWorkOrder.Text.Length > 0)
            {
                int code = mRepair.SetWorkOrder(txtWorkOrder.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Work Order' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Field 'Work Order' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtName.Text.Length > 0)
            {
                int code = mRepair.SetOwnerName(txtName.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Name' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Field 'Name' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtAltPhone.PhoneNumber.Length > 0)
            {
                int code = mRepair.SetAltNumber(txtAltPhone.PhoneNumber);
                if (code > 0)
                {
                    MessageBox.Show("Field 'Alt. Number' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.AltNumber.ToString());
            }
            if (txtEmail.Text.Length > 0)
            {
                int code = mRepair.SetEmail(txtEmail.Text.ToLower());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Email' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.Email.ToString());
            }
            if (txtNotes.Text.Length > 0)
            {
                mRepair.SetNotes(txtNotes.Text);

            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.Notes.ToString());
            }
            if (txtPhone.PhoneNumber.Length > 0)
            {
                int code = mRepair.SetHomeNumber(txtPhone.PhoneNumber);
                if (code > 0)
                {
                    MessageBox.Show("Field 'Phone' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.HomeNumber.ToString());
            }
            if (txtRequested.Text.Length > 0)
            {
                int code = mRepair.SetRequestedBy(txtRequested.Text.ToUpper());
                if (code > 0)
                {
                    MessageBox.Show("Field 'Requested By' - " + msgCodes.GetString("M" + code), "Error - " + code, MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.RequestedBy.ToString());
            }
            if (txtSource.Text.Length > 0)
            {
                mRepair.SetSourceCode(txtSource.Text.ToUpper());

            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.SourceCode.ToString());
            }
            if (txtWindow.Text.Length > 0)
            {
                mRepair.SetWindowOfAppointment(txtWindow.Text.ToUpper());

            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.WindowOfAppointment.ToString());
            }
            if (!dpDate.SelectedDate.Equals(DateTime.MinValue))
            {
                mRepair.SetDateOfAppointment(dpDate.SelectedDate);
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.DateOfAppointment.ToString());
            }
            if (!dpInspection.SelectedDate.Equals(DateTime.MinValue))
            {
                mRepair.SetInspectionPassed(dpInspection.SelectedDate);
            }
            else if (!isNew)
            {
                mRepair.ClearField(LotRepair.Fields.InspectionPassed.ToString());
            }
            return true;
        }

        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                if (isNew)
                {
                    MessageBoxResult res = MessageBox.Show("Saving New Repair - " + msgCodes.GetString("M3201"), "Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBoxResult res = MessageBox.Show("Saving Repair Modifications - " + msgCodes.GetString("M3202"), "Warning - 3202", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                if (dataGridData.NumberOfRows() < 1)
                {
                    MessageBox.Show("Missing Repair Action(s) - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (SaveChanges())
                    {
                        try
                        {
                            DataSet check;
                            if (isNew)
                            {
                                check = db.Select("*", LotRepair.Table, LotRepair.Fields.WorkOrder.ToString() + " = '" + txtWorkOrder.Text + "'");
                            }
                            else
                            {
                                check = db.Select("*", LotRepair.Table, LotRepair.Fields.repairID.ToString() + " != '" + mRepair.GetRepairID() + "' AND " + LotRepair.Fields.WorkOrder.ToString() + " = '" + txtWorkOrder.Text.ToUpper() + "'");
                            }
                            if (check.NumberOfRows() > 0)
                            {
                                MessageBox.Show("Reapir may already exist - " + msgCodes.GetString("M3203"), "Error - 3203", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            mRepair.SaveObject(db);
                            db.CommitTransaction();
                            PopulateFields();
                            LockFields();
                            isModified = false;
                            isNew = false;
                            cmdSaveEdit.Content = unlockBtnTxt;
                            cmdCancel.IsEnabled = false;
                            String oldName = this.Name;
                            this.Name = "LotRepairView" + mRepair.GetRepairID();
                            MainWindow.UpdateTabTitle(oldName, mRepair.GetWorkOrder(), "LotRepairView" + mRepair.GetRepairID());
                        }
                        catch (Exception ex)
                        {
                            db.RollbackTransaction();
                            MessageBox.Show("Saving Repair - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                UnlockFields();
                isModified = false;
                cmdSaveEdit.IsEnabled = false;
                cmdCancel.IsEnabled = true;
                cmdSaveEdit.Content = saveBtnTxt;
                try
                {
                    db.BeginTransaction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unlocking Repair - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            this.TabIsGainingFocus();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isNew)
            {
                if (isModified)
                {
                    MessageBoxResult res = MessageBox.Show("Adding New Repair - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                db.RollbackTransaction();
                MainWindow.RemoveTab(this.Name);
                return;
            }
            else
            {
                if (isModified)
                {
                    MessageBoxResult res = MessageBox.Show("Modifiying Repair - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }
                db.RollbackTransaction();
                mRepair.ReloadObject();
                LoadRepairActions();
                PopulateFields();
                LockFields();
                cmdSaveEdit.Content = unlockBtnTxt;
                cmdSaveEdit.IsEnabled = true;
                cmdCancel.IsEnabled = false;

            }
            this.TabIsGainingFocus();
        }

        private void lotGridView_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = LotRepairActionBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgRepairActions.Columns)
            {
                int index = dgRepairActions.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgRepairActions.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgRepairActions.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void lotGridView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                if (dgRepairActions.SelectedIndex > -1)
                {
                    LotRepairActionBinding obj = (LotRepairActionBinding)dgRepairActions.SelectedCells[0].Item;
                    dataGridData.SeekToPrimaryKey(obj.actionID);
                    mSelectedAction = new LotRepairAction(dataGridData.GetRecordDataSet());

                    LotRepairActionWindow actionWindow = new LotRepairActionWindow(mSelectedAction, false);
                    actionWindow.ObjectModified += new RoutedEventHandler(actionWindow_ObjectModified);
                    actionWindow.ShowDialog();
                }
            }
        }

        private ArrayList GetExisitingRepairActions()
        {
            ArrayList actions = new ArrayList();
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                if (isModified)
                {
                    IconButton savenewRepairBtn = new IconButton();
                    savenewRepairBtn.Text = "Save Repair Changes";
                    savenewRepairBtn.Source = (Image)App.iconSet["symbol-save"];
                    savenewRepairBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(savenewRepairBtn);
                }
                IconButton addNewRepairActionBtn = new IconButton();
                addNewRepairActionBtn.Text = "Add New Repair Action";
                addNewRepairActionBtn.Source = (Image)App.iconSet["symbol-add"];
                addNewRepairActionBtn.MouseDown += new MouseButtonEventHandler(addNewRepairActionBtn_MouseDown);
                actions.Add(addNewRepairActionBtn);

                if (isModified)
                {

                    IconButton cancelNewRepairBtn = new IconButton();
                    cancelNewRepairBtn.Text = "Cancel Repair Changes";
                    cancelNewRepairBtn.Source = (Image)App.iconSet["symbol-delete"];
                    cancelNewRepairBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(cancelNewRepairBtn);
                }
                else
                {
                    IconButton cancelNewRepairBtn = new IconButton();
                    cancelNewRepairBtn.Text = "Lock Form";
                    cancelNewRepairBtn.Source = (Image)App.iconSet["symbol-lock"];
                    cancelNewRepairBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                    actions.Add(cancelNewRepairBtn);
                }
            }
            else
            {
                IconButton unlockRepairBtn = new IconButton();
                unlockRepairBtn.Text = "Unlock Form";
                unlockRepairBtn.Source = (Image)App.iconSet["symbol-unlock"];
                unlockRepairBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(unlockRepairBtn);
            }

            return actions;
        }

        private ArrayList GetNewRepairActions()
        {
            ArrayList actions = new ArrayList();
            if (isModified)
            {
                IconButton savenewRepairBtn = new IconButton();
                savenewRepairBtn.Text = "Save New Repair";
                savenewRepairBtn.Source = (Image)App.iconSet["symbol-save"];
                savenewRepairBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                actions.Add(savenewRepairBtn);
            }
            IconButton addNewRepairActionBtn = new IconButton();
            addNewRepairActionBtn.Text = "Add New Repair Action";
            addNewRepairActionBtn.Source = (Image)App.iconSet["symbol-add"];
            addNewRepairActionBtn.MouseDown += new MouseButtonEventHandler(addNewRepairActionBtn_MouseDown);
            actions.Add(addNewRepairActionBtn);

            IconButton cancelNewRepairBtn = new IconButton();
            cancelNewRepairBtn.Text = "Cancel New Repair";
            cancelNewRepairBtn.Source = (Image)App.iconSet["symbol-delete"];
            cancelNewRepairBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
            actions.Add(cancelNewRepairBtn);
            return actions;
        }

        private void addNewRepairActionBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mSelectedAction = new LotRepairAction(mRepair.GetRepairID());
                LotRepairActionWindow actionWindow = new LotRepairActionWindow(mSelectedAction, true);
                actionWindow.ObjectModified += new RoutedEventHandler(actionWindow_ObjectModified);
                actionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading New Lot Repair Action - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void actionWindow_ObjectModified(object sender, RoutedEventArgs e)
        {
            try
            {
                mSelectedAction.SaveObject(db);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saving Lot Repair Action - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LoadRepairActions();
            if (!isModified && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
                TabIsGainingFocus();
            }

        }

        #region ISTabView Members

        public bool TabIsClosing()
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                MessageBoxResult res;
                if (isModified && isNew)
                {
                    res = MessageBox.Show("Cancel new Lot Repair - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else if (isModified)
                {
                    res = MessageBox.Show("Cancel Repair Changes - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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
                    MessageBox.Show("Rolling back Repair - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return true;
        }

        public bool TabIsLosingFocus()
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                MessageBoxResult res = MessageBox.Show("Repair not Saved - " + msgCodes.GetString("M3207"), "Error - 3207", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public void TabIsGainingFocus()
        {
            if (isNew)
            {
                MainWindow.setActionList(GetNewRepairActions());
            }
            else
            {
                MainWindow.setActionList(GetExisitingRepairActions());
            }
        }

        public string TabTitle()
        {
            if (isNew)
            {
                return "New Repair";
            }
            return mRepair.GetWorkOrder();
        }

        public Image TabIcon()
        {
            return (Image)App.iconSet["symbol-repair"];
        }

        #endregion
    }
}
