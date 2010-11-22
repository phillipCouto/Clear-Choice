using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class TimeSheetBinding
    {
        [BindableProperty]
        public String   timeID  { get; set; }
        [BindableProperty]
        public String   Date    { get; set; }
        [BindableProperty]
        public String   Name    { get; set; }
        [BindableProperty]
        public String   JobCode { get; set; }
        [BindableProperty]
        public float    Hours   { get; set; }
        [BindableProperty]
        public String Notes     { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("JobCode", "Job Code");
            return textmap;
        }

    }
}
