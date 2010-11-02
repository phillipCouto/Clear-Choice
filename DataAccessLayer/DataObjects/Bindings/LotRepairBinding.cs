using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotRepairBinding
    {
        [BindableProperty]
        public String repairID { get; set; }
        [BindableProperty]
        public String WorkOrder { get; set; }
        [BindableProperty]
        public String DateOfAppointment{get;set;}
        [BindableProperty]
        public String WindowOfAppointment { get; set; }
        [BindableProperty]
        public String RequestedBy { get; set; }
        [BindableProperty]
        public String InspectionPassed { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("WorkOrder", "Work Order");
            textmap.Add("DateOfAppointment", "Date");
            textmap.Add("WindowOfAppointment", "Window");
            textmap.Add("RequestedBy", "Requested By");
            textmap.Add("InspectionPassed", "Inspection Passed");
            return textmap;
        }
    }
}
