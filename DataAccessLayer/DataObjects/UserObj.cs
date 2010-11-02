using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DatabaseObjects;

namespace DataAccessLayer.DataObjects
{
    /// <summary>
    /// A User object used for storing information in regards to the currently logged in user.
    /// </summary>
    public class UserObj : DataObject
    {
        #region Constants: Table Fields
        public const String UID = "uid";
        public const String USERNAME = "username";
        public const String PASSWORD = "password";
        #endregion

        public ACS AccessControlList;
        /// <summary>
        /// Creates a new User object with a fresh ACL
        /// </summary>
        /// <param name="db"></param>
        public UserObj(Database db)
            : base("Users", db)
        {
            //Set the unqiue ID column for the Object
            uniqueIDColumn = UID;
            //Generate random number for primary Key
            Random ranNum = new Random();
            Boolean tryAgain = true;
            int id = 0;
            while (tryAgain)
            {
                id = ranNum.Next(Int32.MaxValue);
                DataSet res = db.Select(uniqueIDColumn, "Users", uniqueIDColumn + " = " + id);
                if (res.NumberOfRows() == 0)
                {
                    tryAgain = false;
                }
            }
            SetValue(uniqueIDColumn, id);
            AccessControlList = new ACS();
        }

        /// <summary>
        /// Loads an existing User Object from the Dataset
        /// </summary>
        /// <param name="dataSet"></param>
        public UserObj(DataSet dataSet)
            : base(dataSet)
        {
            uniqueIDColumn = UID;
            AccessControlList = new ACS(this.getString("ACL"));            
        }

        /// <summary>
        /// Saves the Object to database while updating the ACL.
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public new Boolean SaveObject(Database db)
        {
            this.SetValue("ACL", AccessControlList.ACLString());
            return base.SaveObject(db);
        }

        public static UserObj Login(Database db, String username, String password)
        {
            DataSet data = db.Select("*", "Users", USERNAME + " = '" + username + "' AND " + PASSWORD + " = '" + password + "'");
            if (data.NumberOfRows() > 0)
            {
                return new UserObj(data);
            }
            else
            {
                return null;
            }
        }
    }
}
