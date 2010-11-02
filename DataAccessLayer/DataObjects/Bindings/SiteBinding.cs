using System;
using System.Collections;


namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class SiteBinding
    {
        [BindableProperty]
        public String siteID { get; set; }
        [BindableProperty]
        public String SiteName { get; set; }
        [BindableProperty]
        public String Address { get; set; }
        [BindableProperty]
        public String City { get; set; }

        public static Hashtable getdisplayTextMap()
        {
            Hashtable displayText = new Hashtable();
            displayText.Add("SiteName", "Site Name");
            displayText.Add("SupervisorPhone", "Supervisor Phone");
            return displayText;
        }
        public static String selectedFields()
        {
            return "siteID,SiteName,Address,City";
        }
    }
}
