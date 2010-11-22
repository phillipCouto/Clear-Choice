using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class LotService : DataObject
    {
        public enum Fields
        {
            lotID,
            ServiceType,
            CalledIn,
            Passed,
            Billed,
            Amount,
            Notes
        }
        public const String Table = "lot_services";
        public const String PrimaryKey = "lotID";
        public readonly String[] PrimaryKeys = new String[2] { "lotID", "ServiceType" };

        public LotService(Database db,Lot.LotServices serviceType,String lotID)
            : base(Table, db)
        {
            PrimaryKeyColumns = PrimaryKeys;
            int serviceNum = 0;
            switch (serviceType)
            {
                case Lot.LotServices.RoughIn:
                    serviceNum = 0;
                    break;
                case Lot.LotServices.Service:
                    serviceNum = 1;
                    break;
                case Lot.LotServices.Final:
                    serviceNum = 2;
                    break;
            }
            SetValue(Fields.ServiceType.ToString(), serviceNum);
            SetValue(Fields.lotID.ToString(), lotID);
        }

        public LotService(DataSet data): base(data)
        {
            PrimaryKeyColumns = PrimaryKeys;
            base.SetTable(Table);
        }

        /// <summary>
        /// Sets the amount billed to the client for the service.
        /// </summary>
        /// <param name="amount"></param>
        public void SetAmount(float amount)
        {
            SetValue(Fields.Amount.ToString(), amount);
        }
        /// <summary>
        /// Returns the amount billed to the client for the service.
        /// </summary>
        /// <returns></returns>
        public float GetAmount()
        {
            return getFloat(Fields.Amount.ToString());
        }
        /// <summary>
        /// Sets the billed date.
        /// </summary>
        /// <param name="date"></param>
        public void SetBilled(DateTime date)
        {
            SetValue(Fields.Billed.ToString(), date);
        }
        /// <summary>
        /// Returns the billed date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetBilled()
        {
            return getDateTime(Fields.Billed.ToString());
        }
        /// <summary>
        /// Sets the notes for the service.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public void SetNotes(String notes)
        {
                SetValue(Fields.Notes.ToString(), notes.Replace("'","\\'"));
        }
        /// <summary>
        /// Returns the notes for the Service.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets the passing date.
        /// </summary>
        /// <param name="date"></param>
        public void SetPassed(DateTime date)
        {
            SetValue(Fields.Passed.ToString(), date);
        }
        /// <summary>
        /// Returns the date of when the service passed.
        /// </summary>
        /// <returns></returns>
        public DateTime GetPassed()
        {
            return getDateTime(Fields.Passed.ToString());
        }
        /// <summary>
        /// Sets the called in date.
        /// </summary>
        /// <param name="date"></param>
        public void SetCalledIn(DateTime date)
        {
            SetValue(Fields.CalledIn.ToString(), date);
        }
        /// <summary>
        /// Returns the called in date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetCalledIn()
        {
            return getDateTime(Fields.CalledIn.ToString());
        }
        /// <summary>
        /// Returns the LotID for this service.
        /// </summary>
        /// <returns></returns>
        public String GetLotID()
        {
            return getString(Fields.lotID.ToString());
        }
        /// <summary>
        /// Returns the ServiceType for this Service.
        /// </summary>
        /// <returns></returns>
        public Lot.LotServices GetServiceType()
        {
            switch (getInt(Fields.ServiceType.ToString()))
            {                    
                case 1:
                    return Lot.LotServices.Service;
                case 2:
                    return Lot.LotServices.Final;
                default:
                    return Lot.LotServices.RoughIn;
            }
        }
    }
}
