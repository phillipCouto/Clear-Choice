using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    /// <summary>
    /// Simple Implementation of Lot object. Will be expanded soon.
    /// </summary>
    public class Lot : DataObject
    {
        /// <summary>
        /// Represents the Fields that make up the schema of the table.
        /// </summary>
        public enum Fields
        {
            lotID,
            assocID,
            Address,
            City,
            LotNumber,
            BlockNumber,
            PlanInfo,
            PermitNumber,
            ClosedDate,
            LotSize,
            ServiceSize,
            PermitDate,
            JobBC,
            HoodColour,
            Type,
            SPType,
            SPColour,
            JobTotal,
            RoughInValue,
            ServiceValue,
            FinalValue,
            Completed,
            Notes
        }

        public enum LotServices { RoughIn, Service, Final }
        public const String Table = "lots";
        public const String PrimaryKey = "lotID";

        /// <summary>
        /// Creates a new Lot object using a the lot Table schema.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        public Lot(Database db)
            : base(Table, db)
        {
            PrimaryKeyColumns = new String[1]{PrimaryKey};
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
        /// Creates a lot object using a dataset as the source for data.
        /// </summary>
        /// <param name="dataSet"></param>
        public Lot(DataSet dataSet)
            : base(dataSet)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            base.SetTable(Table);
        }

        /// <summary>
        /// Returns the Display Name for the Lot
        /// </summary>
        /// <returns></returns>
        public String LotDisplayName()
        {
            return base.getString(Fields.Address.ToString());
        }

        /// <summary>
        /// Returns a LotService that matches the referenced type.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public LotService GetLotService(Database db, LotServices service)
        {
            int serviceNum = 0;
            switch (service)
            {
                case LotServices.RoughIn:
                    serviceNum = 0;
                    break;
                case LotServices.Service:
                    serviceNum = 1;
                    break;
                case LotServices.Final:
                    serviceNum = 2;
                    break;
            }
            DataSet serviceData = db.Select("*", LotService.Table, LotService.Fields.lotID.ToString() + " = '" + GetLotID() + "' AND " + LotService.Fields.ServiceType.ToString() + " = " + serviceNum);
            if (serviceData.NumberOfRows() > 0)
            {
                serviceData.Read();
                return new LotService(serviceData.GetRecordDataSet());
            }
            return null;
        }
        /// <summary>
        /// Sets the notes for the lot.
        /// </summary>
        /// <param name="notes"></param>
        public void SetNotes(String notes)
        {
            SetValue(Fields.Notes.ToString(),notes.Replace("'","\\'"));
        }
        /// <summary>
        /// Returns the notes associated with this lot.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets if the lot is completed or not.
        /// </summary>
        /// <param name="completed"></param>
        public void SetCompleted(bool completed)
        {
            if (completed)
            {
                SetValue(Fields.Completed.ToString(), 1);
            }
            else
            {
                SetValue(Fields.Completed.ToString(), 0);
            }
        }
        /// <summary>
        /// Returns if lot is completed or not.
        /// </summary>
        /// <returns></returns>
        public bool IsCompleted()
        {
            return getInt(Fields.Completed.ToString()) == 1;
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the sp colour.
        /// </summary>
        /// <param name="colour"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetSPColour(String colour)
        {
            if (Formating.TitleCheck(colour))
            {
                SetValue(Fields.SPColour.ToString(), colour);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the SP Colour.
        /// </summary>
        /// <returns></returns>
        public String GetSPColour()
        {
            return getString(Fields.SPColour.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the SP Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetSPType(String type)
        {
            if (Formating.TitleCheck(type))
            {
                SetValue(Fields.SPType.ToString(), type);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the SP type.
        /// </summary>
        /// <returns></returns>
        public String GetSPType()
        {
            return getString(Fields.SPType.ToString());
        }
        /// <summary>
        /// Sets the lot type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetLotType(String type)
        {
            if (Formating.TitleCheck(type))
            {
                SetValue(Fields.Type.ToString(), type);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the lot type.
        /// </summary>
        /// <returns></returns>
        public String GetLotType()
        {
            return getString(Fields.Type.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the hoods colour.
        /// </summary>
        /// <param name="colour"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetHoodColour(String colour)
        {
            if (Formating.TitleCheck(colour))
            {
                SetValue(Fields.HoodColour.ToString(), colour);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the hood colour.
        /// </summary>
        /// <returns></returns>
        public String GetHoodColour()
        {
            return getString(Fields.HoodColour.ToString());
        }
        /// <summary>
        /// Sets the Jobs BC
        /// </summary>
        /// <param name="date"></param>
        public void setJobBC(DateTime date)
        {
            SetValue(Fields.JobBC.ToString(), date);
        }
        /// <summary>
        /// Returns the Jobs BC
        /// </summary>
        /// <returns></returns>
        public DateTime getJobBC()
        {
            return getDateTime(Fields.JobBC.ToString());
        }
        /// <summary>
        /// Sets the permit date.
        /// </summary>
        /// <param name="date"></param>
        public void SetPermitDate(DateTime date)
        {
            SetValue(Fields.PermitDate.ToString(), date);
        }
        /// <summary>
        /// Returns the date of the permit being issued.
        /// </summary>
        /// <returns></returns>
        public DateTime GetPermitDate()
        {
            return getDateTime(Fields.PermitDate.ToString());
        }
        /// <summary>
        /// Sets the service size for the lot.
        /// </summary>
        /// <param name="size"></param>
        public void SetServiceSize(int size)
        {
            SetValue(Fields.ServiceSize.ToString(), size);
        }
        /// <summary>
        /// Returns the service size for the lot.
        /// </summary>
        /// <returns></returns>
        public int GetServiceSize()
        {
            return getInt(Fields.ServiceSize.ToString());
        }
        /// <summary>
        /// Sets the lot size.
        /// </summary>
        /// <param name="size"></param>
        public void SetLotSize(float size)
        {
            SetValue(Fields.LotSize.ToString(), size);
        }
        /// <summary>
        /// Returns the lot size.
        /// </summary>
        /// <returns></returns>
        public float GetLotSize()
        {
            return getFloat(Fields.LotSize.ToString());
        }
        /// <summary>
        /// Sets the closed date for the lot.
        /// </summary>
        /// <param name="date"></param>
        public void SetClosedDate(DateTime date)
        {
            SetValue(Fields.ClosedDate.ToString(), date);
        }
        /// <summary>
        /// Returns the closed date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetClosedDate()
        {
            return getDateTime(Fields.ClosedDate.ToString());
        }
        /// <summary>
        /// Sets the permit date for the lot.
        /// </summary>
        /// <param name="number"></param>
        public void SetPermitNumber(int number)
        {
            SetValue(Fields.PermitNumber.ToString(), number);
        }
        /// <summary>
        /// Returns the permit number.
        /// </summary>
        /// <returns></returns>
        public int GetPermitNumber()
        {
            return getInt(Fields.PermitNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the pla info.
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetPlanInfo(String info)
        {
            if (Formating.TitleCheck(info))
            {
                SetValue(Fields.PlanInfo.ToString(), info);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the plan info for the lot.
        /// </summary>
        /// <returns></returns>
        public String GetPlanInfo()
        {
            return getString(Fields.PlanInfo.ToString());
        }
        /// <summary>
        /// Sets the block number for the lot.
        /// </summary>
        /// <param name="block"></param>
        public void SetBlockNumber(int block)
        {
            SetValue(Fields.BlockNumber.ToString(), block);
        }
        /// <summary>
        /// Returns the block number for the lot.
        /// </summary>
        /// <returns></returns>
        public int GetBlockNumber()
        {
            return getInt(Fields.BlockNumber.ToString());
        }
        /// <summary>
        /// Sets the lot number for the lot.
        /// </summary>
        /// <param name="number"></param>
        public void SetLotNumber(int number)
        {
            SetValue(Fields.LotNumber.ToString(), number);
        }
        /// <summary>
        /// Returns the lot number associated with the Lot.
        /// </summary>
        /// <returns></returns>
        public int GetLotNumber()
        {
            return getInt(Fields.LotNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if check clears sets City.
        /// </summary>
        /// <param name="city"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
        public int SetCity(String city)
        {
            if (Formating.TitleCheck(city))
            {
                SetValue(Fields.City.ToString(), city);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the site city.
        /// </summary>
        /// <returns></returns>
        public String GetCity()
        {
            return getString(Fields.City.ToString());
        }
        /// <summary>
        /// Perform a format check and if the check clears set the address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns>0 if successful otherwise returns Message Code</returns>
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
        /// Returns the address for the site.
        /// </summary>
        /// <returns></returns>
        public String GetAddress()
        {
            return getString(Fields.Address.ToString());
        }
        /// <summary>
        /// Sets the Association ID for the Lot object.
        /// </summary>
        /// <param name="id"></param>
        public void SetAssociationID(String id)
        {
            SetValue(Fields.assocID.ToString(), id);
        }
        /// <summary>
        /// Returns the Association ID for the lot which can either be a site object or Client object.
        /// </summary>
        /// <returns></returns>
        public String GetAssociationID()
        {
            return getString(Fields.assocID.ToString());
        }
        /// <summary>
        /// Returns the LotID for the lot object.
        /// </summary>
        /// <returns></returns>
        public String GetLotID()
        {
            return getString(Fields.lotID.ToString());
        }

        /// <summary>
        /// Returns the percentage to bill a client on rough in completion.
        /// </summary>
        /// <returns></returns>
        public float GetRoughInValue()
        {
            return this.getFloat(Fields.RoughInValue.ToString());
        }
        /// <summary>
        /// Sets the Rough In percentage for billing a client.
        /// </summary>
        /// <param name="value"></param>
        public void SetRoughInValue(float value)
        {
            SetValue(Fields.RoughInValue.ToString(), value);
        }
        /// <summary>
        /// Returns the percentage to bill a client on service completion.
        /// </summary>
        /// <returns></returns>
        public float GetServiceValue()
        {
            return getFloat(Fields.ServiceValue.ToString());
        }
        /// <summary>
        /// Sets the Service percentage for billing a client.
        /// </summary>
        /// <param name="value"></param>
        public void SetServiceValue(float value)
        {
            SetValue(Fields.ServiceValue.ToString(), value);
        }
        /// <summary>
        /// Returns the Final percentage used for billing a client
        /// </summary>
        /// <returns></returns>
        public float GetFinalValue()
        {
            return this.getFloat(Fields.FinalValue.ToString());
        }
        /// <summary>
        /// Sets the final percentage used for billing a client.
        /// </summary>
        /// <param name="value"></param>
        public void SetFinalValue(float value)
        {
            SetValue(Fields.FinalValue.ToString(), value);
        }
        /// <summary>
        /// Returns the Job Total quoted to client
        /// </summary>
        /// <returns></returns>
        public float GetJobTotal()
        {
            return getFloat(Fields.JobTotal.ToString());
        }
        /// <summary>
        /// Sets the job total for the lot.
        /// </summary>
        /// <param name="value"></param>
        public void SetJobTotal(float value)
        {
            SetValue(Fields.JobTotal.ToString(), value);
        }
    }
}
