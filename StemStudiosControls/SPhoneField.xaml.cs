using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;
using System.Text;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for SPhoneField.xaml
    /// </summary>
    public partial class SPhoneField : UserControl
    {
        private string number = "";
        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(SNumberBox));

        public event TextChangedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        public string PhoneNumber
        {
            get { return number; }
            set { if(!IsTextValid(value)){number = CleanNumber(value);}else{number = value;} ValueChanged(); }
        }

        private string CleanNumber(string value)
        {
            StringBuilder newNumber = new StringBuilder();
            foreach (Char c in value.ToCharArray())
            {
                if (Char.IsDigit(c))
                {
                    newNumber.Append(c);
                }
            }
            return newNumber.ToString();
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

        public bool IsReadOnly
        {
            get { return txtValue.IsReadOnly; }
            set { if (value != txtValue.IsReadOnly) { SwitchReadState(); } }
        }

        private void SwitchReadState()
        {
            if (txtValue.IsReadOnly)
            {
                txtValue.Text = number;
                txtValue.IsReadOnly = false;
            }
            else
            {
                number = txtValue.Text;
                txtValue.Text = String.Format("{0:####(###) ###-####}", double.Parse(number));
                txtValue.IsReadOnly = true;
            }
        }

        private bool IsTextValid(String text)
        {
            foreach (Char c in text.ToCharArray())
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public SPhoneField()
        {
            InitializeComponent();
        }

        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsReadOnly)
            {
                TextChangedEventArgs newEvent = new TextChangedEventArgs(TextChangedEvent, e.UndoAction);
                RaiseEvent(newEvent);
            }
        }

        private void txtValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextValid(e.Text);
        }

        private void txtValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextValid(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private void ValueChanged()
        {
            if (!txtValue.IsReadOnly)
            {
                txtValue.Text = number;
            }
            else
            {
                txtValue.Text = String.Format("{0:####(###) ###-####}", double.Parse(number));
            }
        }
    }
}
