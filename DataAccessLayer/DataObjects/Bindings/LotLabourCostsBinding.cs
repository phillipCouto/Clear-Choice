using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotLabourCostsBinding
    {
        [BindableProperty]
        public int LotNumber { get; set; }
        [BindableProperty]
        public String Address { get; set; }
        [BindableProperty]
        public String City { get; set; }
        [BindableProperty]
        public double Hours { get; set; }
        [BindableProperty]
        public String LabourCost { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("LotNumber","Lot Number");
            textmap.Add("Hours", "Total Hours");
            textmap.Add("LabourCost", "Total Labour Cost");
            return textmap;
        }
    }
}
