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
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DataAccessLayer;

namespace Clear_Choice.Views
{
    /// <summary>
    /// Interaction logic for SiteMaterialSummary.xaml
    /// </summary>
    public partial class SiteMaterialSummary : UserControl
    {
        private Site mSite;
        private Database db = Database.Instance;
        public SiteMaterialSummary()
        {
            InitializeComponent();
        }

        private void LoadSummary()
        {
            String fields = "inventory_transactions.assocID,inventory_transactions.DateOfTransaction,inventory_item_transactions.itemID,inventory_items.ItemName, SUM(inventory_item_transactions.Quantity) As Quantity";
            String from = "inventory_transactions,inventory_item_transactions,inventory_items";
            String where = "inventory_transactions.transactionID = inventory_item_transactions.transactionID AND inventory_item_transactions.itemID = inventory_items.itemID AND inventory_transactions.assocID = '"+mSite.GetSiteID()+"'";
            String groupBy = "GROUP BY inventory_item_transactions.itemID";
        }
    }
}
