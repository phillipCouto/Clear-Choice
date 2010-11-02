using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.DatabaseObjects;

namespace DataAccessLayer.DataObjects
{
    public class InventoryPackageItem : DataObject
    {
        public enum Fields {packageID,itemID,Quantity}

        public const String Table = "";
        public String[] PrimaryKeys = null;

        public InventoryPackageItem(Database db,String PackageID,String ItemID): base(Table,db)
        {
            PrimaryKeys = new String[2]{"packageID","itemID"};
            uniqueIDColumn = PrimaryKeys;
            SetValue(Fields.itemID.ToString(), ItemID);
            SetValue(Fields.packageID.ToString(), PackageID);
        }
        public InventoryPackageItem(DataSet data)
            : base(data)
        {
            PrimaryKeys = new String[2] { "packageID", "itemID" };
            uniqueIDColumn = PrimaryKeys;
        }
        /// <summary>
        /// Sets the quantity for this packaged item.
        /// </summary>
        /// <param name="quantity"></param>
        public void setQuantity(int quantity)
        {
            setValue(Fields.Quantity.ToString(), quantity);
        }
        /// <summary>
        /// Returns the quantity associated with this packaged item.
        /// </summary>
        /// <returns></returns>
        public int getQuantity()
        {
            return getInt(Fields.Quantity.ToString());
        }
        /// <summary>
        /// Returns the ItemID associated with this packaged item.
        /// </summary>
        /// <returns></returns>
        public String getItemID()
        {
            return getString(Fields.itemID.ToString());
        }
        /// <summary>
        /// Returns the package ID associated with this packaged item.
        /// </summary>
        /// <returns></returns>
        public String getPackageID()
        {
            return getString(Fields.packageID.ToString());
        }
    }
}
