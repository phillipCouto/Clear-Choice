using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotRepairActionBinding
    {
        [BindableProperty]
        public String actionID { get; set; }
        [BindableProperty]
        public String ProblemArea { get; set; }
        [BindableProperty]
        public String Description { get; set; }
        [BindableProperty]
        public String Date { get; set; }
        [BindableProperty]
        public String Time { get; set; }
        [BindableProperty]
        public String Action { get; set; }

        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textmap = new Hashtable();
            textmap.Add("ProblemArea", "Problem Area");
            textmap.Add("Date", "Action Date");
            textmap.Add("Time", "Action Time");
            textmap.Add("Action", "Action Taken");
            return textmap;
        }
    }
}
