using System;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class InventoryItemBinding
    {
        [BindableProperty]
        public String itemID { get; set; }
        [BindableProperty]
        public String ItemName { get; set; }
        [BindableProperty]
        public String Category { get; set; }
        [BindableProperty]
        public float Quantity { get; set; }
        [BindableProperty]
        public String AverageCost { get; set; }
        [BindableProperty]
        public String CurrentCost { get; set; }

        public static Hashtable getDisplayTextMap()
        {
            Hashtable textMap = new Hashtable();
            textMap.Add("itemID", "Item ID");
            textMap.Add("ItemName", "Item Name");
            textMap.Add("AverageCost", "Average Cost");
            textMap.Add("CurrentCost", "Current Cost");
            return textMap;
        }

    }
}
