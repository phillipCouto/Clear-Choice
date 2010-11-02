using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class LotRepair: DataObject
    {
        public enum Fields
        {
            repairID,
            lotID,
            WorkOrder,
            HomeOwnerName,
            HomeNumber,
            AltNumber,
            Email,
            DateOfAppointment,
            WindowOfAppointment,
            RequestedBy,
            InspectionPassed,
            SourceCode,
            Notes,
            
        }

        public const String Table = "lot_repairs";
        public const String PrimaryKey = "repairID";

        public LotRepair(String lotID)
            : base(Table,Database.Instance)
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

        public LotRepair(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            data.SetTableName(Table);
        }
        /// <summary>
        /// Sets the notes.
        /// </summary>
        /// <param name="notes"></param>
        public void SetNotes(String notes)
        {
            SetValue(Fields.Notes.ToString(), notes);
        }
        /// <summary>
        /// Returns the Notes.
        /// </summary>
        /// <returns></returns>
        public String GetNotes()
        {
            return getString(Fields.Notes.ToString());
        }
        /// <summary>
        /// Sets the source code.
        /// </summary>
        /// <param name="code"></param>
        public void SetSourceCode(String code)
        {
            SetValue(Fields.SourceCode.ToString(), code.Replace("'", "\\'"));
        }
        /// <summary>
        /// Returns the source code.
        /// </summary>
        /// <returns></returns>
        public String GetSourceCode()
        {
            return getString(Fields.SourceCode.ToString());
        }
        /// <summary>
        /// Sets the Inspection Passed Date.
        /// </summary>
        /// <param name="date"></param>
        public void SetInspectionPassed(DateTime date)
        {
            SetValue(Fields.InspectionPassed.ToString(), date);
        }
        /// <summary>
        /// Returns the date the inspected was passed.
        /// </summary>
        /// <returns></returns>
        public DateTime GetInspectionPassed()
        {
            return getDateTime(Fields.InspectionPassed.ToString());
        }
        /// <summary>
        /// Performs a format check and if check clears sets Requested By
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int SetRequestedBy(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.RequestedBy.ToString(), name);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns who requested the repair.
        /// </summary>
        /// <returns></returns>
        public String GetRequestedBy()
        {
            return getString(Fields.RequestedBy.ToString());
        }
        /// <summary>
        /// Sets the Window of the Appointment.
        /// </summary>
        /// <param name="window"></param>
        public void SetWindowOfAppointment(String window)
        {
            SetValue(Fields.WindowOfAppointment.ToString(),window.Replace("'","\\'"));
        }
        /// <summary>
        /// Returns the Window of the Appointment
        /// </summary>
        /// <returns></returns>
        public String GetWindowOfAppointment()
        {
            return getString(Fields.WindowOfAppointment.ToString());
        }
        /// <summary>
        /// Sets the date of the Appointment.
        /// </summary>
        /// <param name="date"></param>
        public void SetDateOfAppointment(DateTime date)
        {
            SetValue(Fields.DateOfAppointment.ToString(), date);
        }
        /// <summary>
        /// Returns the Date of the Appointment.
        /// </summary>
        /// <returns></returns>
        public DateTime GetDateOfAppointment()
        {
            return getDateTime(Fields.DateOfAppointment.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int SetEmail(String email)
        {
            if (Formating.EmailAddressCheck(email))
            {
                SetValue(Fields.Email.ToString(), email);
                return 0;
            }
            return 1107;
        }
        /// <summary>
        /// Returns the Email.
        /// </summary>
        /// <returns></returns>
        public String GetEmail()
        {
            return getString(Fields.Email.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Alternate Number
        /// </summary>
        /// <param name="number"></param>
        /// <returns>0 if success otherwise returns Message Code</returns>
        public int SetAltNumber(String number)
        {
            if (Formating.PhoneNumberCheck(number))
            {
                SetValue(Fields.AltNumber.ToString(), number);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Return the Alternate Number
        /// </summary>
        /// <returns></returns>
        public String GetAltNumber()
        {
            return getString(Fields.AltNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the home number.
        /// </summary>
        /// <param name="number"></param>
        /// <returns>0 if success otherwise returns Message Code</returns>
        public int SetHomeNumber(String number)
        {
            if (Formating.PhoneNumberCheck(number))
            {
                SetValue(Fields.HomeNumber.ToString(), number);
                return 0;
            }
            return 1105;
        }
        /// <summary>
        /// Returns the home phone number.
        /// </summary>
        /// <returns></returns>
        public String GetHomeNumber()
        {
            return getString(Fields.HomeNumber.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Owner Name.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns>0 if success otherwise returns Message Code</returns>
        public int SetOwnerName(String owner)
        {
            if (Formating.TitleCheck(owner))
            {
                SetValue(Fields.HomeOwnerName.ToString(), owner);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the Home Owners name.
        /// </summary>
        /// <returns></returns>
        public String GetOwnerName()
        {
            return getString(Fields.HomeOwnerName.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the work order number.
        /// </summary>
        /// <param name="wo"></param>
        /// <returns>0 if success otherwise the Message Code.</returns>
        public int SetWorkOrder(String wo)
        {
            if (Formating.ItemIDCheck(wo))
            {
                SetValue(Fields.WorkOrder.ToString(), wo);
                return 0;
            }
            return 1108;
        }
        /// <summary>
        /// Returns the work order number.
        /// </summary>
        /// <returns></returns>
        public String GetWorkOrder()
        {
            return getString(Fields.WorkOrder.ToString());
        }
        /// <summary>
        /// Returns the lot ID associated with the Repair.
        /// </summary>
        /// <returns></returns>
        public String GetLotID()
        {
            return getString(Fields.lotID.ToString());
        }
        /// <summary>
        /// Returns the Repair ID.
        /// </summary>
        /// <returns></returns>
        public String GetRepairID()
        {
            return getString(Fields.repairID.ToString());
        }
    }
}
