using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Resources;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClearChoice;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using Stemstudios.UIControls;
using System.Windows;
using System.Text;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for TimeSheetView.xaml
    /// </summary>
    public partial class TimeSheetView : UserControl, ISTabContent
    {
        private Database db = Database.Instance;
        private DataSet sheetData;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        private bool isFormDirty = false;
        private bool isNewSheet = false;
        private bool isFormHidden = false;

        private Lot mLot;
        private TimeSheet mSelectedSheet;

        private string saveBtnTxt = "Save Changes";
        private string unlockBtnText = "Unlock Form";

        public TimeSheetView(Lot pLot)
        {
            InitializeComponent();
            mLot = pLot;
            LoadTimeSheets();
            displayOrHideForm();
            LockFields();
            cmdSaveEdit.Content = unlockBtnText;
            cmdSaveEdit.IsEnabled = false;
            cmdCancel.IsEnabled = false;
        }

        #region Event Delegations
        private void cmboName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFormDirty && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isFormDirty = true;
                cmdSaveEdit.IsEnabled = true;
            }
            if (isNewSheet && cmboName.SelectedIndex> -1)
            {
                String name = ((ComboBoxItem)cmboName.Items[cmboName.SelectedIndex]).Content.ToString();
                DataSet data = db.Select(TimeSheet.Fields.Wage.ToString(), TimeSheet.Table, TimeSheet.Fields.Name.ToString() + " = '" + name + "'", TimeSheet.Fields.Date.ToString() + " DESC", "1");
                if (data.NumberOfRows() > 0)
                {
                    amtWage.Amount = data.getFloat(TimeSheet.Fields.Wage.ToString());
                }
            }
        }

        private void cmboCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFormDirty && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isFormDirty = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void FieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isFormDirty && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isFormDirty = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isFormDirty && cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                isFormDirty = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void TabShowHideButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            displayOrHideForm();
        }
        private void UserControl_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                if (cmdSaveEdit.Content.Equals(saveBtnTxt))
                {
                    MainWindow.setActionList(GetEditModeActions());
                }
                else
                {
                    MainWindow.setActionList(GetViewModeActions());
                }
                if (dgTimeSheets.Items.Count > 0)
                {
                    DataSet qty = db.Select("SUM(" + TimeSheet.Fields.Hours.ToString() + ")", TimeSheet.Table, TimeSheet.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                    qty.Read();
                    txtTotalHours.Text = "" + qty.getString(0);

                    qty = db.Select("SUM(" + TimeSheet.Fields.Hours.ToString() + " * " + TimeSheet.Fields.Wage.ToString() + ")", TimeSheet.Table, TimeSheet.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                    qty.Read();
                    amtTotalCost.Amount = Single.Parse(qty.getString(0));
                }
            }
        }

        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt))
            {
                MessageBoxResult res;
                if (isNewSheet)
                {
                    res = MessageBox.Show("Saving new Time Sheet - " + msgCodes.GetString("M3201"), "Warning - 3201", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBox.Show("Saving Time Sheet Modifications - " + msgCodes.GetString("M3202"), "Warning - 3202", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                if (res == MessageBoxResult.No)
                {
                    return;
                }

                if (SaveChanges())
                {
                    if (IsSheetUnique())
                    {
                        try
                        {
                            db.BeginTransaction();
                            mSelectedSheet.SaveObject(db);
                            db.CommitTransaction();
                            LockFields();
                            cmdCancel.IsEnabled = false;
                            cmdSaveEdit.Content = unlockBtnText;
                            cmdSaveEdit.IsEnabled = true;
                            LoadTimeSheets();
                            PopulateFields();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Saving Time Sheet - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                UnlockFields();
                isFormDirty = false;
                cmdSaveEdit.IsEnabled = false;
                cmdSaveEdit.Content = saveBtnTxt;
                cmdCancel.IsEnabled = true;
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isFormDirty)
            {
                MessageBoxResult res;
                if (isNewSheet)
                {
                    res = MessageBox.Show("Cancel New Time Sheet - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBox.Show("Cancel Time Sheet Modifications - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                if (res == MessageBoxResult.Yes)
                {
                    LockFields();
                    if (!isNewSheet)
                    {
                        PopulateFields();
                    }
                    cmdSaveEdit.IsEnabled = true;
                    cmdSaveEdit.Content = unlockBtnText;
                    cmdCancel.IsEnabled = false;
                }
            }
            else
            {
                LockFields();
                PopulateFields();
                cmdSaveEdit.IsEnabled = true;
                cmdSaveEdit.Content = unlockBtnText;
                cmdCancel.IsEnabled = false;

            }
        }

        private void dgTimeSheets_AutoGeneratedColumns(object sender, EventArgs e)
        {
            Hashtable textMap = TimeSheetBinding.GetDisplayTextMap();
            foreach (DataGridColumn column in dgTimeSheets.Columns)
            {
                int index = dgTimeSheets.Columns.IndexOf(column);
                String headerText = column.Header.ToString();
                if (headerText.Contains("ID"))
                {
                    dgTimeSheets.Columns[index].Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    if (textMap.ContainsKey(headerText))
                    {
                        dgTimeSheets.Columns[index].Header = textMap[headerText];
                    }
                }
            }
        }

        private void dgTimeSheets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTimeSheets.SelectedIndex > -1)
            {
                if (cmdSaveEdit.Content.Equals(saveBtnTxt) && isFormDirty)
                {
                    MessageBoxResult res;
                    if (isNewSheet)
                    {
                        res = MessageBox.Show("Cancel New Time Sheet - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    }
                    else
                    {
                        res = MessageBox.Show("Cancel Time Sheet Modifications - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    }
                    if (res == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                TimeSheetBinding obj = (TimeSheetBinding)dgTimeSheets.SelectedCells[0].Item;
                sheetData.SeekToPrimaryKey(obj.timeID);
                mSelectedSheet = new TimeSheet(sheetData.GetRecordDataSet());
                if (isFormHidden)
                {
                    displayOrHideForm();
                }
                LockFields();
                PopulateFields();
                cmdSaveEdit.Content = unlockBtnText;
                cmdSaveEdit.IsEnabled = true;
                cmdCancel.IsEnabled = false;

                UserControl_IsVisibleChanged(null, new DependencyPropertyChangedEventArgs());
            }
        }

        #endregion

        private bool IsSheetUnique()
        {
            try
            {
                StringBuilder where = new StringBuilder();
                where.Append(TimeSheet.Fields.Name.ToString() + " = '" + cmboName.Text.ToUpper() + "'");
                where.Append(" AND " + TimeSheet.Fields.JobCode.ToString() + " = '" + cmboCode.Text + "'");
                where.Append(" AND " + TimeSheet.Fields.Date.ToString() + " = '" + dpDate.SelectedDate.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                if (!isNewSheet)
                {
                    where.Append(" AND " + TimeSheet.Fields.timeID.ToString() + " != '" + mSelectedSheet.GetTimeID() + "'");
                }

                DataSet data = db.Select("*", TimeSheet.Table, where.ToString());
                if (data.NumberOfRows() > 0)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checking Time Sheet is unique - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool SaveChanges()
        {
            if (cmboName.Text.Length > 0)
            {
                int code = mSelectedSheet.SetName(cmboName.Text.ToUpper());
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

            if (cmboCode.SelectedIndex == -1)
            {
                MessageBox.Show("Field 'Job Code' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                mSelectedSheet.SetJobCode(cmboCode.Text);
            }
            if (fltHours.Amount > 0)
            {
                mSelectedSheet.SetHours(fltHours.Amount);
            }
            else
            {
                MessageBox.Show("Field 'Hours' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (amtWage.Amount > 0)
            {
                mSelectedSheet.SetWage(amtWage.Amount);
            }
            else
            {
                MessageBox.Show("Field 'Wage' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (!dpDate.SelectedDate.Equals(DateTime.MinValue))
            {
                mSelectedSheet.SetDate(dpDate.SelectedDate);
            }
            else
            {
                MessageBox.Show("Field 'Date' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNotes.Text.Length > 0)
            {
                mSelectedSheet.SetNotes(txtNotes.Text);
            }
            else
            {
                mSelectedSheet.ClearField(TimeSheet.Fields.Notes.ToString());
            }
            return true;
        }

        private void UnlockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            txtNotes.IsReadOnly = false;
            txtNotes.Foreground = foreGround;
            txtNotes.Background = backGround;

            dpDate.IsReadOnly = false;
            amtWage.IsReadOnly = false;
            amtWage.Foreground = foreGround;
            amtWage.Background = backGround;

            fltHours.IsReadOnly = false;
            fltHours.Foreground = foreGround;
            fltHours.Background = backGround;

            cmboCode.IsReadOnly = false;
            cmboCode.Foreground = foreGround;
            cmboCode.Background = backGround;

            cmboName.IsReadOnly = false;
            cmboName.Foreground = foreGround;
            cmboName.Background = backGround;

        }

        private void LockFields()
        {
            SolidColorBrush foreGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            SolidColorBrush backGround = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));

            txtNotes.IsReadOnly = true;
            txtNotes.Foreground = foreGround;
            txtNotes.Background = backGround;

            dpDate.IsReadOnly = true;
            amtWage.IsReadOnly = true;
            amtWage.Foreground = foreGround;
            amtWage.Background = backGround;

            amtTotal.IsReadOnly = true;
            amtTotal.Foreground = foreGround;
            amtTotal.Background = backGround;

            fltHours.IsReadOnly = true;
            fltHours.Foreground = foreGround;
            fltHours.Background = backGround;

            cmboCode.IsReadOnly = true;
            cmboCode.Foreground = foreGround;
            cmboCode.Background = backGround;

            cmboName.IsReadOnly = true;
            cmboName.Foreground = foreGround;
            cmboName.Background = backGround;

            amtTotalCost.Foreground = foreGround;
            amtTotalCost.Background = backGround;
        }

        private void ClearFields()
        {
            amtTotal.Amount = 0;
            amtWage.Amount = 0;
            fltHours.Amount = 0;

            cmboCode.Text = "";
            cmboName.Text = "";
            txtNotes.Text = "";

            dpDate.Text = "";
        }

        private void PopulateFields()
        {
            amtWage.Amount = mSelectedSheet.GetWage();
            fltHours.Amount = mSelectedSheet.GetHours();
            amtTotal.Amount = fltHours.Amount * amtWage.Amount;
            cmboCode.Text = mSelectedSheet.GetJobCode();
            cmboName.Text = mSelectedSheet.GetName();
            dpDate.Text = ((mSelectedSheet.GetDate().Equals(DateTime.MinValue)) ? "" : mSelectedSheet.GetDate().ToShortDateString());
            txtNotes.Text = mSelectedSheet.GetNotes();
        }

        private ArrayList GetEditModeActions()
        {
            ArrayList actions = new ArrayList();
            if (isNewSheet)
            {
                if (isFormDirty)
                {
                    IconButton savenewTimeSheetBtn = new IconButton();
                    savenewTimeSheetBtn.Text = "Save New Time SHeet";
                    savenewTimeSheetBtn.Source = (Image)App.iconSet["symbol-save"];
                    savenewTimeSheetBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(savenewTimeSheetBtn);
                }
                IconButton cancelNewTimeSheetBtn = new IconButton();
                cancelNewTimeSheetBtn.Text = "Cancel New Time Sheet";
                cancelNewTimeSheetBtn.Source = (Image)App.iconSet["symbol-delete"];
                cancelNewTimeSheetBtn.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                actions.Add(cancelNewTimeSheetBtn);
            }
            else
            {
                if (isFormDirty)
                {
                    IconButton saveTimeSheetBtn = new IconButton();
                    saveTimeSheetBtn.Text = "Save Time Sheet Changes";
                    saveTimeSheetBtn.Source = (Image)App.iconSet["symbol-save"];
                    saveTimeSheetBtn.MouseDown += new MouseButtonEventHandler(cmdSaveEdit_Click);
                    actions.Add(saveTimeSheetBtn);
                }
                IconButton cancelChangesButton = new IconButton();
                cancelChangesButton.Text = "Cancel Time Sheet Changes";
                cancelChangesButton.Source = (Image)App.iconSet["symbol-delete"];
                cancelChangesButton.MouseDown += new MouseButtonEventHandler(cmdCancel_Click);
                actions.Add(cancelChangesButton);
            }
            return actions;
        }

        private ArrayList GetViewModeActions()
        {
            ArrayList actions = new ArrayList();
            IconButton addTimeSheetBtn = new IconButton();
            addTimeSheetBtn.Text = "Add Time Sheet";
            addTimeSheetBtn.Source = (Image)App.iconSet["symbol-add"];
            addTimeSheetBtn.MouseDown += new MouseButtonEventHandler(addTimeSheetBtn_MouseDown);
            actions.Add(addTimeSheetBtn);
            return actions;
        }

        private void addTimeSheetBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isFormHidden)
            {
                displayOrHideForm();
            }
            ClearFields();
            UnlockFields();
            cmdCancel.IsEnabled = true;
            cmdSaveEdit.Content = saveBtnTxt;
            isNewSheet = true;
            isFormDirty = false;
            try
            {
                mSelectedSheet = new TimeSheet(mLot.GetLotID());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Time Sheet Schema - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
                LockFields();
            }
        }

        /// <summary>
        /// This handles the hiding and showing of the form
        /// </summary>
        private void displayOrHideForm()
        {
            if (!isFormHidden)
            {
                BeginStoryboard((Storyboard)FindResource("SheetFormShrink"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-show"]).Source;
                isFormHidden = true;
            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("SheetFormExpand"));
                TabShowHideButton.Source = ((Image)App.iconSet["tab-hide"]).Source;
                isFormHidden = false;
            }
        }

        private void LoadTimeSheets()
        {
            try
            {
                DataSet data = db.Select("*", TimeSheet.Table, TimeSheet.Fields.lotID.ToString() + " = '" + mLot.GetLotID() + "'");
                data.BuildPrimaryKeyIndex(TimeSheet.Fields.timeID.ToString());
                Collection<TimeSheetBinding> gridData = data.getBindableCollection<TimeSheetBinding>();
                dgTimeSheets.ItemsSource = gridData;
                sheetData = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Time Sheets - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                DataSet data = db.Select("DISTINCT(" + TimeSheet.Fields.Name.ToString() + ")", TimeSheet.Table);
                cmboName.Items.Clear();
                while (data.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = data.getString(TimeSheet.Fields.Name.ToString());
                    cmboName.Items.Add(item);
                }
                if (mSelectedSheet != null && cmdSaveEdit.Content.Equals(saveBtnTxt))
                {
                    cmboName.Text = mSelectedSheet.GetName();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Employee Names - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region ISTabContent Members

        public bool TabIsClosingCallBack()
        {
            if (cmdSaveEdit.Content.Equals(saveBtnTxt) && isFormDirty)
            {
                MessageBoxResult res;
                if (isNewSheet)
                {
                    res = MessageBox.Show("Cancel New Time Sheet - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                else
                {
                    res = MessageBox.Show("Cancel Time Sheet Modifications - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                }
                if (res == MessageBoxResult.No)
                {
                    return false;
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
