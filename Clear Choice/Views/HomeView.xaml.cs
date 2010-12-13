using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Stemstudios.UIControls;
using Clear_Choice.Views;

namespace ClearChoice.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            this.Name = "MainHomePage";
            InitializeComponent();
            setupActionList();
        }

        private void IconButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new ClientsView());
        }

        private void AddNewClientButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new ClientView());
        }

        private void setupActionList()
        {
            ArrayList actions = new ArrayList();
            IconButton newClientButton = new IconButton();
            newClientButton.Text = "New Client";
            newClientButton.Source = (Image)App.iconSet["customer1"];
            newClientButton.MouseDown += new MouseButtonEventHandler(AddNewClientButton_MouseDown);
            actions.Add(newClientButton);

            IconButton viewClientsButton = new IconButton();
            viewClientsButton.Text = "View Clients";
            viewClientsButton.Source = (Image)App.iconSet["customers"];
            viewClientsButton.MouseDown += new MouseButtonEventHandler(IconButton_MouseDown);
            actions.Add(viewClientsButton);

            IconButton viewInventoryButton = new IconButton();
            viewInventoryButton.Text = "View Inventory";
            viewInventoryButton.Source = (Image)App.iconSet["symbol-inventory"];
            viewInventoryButton.MouseDown += new MouseButtonEventHandler(btnViewInventory_MouseDown);
            actions.Add(viewInventoryButton);

            MainWindow.setActionList(actions);
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                setupActionList();
            }
        }

        private void btnAddInventoryItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new InventoryView(true), (Image)App.iconSet["symbol-inventory"], "Inventory");
        }

        private void btnViewInventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new InventoryView(), (Image)App.iconSet["symbol-inventory"], "Inventory");
        }

        private void btnActiveLots_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new IncompletedLotsReport(), (Image)App.iconSet["home"], "Active Lots Report");
        }

        private void btnCompletedLots_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new CompletedLotsReport(), (Image)App.iconSet["home"], "Completed Lots Report");
        }

        private void btnCitys_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new CityReport());
        }

        private void btnCompletedRepairs_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new ScheduledRepairsView(), (Image)App.iconSet["symbol-repair"], "Completed Repairs Report");
        }

        private void btnViewTransactions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new InventoryTransactionsView(), (Image)App.iconSet["symbol-transactions"], "View Transactions");
        }

        private void btnLowStock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LowAmountInvReport(), (Image)App.iconSet["symbol-emptycart"], "Low Stock Report");
        }

        private void IconButton_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotExtrasBilledReport(), (Image)App.iconSet["check-icon"], "Lot Extras Billed Report");
        }

        private void IconButton_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotServicesBilledReport(), (Image)App.iconSet["check-icon"], "Lot Services Billed");
        }

        private void IconButton_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotLabourCostReport(), (Image)App.iconSet["clock-icon"], "Lot Labour Costs");
        }

        private void IconButton_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new LotSiteMaterialCostReport(), (Image)App.iconSet["symbol-transactions"], "Lot/Site Material Costs");
        }
    }
}
