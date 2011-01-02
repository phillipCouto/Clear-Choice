using System.Resources;
using System.Windows;
using ExceptionLogging;

namespace Clear_Choice.Windows
{
    /// <summary>
    /// Interaction logic for Set_Price_Portions.xaml
    /// </summary>
    public partial class SetPricePortions : Window
    {
        public static readonly RoutedEvent ValuesModifiedEvent = EventManager.RegisterRoutedEvent("SetPricePortions.ValuesModified", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(LotRepairActionWindow));

        private float[] values;
        public float[] Values
        {
            get { return values; }
        }
        private ResourceManager msgCodes = MessageCodes.ResourceManager;
        private bool isModified;

        public event RoutedEventHandler ValuesModified
        {
            add { AddHandler(ValuesModifiedEvent, value); }
            remove { RemoveHandler(ValuesModifiedEvent, value); }
        }

        public SetPricePortions(float[] values)
        {
            InitializeComponent();
            amtRoughIn.Amount = values[0];
            amtService.Amount = values[1];
            amtFinal.Amount = values[2];
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (amtRoughIn.Amount + amtService.Amount + amtFinal.Amount != 1)
            {
                MessageBox.Show("Please confirm the values when added together make 1.00", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                values = new float[3];
                values[0] = amtRoughIn.Amount;
                values[1] = amtService.Amount;
                values[2] = amtFinal.Amount;
                RoutedEventArgs args = new RoutedEventArgs(ValuesModifiedEvent);
                RaiseEvent(args);
                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (isModified)
            {
                MessageBoxResult res = MessageBox.Show("Modifiying Price Portions - " + msgCodes.GetString("M3205"), "Warning - 3205", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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

        private void TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!isModified)
            {
                isModified = true;
                cmdSave.IsEnabled = true;
            }
        }
    }
}
