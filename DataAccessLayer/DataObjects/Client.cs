using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    /// <summary>
    /// Simple Implementation of client object. Will be expanded soon.
    /// </summary>
    public class Client : DataObject
    {
        public enum Fields { clientID, Name, ClientType, Address, City, PostalCode, FaxNumber, PhoneNumber, EmailAddress }
        public const String Table = "clients";
        public const String PrimaryKey = "clientID";

        /// <summary>
        /// Creates a new Client object using a the client Table schema.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        public Client(Database db)
            : base(Table, db)
        {
            //Set the unqiue ID column for the Object
            PrimaryKeyColumns = new String[1];
            PrimaryKeyColumns[0] = PrimaryKey;
            //Generate random number for primary Key
            Random ranNum = new Random();
            Boolean tryAgain = true;
            int id = 0;
            String hashedKey = null;
            while (tryAgain)
            {
                id = ranNum.Next(10);
                String textKey = Table+":" + DateTime.Now.Ticks + ":" + id;
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
        /// Creates a client object using a dataset as the source for data.
        /// </summary>
        /// <param name="dataSet"></param>
        public Client(DataSet dataSet)
            : base(dataSet)
        {
            PrimaryKeyColumns = new String[1];
            PrimaryKeyColumns[0] = PrimaryKey;
            base.SetTable(Table);
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
        public int SetEmailAddress(String email)
        {
            if (Formating.EmailAddressCheck(email))
            {
                SetValue(Fields.EmailAddress.ToString(), email);
                return 0;
            }
            return 1107;
        }
        /// <summary>
        /// Returns the email address.
        /// </summary>
        /// <returns></returns>
        public String GetEmailAddress()
        {
            return getString(Fields.EmailAddress.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the phone number.
        /// </summary>
        /// <param name="phonenumber"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
        public int SetPhoneNumber(String phonenumber)
        {
            if (Formating.PhoneNumberCheck(phonenumber))
            {
                SetValue(Fields.PhoneNumber.ToString(), phonenumber);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the clients phone number.
        /// </summary>
        /// <returns></returns>
        public String GetPhoneNumber()
        {
            return getString(Fields.PhoneNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the postal code for the client.
        /// </summary>
        /// <param name="postalcode"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
        public int SetPostalCode(String postalcode)
        {
            if (Formating.PostalCodeCheck(postalcode))
            {
                SetValue(Fields.PostalCode.ToString(), postalcode);
                return 0;
            }
            return 1106;
        }
        /// <summary>
        /// Returns the client's postal code.
        /// </summary>
        /// <returns></returns>
        public String GetPostalCode()
        {
            return getString(Fields.PostalCode.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the fax number of the client
        /// </summary>
        /// <param name="province"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
        public int SetFaxNumber(String fax)
        {
            if (Formating.PhoneNumberCheck(fax))
            {
                SetValue(Fields.FaxNumber.ToString(), fax);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the client's fax number.
        /// </summary>
        /// <returns></returns>
        public String GetFaxNumber()
        {
            return getString(Fields.FaxNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the city for the client.
        /// </summary>
        /// <param name="city"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
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
        /// Returns the client's city.
        /// </summary>
        /// <returns></returns>
        public String GetCity()
        {
            return getString(Fields.City.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the address of the client.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
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
        /// Returns the address of the client.
        /// </summary>
        /// <returns></returns>
        public String GetAddress()
        {
            return getString(Fields.Address.ToString());
        }
        /// <summary>
        /// Performs a format check and sets the name of the client if the format is ok.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>0 for success number of Message Code if otherwise</returns>
        public int SetName(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.Name.ToString(), name);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the First Name Set to the Client
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            return getString(Fields.Name.ToString());
        }
        /// <summary>
        /// Sets the Client Type.
        /// 0 = Client
        /// 1 = Company
        /// </summary>
        /// <param name="type"></param>
        public void SetClientType(int type)
        {
            SetValue(Fields.ClientType.ToString(), type);
        }
        /// <summary>
        /// Gets the Clients ID
        /// </summary>
        /// <returns></returns>
        public String GetClientID()
        {
            return this.getString("clientID");
        }
        /// <summary>
        /// Gets the int representing the client type.
        /// 0 = Client
        /// 1 = Company
        /// </summary>
        /// <returns></returns>
        public int GetClientType()
        {
            return this.getInt(Fields.ClientType.ToString());
        }
    }
}
