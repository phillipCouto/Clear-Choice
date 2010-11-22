using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class LotExtra : DataObject
    {
        public enum Fields
        {
            extraID,
            lotID,
            Quantity,
            BilledQuantity,
            PO,
            ExtraItem,
            Location,
            Invoice,
            BilledDate,
            UnitPrice,
            TotalPrice,
            Notes
        }

        public const String Table = "lot_extras";
        public const String PrimaryKey = "extraID";

        public LotExtra(Database db)
            : base(Table, db)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            Boolean tryAgain = true;
            String hashedKey = null;
            while (tryAgain)
            {
                hashedKey = "" + DateTime.Now.Ticks;
                DataSet res = db.Select(PrimaryKey, Table, PrimaryKey + " = '" + hashedKey + "'");
                if (res.NumberOfRows() == 0)
                {
                    tryAgain = false;
                }
            }
            SetValue(PrimaryKey, hashedKey);
        }

        public LotExtra(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            base.SetTable(Table);
        }

        /// <summary>
        /// Sets the notes for the extra.
        /// </summary>
        /// <param name="notes"></param>
        public void SetNotes(String notes)
        {
            SetValue(Fields.Notes.ToString(), notes.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns Notes associated with this extra.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets the total price for the extra.
        /// </summary>
        /// <param name="amount"></param>
        public void SetTotalPrice(float amount)
        {
            SetValue(Fields.TotalPrice.ToString(), amount);
        }
        /// <summary>
        /// Returns the total price for the Extra
        /// </summary>
        /// <returns></returns>
        public float GetTotalPrice()
        {
            return getFloat(Fields.TotalPrice.ToString());
        }
        /// <summary>
        /// Sets the price of the extra.
        /// </summary>
        /// <param name="price"></param>
        public void SetUnitPrice(float price)
        {
            SetValue(Fields.UnitPrice.ToString(), price);
        }
        /// <summary>
        /// Returns the price set on the extra.
        /// </summary>
        /// <returns></returns>
        public float GetUnitPrice()
        {
            return getFloat(Fields.UnitPrice.ToString());
        }
        /// <summary>
        /// Sets the billed date for the extra.
        /// </summary>
        /// <param name="date"></param>
        public void SetBilledDate(DateTime date)
        {
            SetValue(Fields.BilledDate.ToString(), date);
        }
        /// <summary>
        /// Returns the billed date of the extra.
        /// </summary>
        /// <returns></returns>
        public DateTime GetBilledDate()
        {
            return getDateTime(Fields.BilledDate.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the invoice number.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int SetInvoice(String invoice)
        {
            if (Formating.ItemIDCheck(invoice))
            {
                SetValue(Fields.Invoice.ToString(), invoice);
                return 0;
            }
            return 1104;
        }
        /// <summary>
        /// Returns the invoice number associated with this extra.
        /// </summary>
        /// <returns></returns>
        public String GetInvoice()
        {
            return getString(Fields.Invoice.ToString());
        }
        /// <summary>
        /// Sets the location for the extra.
        /// </summary>
        /// <param name="location"></param>
        public void SetLocation(String location)
        {
            SetValue(Fields.Location.ToString(), location.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the Location for this extra
        /// </summary>
        /// <returns></returns>
        public String GetLocation()
        {
            return getString(Fields.Location.ToString());
        }
        /// <summary>
        /// Sets the Extra Item for the extra
        /// </summary>
        /// <param name="desc"></param>
        public void SetExtraItem(String extra)
        {
            SetValue(Fields.ExtraItem.ToString(), extra.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the Extra Item
        /// </summary>
        /// <returns></returns>
        public String GetExtraItem()
        {
            return getString(Fields.ExtraItem.ToString());
        }
        /// <summary>
        /// Sets the PO for this Extra
        /// </summary>
        /// <param name="po"></param>
        public void SetPO(String po)
        {
            SetValue(Fields.PO.ToString(), po.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the PO for this extra.
        /// </summary>
        /// <returns></returns>
        public String GetPO()
        {
            return getString(Fields.PO.ToString());
        }
        /// <summary>
        /// Sets the billed quantity for this extra.
        /// </summary>
        /// <param name="amount"></param>
        public void SetBilledQuantity(int amount)
        {
            SetValue(Fields.BilledQuantity.ToString(), amount);
        }
        /// <summary>
        /// Returns the billed quantity for this extra.
        /// </summary>
        /// <returns></returns>
        public int GetBilledQuantity()
        {
            return getInt(Fields.BilledQuantity.ToString());
        }
        /// <summary>
        /// Sets the quantity of this extra
        /// </summary>
        /// <param name="amount"></param>
        public void SetQuantity(int amount)
        {
            SetValue(Fields.Quantity.ToString(), amount);
        }
        /// <summary>
        /// Returns the quantity of this extra.
        /// </summary>
        /// <returns></returns>
        public int GetQuantity()
        {
            return getInt(Fields.Quantity.ToString());
        }
        /// <summary>
        /// Sets the lot id for this extra.
        /// </summary>
        /// <param name="id"></param>
        public void SetLotID(String id)
        {
            SetValue(Fields.lotID.ToString(), id);
        }
        /// <summary>
        /// Returns the ID of the lot associated with the extra.
        /// </summary>
        /// <returns></returns>
        public String GetLotID()
        {
            return getString(Fields.lotID.ToString());
        }
        /// <summary>
        /// Returns the extras ID number
        /// </summary>
        /// <returns></returns>
        public String GetExtraID()
        {
            return getString(Fields.extraID.ToString());
        }
    }
}
