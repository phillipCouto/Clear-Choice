using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    /// <summary>
    /// This object represents a site object in the Sites Table.
    /// </summary>
    public class Site : DataObject
    {
        public enum Fields
        {
            siteID,
            clientID,
            SiteName,
            Address,
            City,
            SiteEmail,
            InspectorOffice,
            InspectorName,
            InspectorOfficePhone,
            InspectorCellPhone,
            InspectorEmail,
            ServiceSize,
            Notes
        }
        #region Fields
        public const String Table = "sites";
        public const String PrimaryKey = "siteID";
        #endregion
        /// <summary>
        /// Creates a new Siteobj with a unique ID.
        /// </summary>
        /// <param name="db"></param>
        public Site(Database db)
            : base(Table, db)
        {
            //Set the unqiue ID column for the Object
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            //Generate random number for primary Key
            Random ranNum = new Random();
            Boolean tryAgain = true;
            int id = 0;
            String hashedKey = null;
            while (tryAgain)
            {
                id = ranNum.Next(10);
                String textKey = Table + ":" + DateTime.Now.Ticks + ":" + id;
                hashedKey = db.GetMD5Hash(textKey);
                DataSet res = db.Select(PrimaryKey, Table, PrimaryKey + " = '" + hashedKey + "'");
                if (res.NumberOfRows() == 0)
                {
                    tryAgain = false;
                }
            }
            SetValue(PrimaryKey, hashedKey);
        }
        /// <summary>
        /// Loads the Site object with the provided dataset.
        /// </summary>
        /// <param name="dataSet"></param>
        public Site(DataSet dataSet)
            : base(dataSet)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            base.SetTable(Table);
        }
        /// <summary>
        /// Retrieves the contact object from the database.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SiteContact GetSiteContact(SiteContact.ContactTypes type)
        {
            int contactNum = 0;
            switch (type)
            {
                case SiteContact.ContactTypes.Foreman:
                    contactNum = 0;
                    break;
                case SiteContact.ContactTypes.Supervisor1:
                    contactNum = 1;
                    break;
                case SiteContact.ContactTypes.Supervisor2:
                    contactNum = 2;
                    break;
                case SiteContact.ContactTypes.SupplyAuth:
                    contactNum = 3;
                    break;
            }
            DataSet serviceData = Database.Instance.Select("*", SiteContact.Table, SiteContact.Fields.siteID.ToString() + " = '" + GetSiteID() + "' AND " + SiteContact.Fields.contactType.ToString() + " = " + contactNum);
            if (serviceData.NumberOfRows() > 0)
            {
                serviceData.Read();
                return new SiteContact(serviceData.GetRecordDataSet());
            }
            return null;
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the inspector's email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetInspectorEmail(string email)
        {
            if (Formating.EmailAddressCheck(email))
            {
                SetValue(Fields.InspectorEmail.ToString(), email);
                return 0;
            }
            return 1107;
        }
        /// <summary>
        /// Returns the email address for the inspector.
        /// </summary>
        /// <returns></returns>
        public String GetInspectorEmail()
        {
            return getString(Fields.InspectorEmail.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the foreman.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetSiteEmail(String email)
        {
            if (Formating.EmailAddressCheck(email))
            {
                SetValue(Fields.SiteEmail.ToString(), email);
                return 0;
            }
            return 1107;
        }
        /// <summary>
        /// Returns the Foreman of the site.
        /// </summary>
        /// <returns></returns>
        public String GetSiteEmail()
        {
            return getString(Fields.SiteEmail.ToString());
        }
        /// <summary>
        /// Sets the notes associated with the site.
        /// </summary>
        /// <param name="notes"></param>
        public void SetNotes(String notes)
        {
            SetValue(Fields.Notes.ToString(), notes.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the notes associated with the site.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets the service size for the site.
        /// </summary>
        /// <param name="size"></param>
        public void SetServiceSize(int size)
        {
            SetValue(Fields.ServiceSize.ToString(), size);
        }
        /// <summary>
        /// Returns the service size for the site.
        /// </summary>
        /// <returns></returns>
        public int GetServiceSize()
        {
            return getInt(Fields.ServiceSize.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Insepectors Cell Number
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public int SetInspectorCellNumber(String number)
        {
            if (Formating.PhoneNumberCheck(number))
            {
                SetValue(Fields.InspectorCellPhone.ToString(), number);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the cell number for the insepector.
        /// </summary>
        /// <returns></returns>
        public String GetInspectorCellNumber()
        {
            return getString(Fields.InspectorCellPhone.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the office number of the Inspector.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetInspectorOfficeNumber(String phone)
        {
            if (Formating.PhoneNumberCheck(phone))
            {
                SetValue(Fields.InspectorOfficePhone.ToString(), phone);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the inspectors Office number.
        /// </summary>
        /// <returns></returns>
        public String GetInspectorOfficeNumber()
        {
            return getString(Fields.InspectorOfficePhone.ToString());
        }
        /// <summary>
        /// Sets the name of the Insepector.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetInspectorName(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.InspectorName.ToString(), name);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the inspectors name.
        /// </summary>
        /// <returns></returns>
        public String GetInspectorName()
        {
            return getString(Fields.InspectorName.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the inspectors office.
        /// </summary>
        /// <param name="office"></param>
        /// <returns></returns>
        public int SetInspectorOffice(String office)
        {
            if (Formating.TitleCheck(office))
            {
                SetValue(Fields.InspectorOffice.ToString(), office);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the office that the inspector belongs to.
        /// </summary>
        /// <returns></returns>
        public String GetInspectorOffice()
        {
            return getString(Fields.InspectorOffice.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the value of the site name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetSiteName(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.SiteName.ToString(), name);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the Site Name.
        /// </summary>
        /// <returns></returns>
        public String GetSiteName()
        {
            return getString(Fields.SiteName.ToString());
        }
        /// <summary>
        /// Sets the client id for the site object.
        /// </summary>
        /// <param name="clientid"></param>
        public void SetClientID(String clientid)
        {
            SetValue(Fields.clientID.ToString(), clientid);
        }
        /// <summary>
        /// Returns the client id associated with the site.
        /// </summary>
        /// <returns></returns>
        public String GetClientID()
        {
            return getString(Fields.clientID.ToString());
        }
        /// <summary>
        /// Performs a format check and if check clears sets the city.
        /// </summary>
        /// <param name="city"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetCity(String city)
        {
            if (Formating.NameCheck(city))
            {
                SetValue(Fields.City.ToString(), city);
                return 0;
            }
            return 1103;
        }
        /// <summary>
        /// Returns the city for the site.
        /// </summary>
        /// <returns></returns>
        public String GetCity()
        {
            return getString(Fields.City.ToString());
        }
        /// <summary>
        /// Performs a Format check and if the check clears the address is set.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>0 if successfull otherwise the MessageCode number</returns>
        public int SetAddress(String address)
        {
            if (Formating.TitleCheck(address))
            {
                SetValue(Fields.Address.ToString(), address);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the Address of the site.
        /// </summary>
        /// <returns></returns>
        public String GetAddress()
        {
            return getString(Fields.Address.ToString());
        }
        /// <summary>
        /// Returns the SiteID for the site.
        /// </summary>
        /// <returns></returns>
        public String GetSiteID()
        {
            return getString(Fields.siteID.ToString());
        }
    }


}
