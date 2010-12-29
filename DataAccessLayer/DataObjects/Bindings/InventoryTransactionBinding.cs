using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class InventoryTransactionBinding
    {
        [BindableProperty]
        public String transactionID { get; set; }
        [BindableProperty]
        public String Name { get; set; }
        [BindableProperty]
        public String DateOfTransaction { get; set; }
        [BindableProperty]
        public String TotalValue{get;set;}
        [BindableProperty]
        public int TotalQuantity { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("DateOfTransaction", "Date of Transaction");
            textmap.Add("TotalValue", "Total Value");
            textmap.Add("TotalQuantity", "Total Quantity");
            textmap.Add("transactionID", "Transaction ID");
            textmap.Add("Name", "Recieved By");
            return textmap;
        }
    }
}
