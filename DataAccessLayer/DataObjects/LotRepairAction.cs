using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class LotRepairAction:DataObject
    {
        public enum Fields
        {
            actionID,
            repairID,
            ProblemArea,
            Description,
            Date,
            Time,
            Action
        }

        public const String Table = "lot_repair_actions";
        public const String PrimaryKey = "actionID";

        public LotRepairAction(String repairID)
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
            SetValue(Fields.repairID.ToString(), repairID);
        }

        public LotRepairAction(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            data.SetTableName(Table);
        }
        /// <summary>
        /// Sets the action.
        /// </summary>
        /// <param name="action"></param>
        public void SetAction(String action)
        {
            SetValue(Fields.Action.ToString(), action.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the action.
        /// </summary>
        /// <returns></returns>
        public String GetAction()
        {
            return getString(Fields.Action.ToString());
        }
        /// <summary>
        /// Sets the time for the action.
        /// </summary>
        /// <param name="time"></param>
        public void SetTime(String time)
        {
            SetValue(Fields.Time.ToString(), time);
        }
        /// <summary>
        /// Returns the Time.
        /// </summary>
        /// <returns></returns>
        public String GetTime()
        {
            return getString(Fields.Time.ToString());
        }
        /// <summary>
        /// Sets the Date.
        /// </summary>
        /// <param name="date"></param>
        public void SetDate(DateTime date)
        {
            SetValue(Fields.Date.ToString(), date);
        }
        /// <summary>
        /// Returns Date.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDate()
        {
            return getDateTime(Fields.Date.ToString());
        }
        /// <summary>
        /// Sets the description
        /// </summary>
        /// <param name="desc"></param>
        public void SetDescritpion(String desc)
        {
            SetValue(Fields.Description.ToString(), desc.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the Description
        /// </summary>
        /// <returns></returns>
        public String GetDescription()
        {
            return getString(Fields.Description.ToString());
        }
        /// <summary>
        /// Sets the problem area.
        /// </summary>
        /// <param name="area"></param>
        public void SetProblemArea(String area)
        {
            SetValue(Fields.ProblemArea.ToString(), area.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the problem area description
        /// </summary>
        /// <returns></returns>
        public String GetProblemArea()
        {
            return getString(Fields.ProblemArea.ToString());
        }
        /// <summary>
        /// Returns the repair ID
        /// </summary>
        /// <returns></returns>
        public String GetRepairID()
        {
            return getString(Fields.repairID.ToString());
        }
        /// <summary>
        /// Returns the action ID
        /// </summary>
        /// <returns></returns>
        public String GetActionID()
        {
            return getString(Fields.actionID.ToString());
        }
    }
}
