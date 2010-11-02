using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    /// <summary>
    /// An object used for sending an event to the database as part of the audit process.
    /// </summary>
    public class AuditEvent
    {
        #region Constants: Table Fields
        const String AUDITEVENTID = "auditEventID";
        const String OBJECTAFFECTED = "ObjectAffected";
        const String EVENTDESCRIPTION = "EventDescription";
        const String TRIGGEREDBY = "TriggeredBy";
        const String TIMEOFEVENT = "TimeOfEvent";
        #endregion

        private DataSet data;
        private Database db;

        /// <summary>
        /// Create and Audit Event Object
        /// </summary>
        /// <param name="db"></param>
        public AuditEvent(Database db)
        {
            data = new DataSet(db, "audit_events", 1);
            this.db = db;
        }
        /// <summary>
        /// Define the data for the Event object
        /// </summary>
        /// <param name="ObjAffected"></param>
        /// <param name="EventDesc"></param>
        public void EventDescription(String ObjAffected, String EventDesc)
        {
            data.SetValue(0, OBJECTAFFECTED, ObjAffected);
            data.SetValue(0,EVENTDESCRIPTION, EventDesc.Replace("\\'","'").Replace("'","\\'"));
            data.SetValue(0, TRIGGEREDBY, System.Environment.UserName);
            data.SetValue(0, TIMEOFEVENT, DateTime.Now);
        }
        /// <summary>
        /// Save the Event Object to the Database
        /// </summary>
        public void SaveEvent()
        {
            db.Insert(data.GetTableName(),data);
        }
    }
}
