using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Stemstudios.DataAccessLayer.DataObjects;
using System.Resources;
using ExceptionLogging;

namespace Clear_Choice.Windows
{
    /// <summary>
    /// Interaction logic for LotRepairActionWindow.xaml
    /// </summary>
    public partial class LotRepairActionWindow : Window
    {
        public static readonly RoutedEvent ObjectModifiedEvent = EventManager.RegisterRoutedEvent("ObjectModified", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LotRepairActionWindow));

        public event RoutedEventHandler ObjectModified
        {
            add { AddHandler(ObjectModifiedEvent, value); }
            remove { RemoveHandler(ObjectModifiedEvent, value); }
        }

        private LotRepairAction mAction;
        private bool isModified = false;
        private bool isNew = true;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        public LotRepairActionWindow(LotRepairAction action,bool New)
        {
            InitializeComponent();
            mAction = action;
            if (!New)
            {
                PopulateFields();
                isNew = false;
            }
            isModified = false;
            cmdSaveEdit.IsEnabled = false;
        }

        private void PopulateFields()
        {
            txtAction.Text = mAction.GetAction();
            txtDescription.Text = mAction.GetDescription();
            txtProblem.Text = mAction.GetProblemArea();
            txtTime.Text = mAction.GetTime();
            dpActionDate.Text = ((mAction.GetDate().Equals(DateTime.MinValue)) ? "" : mAction.GetDate().ToShortDateString());
        }

        private void FieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isModified)
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void dpActionDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isModified)
            {
                isModified = true;
                cmdSaveEdit.IsEnabled = true;
            }
        }

        private void cmdSaveEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtProblem.Text.Length > 0)
            {
                mAction.SetProblemArea(txtProblem.Text.ToUpper());
            }
            else
            {
                MessageBox.Show("Field 'Problem Area' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtDescription.Text.Length > 0)
            {
                mAction.SetDescritpion(txtDescription.Text.ToUpper());
            }
            else
            {
                MessageBox.Show("Field 'Description' - " + msgCodes.GetString("M1101"), "Error - 1101", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtTime.Text.Length > 0)
            {
                mAction.SetTime(txtTime.Text.ToUpper());
            }
            else if(!isNew)
            {
                mAction.ClearField(LotRepairAction.Fields.Time.ToString());
            }
            if (txtAction.Text.Length > 0)
            {
                mAction.SetAction(txtAction.Text.ToUpper());
            }
            else if (!isNew)
            {
                mAction.ClearField(LotRepairAction.Fields.Action.ToString());
            }
            if (!dpActionDate.SelectedDate.Equals(DateTime.MinValue))
            {
                mAction.SetDate(dpActionDate.SelectedDate);
            }
            else if (!isNew)
            {
                mAction.ClearField(LotRepairAction.Fields.Date.ToString());
            }

            RoutedEventArgs args = new RoutedEventArgs(ObjectModifiedEvent);
            RaiseEvent(args);
            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isNew)
            {
                if (isModified)
                {
                    MessageBoxResult res = MessageBox.Show("Adding New Repair Action - " + msgCodes.GetString("M3204"), "Warning - 3204", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                if (isModified)
                {
                    MessageBoxResult res = MessageBox.Show("Modifiying Repair Action - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
