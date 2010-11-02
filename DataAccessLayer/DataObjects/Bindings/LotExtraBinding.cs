using System;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotExtraBinding
    {
        [BindableProperty]
        public String extraID { get; set; }
        [BindableProperty]
        public String ExtraItem { get; set; }
        [BindableProperty]
        public String Location { get; set; }
        [BindableProperty]
        public String PO { get; set; }
        [BindableProperty]
        public int Quantity { get; set; }
        [BindableProperty]
        public int BilledQuantity { get; set; }
        [BindableProperty]
        public String UnitPrice { get; set; }
        [BindableProperty]
        public String TotalPrice { get; set; }
        [BindableProperty]
        public String BilledDate { get; set; }


        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textMap = new Hashtable();
            textMap.Add("BilledQuantity", "Billed Quantity");
            textMap.Add("BilledDate", "Billed Date");
            textMap.Add("ExtraItem", "Extra");
            textMap.Add("UnitPrice", "Unit Price");
            textMap.Add("TotalPrice", "Total Price");
            return textMap;
        }
    }
}
