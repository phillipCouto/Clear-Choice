using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class TotalMaterialCostSiteBinding
    {
        [BindableProperty]
        public String SiteName { get; set; }
        [BindableProperty]
        public String Quantity { get; set; }
        [BindableProperty]
        public String TotalValue { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("SiteName", "Site Name");
            textmap.Add("TotalValue", "Cost");
            return textmap;
        }
    }
}
