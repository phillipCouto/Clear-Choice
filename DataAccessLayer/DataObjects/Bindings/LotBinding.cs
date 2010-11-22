using System;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotBinding
    {
        [BindableProperty]
        public String lotID { get; set; }
        [BindableProperty]
        public String assocID { get; set; }
        [BindableProperty]
        public int LotNumber { get; set; }
        [BindableProperty]
        public int BlockNumber { get; set; }
        [BindableProperty]
        public String Address { get; set; }
        [BindableProperty]
        public String City { get; set; }


        public static Hashtable getdisplayTextMap()
        {
            Hashtable displayText = new Hashtable();
            displayText.Add("LotNumber", "Lot Number");
            displayText.Add("BlockNumber", "Block");
            return displayText;
        }

    }
}
