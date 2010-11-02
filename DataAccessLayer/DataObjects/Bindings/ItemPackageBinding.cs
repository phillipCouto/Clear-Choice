using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios.DataAccessLayer.DataObjects.Attributes;
using System.Collections;

namespace DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class ItemPackageBinding
    {
        [BindableProperty]
        public String itemID { get; set; }
        [BindableProperty]
        public String ItemName { get; set; }
        [BindableProperty]
        public String ItemQuantity { get; set; }

        public static Hashtable getdisplayTextMap()
        {
            Hashtable textMap = new Hashtable();
            textMap.Add("ItemName","Item Name");
            textMap.Add("ItemQuantity", "Item Quantity");
            return textMap;
        }
    }
}
