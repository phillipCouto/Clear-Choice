using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class TotalBilledExtraBinding
    {
        [BindableProperty]
        public String ExtraItem { get; set; }
        [BindableProperty]
        public String Quantity { get; set; }
        [BindableProperty]
        public String UnitPrice { get; set; }
        [BindableProperty]
        public String TotalPrice { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("ExtraItem", "Extra Item");
            textmap.Add("Quantity", "Total Quantity");
            textmap.Add("UnitPrice", "Average Unit Price");
            textmap.Add("TotalPrice", "Total Price");
            return textmap;
        }
    }
}
