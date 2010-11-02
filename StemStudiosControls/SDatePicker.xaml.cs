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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for SDatePicker.xaml
    /// </summary>
    public partial class SDatePicker : UserControl
    {
        private bool readOnly = false;

        public static readonly RoutedEvent DateChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(SDatePicker));

        public String Text
        {
            get { if (readOnly) { return txtDisplay.Text; } else { return dpDate.Text; } }
            set { txtDisplay.Text = value; dpDate.Text = value;}
        }

        public Boolean IsReadOnly
        {
            get { return readOnly; }
            set { readOnly = value; ChangeReadOnlyState(); }
        }

        public Brush TextForeground
        {
            get { return txtDisplay.Foreground; }
            set { txtDisplay.Foreground = value; }
        }

        public DateTime SelectedDate
        {
            get { if (dpDate.SelectedDate.HasValue) { return dpDate.SelectedDate.Value; } else { return DateTime.MinValue; } }
        }

        public event SelectionChangedEventHandler SelectedDateChanged
        {
            add { AddHandler(DateChangedEvent, value); }
            remove { RemoveHandler(DateChangedEvent, value); }
        }

        public SDatePicker()
        {
            InitializeComponent();
        }

        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChangedEventArgs newEventArgs = new SelectionChangedEventArgs(DateChangedEvent,e.RemovedItems, e.AddedItems);
            UpdateToolTips();
            RaiseEvent(newEventArgs);
        }

        private void UpdateToolTips()
        {
            if (dpDate.Text.Length > 0)
            {
                try
                {

                    txtDisplay.ToolTip = dpDate.SelectedDate.Value.ToLongDateString();
                    dpDate.ToolTip = dpDate.SelectedDate.Value.ToLongDateString();
                }
                catch (Exception)
                {
                    //Not really important to display
                }
            }
        }

        private void ChangeReadOnlyState()
        {
            if (readOnly)
            {
                txtDisplay.Visibility = Visibility.Visible;
                dpDate.Visibility = Visibility.Collapsed;
                txtDisplay.Text = dpDate.Text;
            }
            else
            {
                txtDisplay.Visibility = Visibility.Collapsed;
                dpDate.Visibility = Visibility.Visible;
            }

        }
    }
}
