using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class TimeSheet:DataObject
    {
        public enum Fields
        {
            timeID,
            lotID,
            Name,
            Date,
            Hours,
            JobCode,
            Wage,
            Notes
        }

        public const String Table = "time_sheets";
        public const String PrimaryKey = "timeID";

        public TimeSheet(String lotID)
            : base(Table, Database.Instance)
        {
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
                hashedKey = Database.Instance.GetMD5Hash(textKey);
                DataSet res = Database.Instance.Select(PrimaryKey, Table, PrimaryKey + " = '" + hashedKey + "'");
                if (res.NumberOfRows() == 0)
                {
                    tryAgain = false;
                }
            }
            SetValue(PrimaryKey, hashedKey);
            SetValue(Fields.lotID.ToString(), lotID);
        }

        public TimeSheet(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            data.SetTableName(Table);
        }
        /// <summary>
        /// Sets notes for the time sheet.
        /// </summary>
        /// <param name="notes"></param>
        public void SetNotes(String notes)
        {
            SetValue(Fields.Notes.ToString(), notes.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the notes for this time sheet.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets the wage for the employee on this sheet.
        /// </summary>
        /// <param name="wage"></param>
        public void SetWage(float wage)
        {
            SetValue(Fields.Wage.ToString(), wage);
        }
        /// <summary>
        /// Return the wage for the employee at the time of this time sheet.
        /// </summary>
        /// <returns></returns>
        public float GetWage()
        {
            return getFloat(Fields.Wage.ToString());
        }
        /// <summary>
        /// Sets the Job Code.
        /// </summary>
        /// <param name="code"></param>
        public void SetJobCode(String code)
        {
            SetValue(Fields.JobCode.ToString(), code);
        }
        /// <summary>
        /// Returns the Jobcode.
        /// </summary>
        /// <returns></returns>
        public String GetJobCode()
        {
            return getString(Fields.JobCode.ToString());
        }
        /// <summary>
        /// Sets the hours for the time sheet.
        /// </summary>
        /// <param name="hours"></param>
        public void SetHours(float hours)
        {
            SetValue(Fields.Hours.ToString(), hours);
        }
        /// <summary>
        /// Returns the hours.
        /// </summary>
        /// <returns></returns>
        public float GetHours()
        {
            return getFloat(Fields.Hours.ToString());
        }
        /// <summary>
        /// Sets the date of the Timesheet.
        /// </summary>
        /// <param name="date"></param>
        public void SetDate(DateTime date)
        {
            SetValue(Fields.Date.ToString(), date);
        }
        /// <summary>
        /// Returns the date the timesheet is intended for.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            return getDateTime(Fields.Date.ToString());
        }
        
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
        /// Returns the employee name that the timesheet is currently assigned to.
        /// </summary>
        /// <returns></returns>
        public String GetName()
        {
            return getString(Fields.Name.ToString());
        }
        /// <summary>
        /// Returns the Lot ID
        /// </summary>
        /// <returns></returns>
        public String GetLotID()
        {
            return getString(Fields.lotID.ToString());
        }
        /// <summary>
        /// Returns the Time sheet ID
        /// </summary>
        /// <returns></returns>
        public String GetTimeID()
        {
            return getString(Fields.timeID.ToString());
        }
    }
}
