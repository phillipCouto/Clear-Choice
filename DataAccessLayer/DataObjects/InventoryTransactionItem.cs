using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class InventoryTransactionItem : DataObject
    {
        public enum Fields
        {
            transactionID,
            itemID,
            Quantity,
            UnitPrice
        }

        public const String Table = "inventory_item_transactions";
        public static readonly String [] PrimaryKeys = new String[2] { "transactionID", "itemID" };

        private InventoryItem item = null;

        public InventoryTransactionItem(String transactionID, String itemID):base(Table,Database.Instance)
        {
            PrimaryKeyColumns = PrimaryKeys;
            SetValue(Fields.transactionID.ToString(), transactionID);
            SetValue(Fields.itemID.ToString(), itemID);
        }
        public InventoryTransactionItem(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = PrimaryKeys;
            base.SetTable(Table);
        }
        /// <summary>
        /// Sets the InventoryItem for temporary use only.
        /// </summary>
        /// <param name="pItem"></param>
        public void SetInventoryItem(InventoryItem pItem)
        {
            item = pItem;
        }
        /// <summary>
        /// Returns the IventoryItem in relation to this transaction.
        /// </summary>
        /// <returns></returns>
        public InventoryItem GetInventoryItem()
        {
            return item;
        }
        /// <summary>
        /// Sets the Unit Price
        /// </summary>
        /// <param name="amount"></param>
        public void SetUnitPrice(float amount)
        {
            SetValue(Fields.UnitPrice.ToString(), amount);
        }
        /// <summary>
        /// Returns the Unit Price
        /// </summary>
        /// <returns></returns>
        public float GetUnitPrice()
        {
            return getFloat(Fields.UnitPrice.ToString());
        }
        /// <summary>
        /// Sets the Quantity.
        /// </summary>
        /// <param name="quantity"></param>
        public void SetQuantity(int quantity)
        {
            SetValue(Fields.Quantity.ToString(),quantity);
        }
        /// <summary>
        /// Returns the Quantity
        /// </summary>
        /// <returns></returns>
        public int GetQuantity()
        {
            return getInt(Fields.Quantity.ToString());
        }
        /// <summary>
        /// Return the Item ID
        /// </summary>
        /// <returns></returns>
        public String GetItemID()
        {
            return getString(Fields.itemID.ToString());
        }
        /// <summary>
        /// Return the Transaction ID
        /// </summary>
        /// <returns></returns>
        public String GetTransactionID()
        {
            return getString(Fields.transactionID.ToString());
        }
    }
}
