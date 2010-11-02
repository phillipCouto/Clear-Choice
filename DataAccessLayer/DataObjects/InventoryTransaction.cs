using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class InventoryTransaction : DataObject
    {
        public enum Fields
        {
            transactionID,
            assocID,
            ClientType,
            DateOfTransaction,
            TotalValue,
            TotalQuantity
        }
        public enum ClientType
        {
            Builder,Client,Inventory
        }
        public const String Table = "inventory_transactions";
        public const String PrimaryKey = "transactionID";

        public InventoryTransaction(String assocID)
            : base(Table, Database.Instance)
        {
            //Set the unqiue ID column for the Object
            PrimaryKeyColumns = new String[1];
            PrimaryKeyColumns[0] = PrimaryKey;
            //Generate random number for primary Key
            Random ranNum = new Random();
            Boolean tryAgain = true;
            int id = 0;
            int letterMin = 65;
            StringBuilder Key = null;
            while (tryAgain)
            {
                Key = new StringBuilder();
                id = ranNum.Next(25);
                Key.Append(Convert.ToChar(letterMin+id));
                id = ranNum.Next(25);
                Key.Append(Convert.ToChar(letterMin + id));
                id = ranNum.Next(25);
                Key.Append(Convert.ToChar(letterMin + id));
                id = ranNum.Next(9);
                Key.Append(id);
                id = ranNum.Next(9);
                Key.Append(id);
                id = ranNum.Next(9);
                Key.Append(id);
                id = ranNum.Next(9);
                Key.Append(id);
                DataSet res = Database.Instance.Select(PrimaryKey, Table, PrimaryKey + " = '" + Key.ToString() + "'");
                if (res.NumberOfRows() == 0)
                {
                    tryAgain = false;
                }
            }
            SetValue(PrimaryKey, Key.ToString());
            SetValue(Fields.assocID.ToString(), assocID);
        }
        public InventoryTransaction(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            SetTable(Table);
        }
        
        /// <summary>
        /// Sets the total quantity for the transaction.
        /// </summary>
        /// <param name="amount"></param>
        public void SetTotalQuantity(int amount)
        {
            SetValue(Fields.TotalQuantity.ToString(), amount);
        }
        /// <summary>
        /// Returns the total Quantity for the Transaction
        /// </summary>
        /// <returns></returns>
        public int GetTotalQuantity()
        {
            return getInt(Fields.TotalQuantity.ToString());
        }
        /// <summary>
        /// Sets the Total Value for the Transaction
        /// </summary>
        /// <param name="value"></param>
        public void SetTotalValue(float value)
        {
            SetValue(Fields.TotalValue.ToString(), value);
        }
        /// <summary>
        /// Returns the Total Value of the Transaction.
        /// </summary>
        /// <returns></returns>
        public float GetTotalValue()
        {
            return getFloat(Fields.TotalValue.ToString());
        }
        /// <summary>
        /// Sets the Date of the Transaction
        /// </summary>
        /// <param name="date"></param>
        public void SetDateOfTransaction(DateTime date)
        {
            SetValue(Fields.DateOfTransaction.ToString(), date);
        }
        /// <summary>
        /// Returns the Date of the Transaction
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateOfTransaction()
        {
            return getDateTime(Fields.DateOfTransaction.ToString());
        }
        /// <summary>
        /// Sets the client type for the transaction.
        /// </summary>
        /// <param name="type"></param>
        public void SetClientType(ClientType type)
        {
            switch (type)
            {
                case ClientType.Builder:
                    SetValue(Fields.ClientType.ToString(), 0);
                    break;
                case ClientType.Client:
                    SetValue(Fields.ClientType.ToString(), 1);
                    break;
                default:
                    SetValue(Fields.ClientType.ToString(), 2);
                    break;
            }
        }
        /// <summary>
        /// Returns the Client type for the transaction.
        /// </summary>
        /// <returns></returns>
        public ClientType GetClientType()
        {
            switch (getInt(Fields.ClientType.ToString()))
            {
                case 0:
                    return ClientType.Builder;
                case 1:
                    return ClientType.Client;
                default:
                    return ClientType.Inventory;
            }
        }
        /// <summary>
        /// Returns the Associated ID
        /// </summary>
        /// <returns></returns>
        public String GetAssocID()
        {
            return getString(Fields.assocID.ToString());
        }
        /// <summary>
        /// Returns the Transaction ID
        /// </summary>
        /// <returns></returns>
        public String GetTransactionID()
        {
            return getString(Fields.transactionID.ToString());
        }
    }
}
