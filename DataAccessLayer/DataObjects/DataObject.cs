using System;
using System.Collections;
using System.Text;
using System.Windows;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    /// <summary>
    /// Dataobject is a Generic Class used for housing basic data structure
    /// </summary>
    public class DataObject
    {
        const String LASTMODIFIED = "lastModified";
        const String MODIFIEDBY = "modifiedBy";

        protected DataSet data;
        private bool newObject;
        private Hashtable updates;
        protected String[] PrimaryKeyColumns;

        /// <summary>
        /// Creates a DataObject Class with an empty set of data
        /// </summary>
        /// <param name="table"></param>
        /// <param name="db"></param>
        public DataObject(String table, Database db)
        {
            newObject = true;
            data = new DataSet(db, table, 1);
        }
        /// <summary>
        /// Creates a DataObject Class with existing data
        /// </summary>
        /// <param name="objData"></param>
        public DataObject(DataSet objData)
        {
            newObject = false;
            data = objData;
            updates = new Hashtable();
        }

        /// <summary>
        /// Saves the Dataobject to the Database
        /// </summary>
        /// <param name="IndexColumn"></param>
        /// <param name="db"></param>
        public void SaveObject(Database db)
        {
            this.SetValue(LASTMODIFIED, DateTime.Now);
            this.SetValue(MODIFIEDBY, System.Environment.UserName);
            StringBuilder whereBuilder = new StringBuilder();
            bool first = true;
            foreach (String column in PrimaryKeyColumns)
            {
                if (!first)
                {
                    whereBuilder.Append(" AND ");
                }
                else
                {
                    first = false;
                }
                whereBuilder.Append("`" + column + "` = ");
                if (data.IsColumnNumberDataType(column))
                {
                    whereBuilder.Append(data.getString(column));
                }
                else
                {
                    whereBuilder.Append("'" + data.getString(column) + "'");
                }
            }
            if (newObject)
            {
                try
                {
                    db.Insert(data.GetTableName(), data);
                    newObject = false;
                    updates = new Hashtable();
                    //Parse an array of UniqueID Columns
                    StringBuilder description = new StringBuilder();
                    first = true;
                    foreach (String column in PrimaryKeyColumns)
                    {
                        if (!first)
                        {
                            description.Append(" and ");
                        }
                        else
                        {
                            first = false;
                        }
                        description.Append(column + " is " + data.getString(column));
                    }
                    //Submit an audit event
                    AuditEvent auditEvent = new AuditEvent(db);
                    auditEvent.EventDescription(data.GetTableName(), "New Object added, " + description.ToString());
                    auditEvent.SaveEvent();
                }
                catch (Exception e)
                {
                    Console.Write(e.StackTrace);
                    throw new Exception("Database Error, Audit log failed.", e);
                }
            }
            else
            {
                StringBuilder setStr = new StringBuilder();
                bool firstKey = true;
                foreach (Object key in updates.Keys)
                {
                    if (!firstKey)
                    {
                        setStr.Append(",");
                    }
                    else
                    {
                        firstKey = false;
                    }
                    setStr.Append("`" + key.ToString() + "` = ");
                    if (data.IsColumnNumberDataType(key.ToString()))
                    {
                        setStr.Append(updates[key].ToString());
                    }
                    else if (updates[key].Equals(DBNull.Value))
                    {
                        setStr.Append("NULL");
                    }
                    else
                    {
                        setStr.Append("'" + updates[key].ToString() + "'");
                    }
                }
                DataSet check = db.Select(LASTMODIFIED + "," + MODIFIEDBY, data.GetTableName(), whereBuilder.ToString());
                MessageBoxResult askBox;
                if (!check.getDateTime(LASTMODIFIED).Equals(data.getDateTime(LASTMODIFIED)))
                {
                    askBox = MessageBox.Show(data.GetTableName() + " Object has already been modified by " + check.getString(MODIFIEDBY) + " since you have opened this object. Continue with operation?", "Consistency Error", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                    if (askBox == MessageBoxResult.Yes)
                    {
                        db.Update(data.GetTableName(), setStr.ToString(), whereBuilder.ToString());
                        UpdateAuditEvent(db);
                        data = db.Select("*", data.GetTableName(), whereBuilder.ToString());

                    }
                    else
                    {
                        throw new Exception("Database Error, Object was out of sync. Reload Object.");
                    }
                }
                else
                {
                    db.Update(data.GetTableName(), setStr.ToString(), whereBuilder.ToString());
                    UpdateAuditEvent(db);

                }
                updates.Clear();
            }
            data = db.Select("*", data.GetTableName(), whereBuilder.ToString());
        }
        /// <summary>
        /// If this Data object is not a new object the DataObject will rebuild the where statement and reload it's dataset.
        /// </summary>
        public void ReloadObject()
        {
            if (!newObject)
            {
                StringBuilder whereBuilder = new StringBuilder();
                bool first = true;
                foreach (String column in PrimaryKeyColumns)
                {
                    if (!first)
                    {
                        whereBuilder.Append(" AND ");
                    }
                    else
                    {
                        first = false;
                    }
                    whereBuilder.Append("`" + column + "` = ");
                    if (data.IsColumnNumberDataType(column))
                    {
                        whereBuilder.Append(data.getString(column));
                    }
                    else
                    {
                        whereBuilder.Append("'" + data.getString(column) + "'");
                    }
                }
                data = Database.Instance.Select("*", data.GetTableName(), whereBuilder.ToString());
            }
        }

        private void UpdateAuditEvent(Database db)
        {
            StringBuilder eventDesc = new StringBuilder();
            eventDesc.Append("Object was updated, ");
            Boolean first = true;
            foreach (Object key in updates.Keys)
            {
                if (!key.ToString().Equals(LASTMODIFIED) & !key.ToString().Equals(MODIFIEDBY))
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        eventDesc.Append(", ");
                    }
                    eventDesc.Append(key.ToString() + " From \"" + data.getString(key.ToString()) + "\" to \"" + updates[key].ToString() + "\"");
                }
            }
            AuditEvent auditEvent = new AuditEvent(db);
            auditEvent.EventDescription(data.GetTableName(), eventDesc.ToString());
            auditEvent.SaveEvent();
        }

        /// <summary>
        /// Sets the table for the dataobject. Used for overriding Joins.
        /// </summary>
        /// <param name="table"></param>
        protected void SetTable(String table)
        {
            data.SetTableName(table);
        }

        #region Get Methods
        /// <summary>
        /// Gets an Integer from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected int getInt(String column)
        {
            try
            {
                return data.getInt(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a Long from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected long getLong(String column)
        {
            try
            {
                return data.getLong(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a Float from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected float getFloat(String column)
        {
            try
            {
                return data.getFloat(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a Double from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected double getDouble(String column)
        {
            try
            {
                return data.getDouble(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a String from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected String getString(String column)
        {
            try
            {
                return data.getString(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a boolean from the Object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected bool getBoolean(String column)
        {
            try
            {
                return data.getBoolean(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a DateTime object
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected DateTime getDateTime(String column)
        {
            try
            {
                return data.getDateTime(column);
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        #endregion

        #region Set Methods
        /// <summary>
        /// Sets field to be updated.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        private void setValue(String column, Object value)
        {
            if (newObject)
            {
                data.SetCellValue(0, Int32.Parse(data.ReturnFieldInfo(DataSet.FIELDTITLES)[column].ToString()), value);
            }
            else
            {
                if (!data.getObject(column).Equals(value))
                {
                    if (updates.Contains(column))
                    {
                        updates.Remove(column);
                    }
                    updates.Add(column, value);
                }
            }
        }
        /// <summary>
        /// Sets 32 bit integer value for column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, int value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(Int32)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store 32 bit integer.");
            }
        }
        /// <summary>
        /// Sets a 64 bit value for Column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, long value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(Int64)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store 64 bit integer.");
            }
        }
        /// <summary>
        /// Sets a float value for Column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, float value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(Single)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store floating point numbers.");
            }
        }
        /// <summary>
        /// Sets a Double value for Column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, double value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(Double)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store double floating point numbers.");
            }
        }
        /// <summary>
        /// Sets String value for Column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, String value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(String)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store Strings.");
            }
        }
        /// <summary>
        /// Sets boolean value for column.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, bool value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(bool)))
            {
                setValue(column, value);
            }
            else
            {
                throw new FormatException("Column does not Store boolean values.");
            }
        }
        /// <summary>
        /// Sets DateTime value for the Column
        /// </summary>
        /// <param name="column"></param>
        /// <param name="value"></param>
        protected void SetValue(String column, DateTime value)
        {
            if (data.GetColoumnDataType(column).Equals(typeof(DateTime)))
            {
                setValue(column, value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                throw new FormatException("Column does not Store DateTime objects.");
            }
        }
        /// <summary>
        /// Clears the column of data.
        /// </summary>
        /// <param name="column"></param>
        public void ClearField(String column)
        {
            if (data.IsColumnNumberDataType(column))
            {
                setValue(column, 0);
            }
            else
            {
                if (newObject)
                {
                    setValue(column, null);
                }
                else
                {
                    setValue(column, DBNull.Value);
                }
            }
        }
        #endregion
    }
}
