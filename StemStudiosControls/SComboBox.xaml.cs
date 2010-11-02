using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for SComboBox.xaml
    /// </summary>
    public partial class SComboBox : UserControl
    {
        private bool readOnly = false;
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChangedEventHandler), typeof(SComboBox));

        public event SelectionChangedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }

        public ItemCollection Items
        {
            get { return cmboValue.Items; }
        }

        public new Brush Foreground 
        {
            get { return txtValue.Foreground; }
            set { txtValue.Foreground = value; }
        }

        public new Brush Background
        {
            get { return txtValue.Background; }
            set { txtValue.Background = value; }
        }

        public Boolean IsReadOnly
        {
            get { return txtValue.IsReadOnly; }
            set { if (value != readOnly) { SwitchReadState(); } }
        }

        public String Text
        {
            get { return cmboValue.Text; }
            set { cmboValue.Text = value; txtValue.Text = value; }
        }

        public int SelectedIndex
        {
            get { return cmboValue.SelectedIndex; }
            set { cmboValue.SelectedIndex = value; }
        }

        public bool IsEditable
        {
            get { return cmboValue.IsEditable; }
            set { cmboValue.IsEditable = value; }
        }

        public SComboBox()
        {
            InitializeComponent();
        }

        private void SwitchReadState()
        {
            if (readOnly)
            {
                readOnly = false;
                cmboValue.Visibility = Visibility.Visible;
                txtValue.Visibility = Visibility.Collapsed;
            }
            else
            {
                readOnly = true;
                cmboValue.Visibility = Visibility.Collapsed;
                txtValue.Visibility = Visibility.Visible;
                if (IsEditable)
                {
                    txtValue.Text = cmboValue.Text;
                }
                else
                {
                    if (cmboValue.SelectedIndex > -1)
                    {
                        txtValue.Text = ((ComboBoxItem)cmboValue.SelectedItem).Content.ToString();
                    }
                }
            }
        }

        private void cmboValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChangedEventArgs newEvent = new SelectionChangedEventArgs(SelectionChangedEvent,e.RemovedItems,e.AddedItems);
            RaiseEvent(newEvent);
        }
    }
}
