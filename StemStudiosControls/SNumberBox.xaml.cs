using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for CurrencyBox.xaml
    /// </summary>
    public partial class SNumberBox : UserControl
    {
        private float mAmount = 0;
        private bool isCurrency = true;

        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(TextChangedEventHandler), typeof(SNumberBox));

        public event TextChangedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }

        public float Amount
        {
            get { return mAmount; }
            set { mAmount = value; updateValue(); }
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
            set { if (value != txtValue.IsReadOnly) { SwitchReadState(); } }
        }

        public Boolean IsCurrency
        {
            get { return isCurrency; }
            set{isCurrency = value;}
        }
        public SNumberBox()
        {
            InitializeComponent();
        }

        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsReadOnly)
            {
                if (txtValue.Text.Length > 0)
                {
                    mAmount = Single.Parse(txtValue.Text);
                }
            }
            TextChangedEventArgs newEvent = new TextChangedEventArgs(TextChangedEvent, e.UndoAction);
            RaiseEvent(newEvent);
        }
        private void updateValue()
        {
            if(txtValue.IsReadOnly & IsCurrency)
            {
                txtValue.Text = "$" + mAmount.ToString("#0.00");
            }
            else{
                txtValue.Text = mAmount.ToString("#0.00");
            }
        }

        private void SwitchReadState()
        {
            if (txtValue.IsReadOnly)
            {
                txtValue.IsReadOnly = false;
                if (mAmount > 0)
                {
                    txtValue.Text = mAmount.ToString("#0.00");
                }
                else
                {
                    txtValue.Text = "";
                }
            }
            else
            {
                txtValue.IsReadOnly = true;
                if(IsCurrency)
                {
                    txtValue.Text = "$" + mAmount.ToString("#0.00");
                }else
                {
                    txtValue.Text = mAmount.ToString("#0.00");
                }
            }
        }

        private void txtValue_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void txtValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            int decCount = 0;
            foreach (Char c in Text2.ToCharArray())
            {
                if (!Char.IsDigit(c) && !Char.Parse(".").Equals(c))
                {
                    return false;
                }
                else if (Char.Parse(".").Equals(c))
                {
                    if (txtValue.Text.Contains("."))
                    {
                        return false;
                    }
                    else
                    {
                        decCount++;
                    }
                }
            }
            if (decCount > 1)
            {
                return false;
            }
            return true;
        }
    }
}
