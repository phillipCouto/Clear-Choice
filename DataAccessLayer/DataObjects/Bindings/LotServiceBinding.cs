using System;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects.Bindings
{
    [BindableObject]
    public class LotServiceBinding
    {
        [BindableProperty]
        public String lotID { get; set; }
        [BindableProperty]
        public int ServiceType { get; set; }
        [BindableProperty]
        public String CalledIn { get; set; }
        [BindableProperty]
        public String Passed { get; set; }
        [BindableProperty]
        public int Defective { get; set; }
        [BindableProperty]
        public String PONumber { get; set; }
        [BindableProperty]
        public String Billed { get; set; }
        [BindableProperty]
        public float Amount { get; set; }


        public static Hashtable GetDisplayTextMap()
        {
            Hashtable textMap = new Hashtable();
            textMap.Add("ServiceType", "Service Type");
            textMap.Add("CalledIn", "Called In");
            textMap.Add("PONumber", "PO Number");
            return textMap;
        }
    }
}
