using System;
using System.Windows;
using Stemstudios.DataAccessLayer;

namespace ClearChoice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static ResourceDictionary iconSet = new ResourceDictionary();
        [STAThread]
        static void Main(String[] args)
        {
            try
            {
                Database.Instance.CanConnect();
            }
            catch (DatabaseConnectionException e)
            {
                MessageBox.Show("Database connection failed. Message: " + e.Message, "Database Connection Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
                //Log Event
                Environment.Exit(0);
            }
            new FrameworkElement();
            iconSet.Source = new Uri("/Resources/IconSet.xaml", UriKind.Relative);
            new MainWindow().Show();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
