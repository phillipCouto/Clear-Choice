using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class TotalBillingBinding
    {
        [BindableProperty]
        public String lotID { get; set; }
        [BindableProperty]
        public int LotNumber { get; set; }
        [BindableProperty]
        public String Address { get; set; }
        [BindableProperty]
        public String City { get; set; }
        [BindableProperty]
        public String Amount { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("LotNumber", "Lot Number");

            return textmap;
        }
    }
}
