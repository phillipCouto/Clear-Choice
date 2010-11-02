using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class InventoryTransactionItemBinding
    {
        [BindableProperty]
        public String transactionID { get; set; }
        [BindableProperty]
        public String itemID { get; set; }
        [BindableProperty]
        public String ItemName { get; set; }
        [BindableProperty]
        public String UnitPrice { get; set; }
        [BindableProperty]
        public int Quantity { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("ItemName", "Item Name");
            textmap.Add("UnitPrice", "Unit Price");
            textmap.Add("itemID", "Item ID");
            return textmap;
        }
    }
}
