using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class SiteContact: DataObject
    {
        public enum Fields {siteID,contactType,Name,Phone,Email}
        public enum ContactTypes{Supervisor1,Supervisor2,SupplyAuth,Foreman}

        public const String Table = "site_contacts";
        public readonly String[] PrimaryKeys = new String[2] { "siteID", "contactType" };

        private Hashtable updates = new Hashtable();
        private bool isNew;

        public SiteContact(ContactTypes type, String siteID)
            : base(Table, Database.Instance)
        {
            PrimaryKeyColumns = PrimaryKeys;
            int contactNum = 0;
            switch (type)
            {
                case ContactTypes.Foreman:
                    contactNum = 0;
                    break;
                case ContactTypes.Supervisor1:
                    contactNum = 1;
                    break;
                case ContactTypes.Supervisor2:
                    contactNum = 2;
                    break;
                case ContactTypes.SupplyAuth:
                    contactNum = 3;
                    break;
            }
            SetValue(Fields.contactType.ToString(), contactNum);
            SetValue(Fields.siteID.ToString(), siteID);
            SetValue(Fields.Name.ToString(), "");
            SetValue(Fields.Email.ToString(), "");
            SetValue(Fields.Phone.ToString(), "");
            isNew = true;
        }

        public SiteContact(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = PrimaryKeys;
            isNew = false;
            base.SetTable(Table);
        }
        /// <summary>
        /// Removes the update record for the field.
        /// </summary>
        /// <param name="field"></param>
        public void ClearFieldUpdate(Fields field)
        {
            updates.Remove(field.ToString());
        }
        /// <summary>
        /// Clears the update record.
        /// </summary>
        public void ClearUpdates()
        {
            updates.Clear();
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the email for the Contact.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>0 if successful otherwise returns Message Code Number</returns>
        public int SetEmail(String email)
        {
            if (Formating.EmailAddressCheck(email))
            {
                SetValue(Fields.Email.ToString(), email);
                if (!isNew)
                {
                    ClearFieldUpdate(Fields.Email);
                    updates.Add(Fields.Email.ToString(), email);
                }
                return 0;
            }
            return 1107;
        }
        /// <summary>
        /// Returns the email address assocaited with this contact.
        /// </summary>
        /// <returns></returns>
        public String GetEmail()
        {
            if (updates.ContainsKey(Fields.Email.ToString()))
            {
                return updates[Fields.Email.ToString()].ToString();
            }
            else
            {
                return getString(Fields.Email.ToString());
            }
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Phone number for the contact.
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>0 if Successful otherwise returns the Message Code number</returns>
        public int SetPhone(String phone)
        {
            if (Formating.PhoneNumberCheck(phone))
            {
                SetValue(Fields.Phone.ToString(), phone);
                if (!isNew)
                {
                    ClearFieldUpdate(Fields.Phone);
                    updates.Add(Fields.Phone.ToString(), phone);
                }
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the Phone number associated with the contact.
        /// </summary>
        /// <returns></returns>
        public String GetPhone()
        {
            if (updates.ContainsKey(Fields.Phone.ToString()))
            {
                return updates[Fields.Phone.ToString()].ToString();
            }
            else
            {
                return getString(Fields.Phone.ToString());
            }
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the name for the contact.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>0 if successful otherwise the Message Code</returns>
        public int SetName(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.Name.ToString(), name);
                if (!isNew)
                {
                    ClearFieldUpdate(Fields.Name);
                    updates.Add(Fields.Name.ToString(), name);
                }
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the name for the contact
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            if (updates.ContainsKey(Fields.Name.ToString()))
            {
                return updates[Fields.Name.ToString()].ToString();
            }
            else
            {
                return getString(Fields.Name.ToString());
            }
        }
        /// <summary>
        /// Returns the Contact Type.
        /// </summary>
        /// <returns></returns>
        public ContactTypes GetContactType()
        {
            int type = getInt(Fields.contactType.ToString());
            switch (type)
            {
                case 0:
                    return ContactTypes.Foreman;
                case 1:
                    return ContactTypes.Supervisor1;
                case 2:
                    return ContactTypes.Supervisor2;
                default:
                    return ContactTypes.SupplyAuth;
            }
        }
        /// <summary>
        /// Returns the Site ID associated with the contact.
        /// </summary>
        /// <returns></returns>
        public String getSiteID()
        {
            return getString(Fields.siteID.ToString());
        }
    }
}
