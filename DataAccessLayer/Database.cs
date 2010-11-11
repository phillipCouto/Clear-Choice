using System;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Resources;
using System.Threading;
using Stemstudios.DataAccessLayer.DataObjects;

namespace Stemstudios.DataAccessLayer
{
    /// <summary>
    /// This object provides an interface between the Database and the application.
    /// It reduces the amount of SQL syntax in the application itself and provides objects for manipulating data.
    /// </summary>
    public class Database : IDisposable
    {
        #region Variable Definition
        private MySqlConnectionStringBuilder connStr = new MySqlConnectionStringBuilder();
        private MySqlConnection conn;
        private MD5 md5Hasher = MD5.Create();
        static readonly Database instance = new Database();
        private Thread bgThread;
        private const int KeepAliveTime = 5;
        #endregion

        static Database()
        {
            //instance.ConfigureConnection();
        }
        Database()
        {
            ConfigureConnection();
        }
        /// <summary>
        /// Configures the database instance with the settings set in the Resource File.
        /// </summary>
        private void ConfigureConnection()
        {

            connStr.Add("server", ConnInfo.Server);
            connStr.Add("user", ConnInfo.Username);
            connStr.Add("database", ConnInfo.Database);
            connStr.Add("port", ConnInfo.Port);
            connStr.Add("password", ConnInfo.Password);
            conn = new MySqlConnection(connStr.GetConnectionString(true));
            bgThread = new Thread(KeepAlive);
            bgThread.SetApartmentState(ApartmentState.STA);
        }
        /// <summary>
        /// The instance of the Database object.
        /// </summary>
        public static Database Instance
        {
            get { return instance; }
        }
        
        #region Select Command and all its overloads
        /// <summary>
        /// Executes a SELECT command with sorting and limiting number of results which returns a DataSet 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="from"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DataSet Select(String fields, String from, String where, String orderBy, String limit)
        {
            String cmdStr = "SELECT " + fields + " FROM " + from;
            if (where != null)
            {
                cmdStr += " WHERE " + where;
            }
            if (orderBy != null)
            {
                cmdStr += " ORDER BY " + orderBy;
            }
            if (limit != null)
            {
                cmdStr += " LIMIT " + limit;
            }
                MySqlCommand cmd = new MySqlCommand();
                openConnection();
                cmd.Connection = conn;
                cmd.CommandText = cmdStr;
                cmd.CommandTimeout = 30;
                MySqlDataReader data = cmd.ExecuteReader();
                DataSet dataSet = new DataSet(data, from);
            //Store Performance
                cmd.Dispose();
                data.Close();
                return dataSet;

        }
        /// <summary>
        /// Executes a SELECT command with sorting which returns a DataSet
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="from"></param>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataSet Select(String fields, String from, String where, String orderBy)
        {
            return Select(fields, from, where, orderBy, null);
        }
        /// <summary>
        /// Executes a SELECT command using a WHERE claus which returns a DataSet
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="from"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataSet Select(String fields, String from, String where)
        {
            return Select(fields, from, where, null, null);
        }
        /// <summary>
        /// Dumps the entire table with the Selected fields and returns a DataSet
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public DataSet Select(String fields, String from)
        {
            return Select(fields, from, null, null, null);
        }
        /// <summary>
        /// Dumps the entire table into a DataSet
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public DataSet Select(String from)
        {
            return Select("*", from, null, null, null);
        }
        #endregion

        #region Insert Command and all its overloads
        /// <summary>
        /// Executes an Insert Command. Make sure values are formatted properly to avoid Syntax errors.
        /// </summary>
        /// <param name="into"></param>
        /// <param name="fields"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Insert(String into, String fields, String values)
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            String cmdStr = "INSERT INTO " + into + " (" + fields + ") VALUES (" + values + ")";
            cmd.CommandText = cmdStr;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return true;
        }
        /// <summary>
        /// Executes an Insert Command. Make sure values are formatted properly to avoid Syntax errors.
        /// </summary>
        /// <param name="into"></param>
        /// <param name="fields"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool Insert(String into, String[] fields, String[] values)
        {
            String strFields, strValues;
            strFields = "";
            strValues = "";
            bool first = true;
            foreach (String field in fields)
            {
                if (first)
                {
                    strFields = field;
                    first = false;
                }
                else
                {
                    strFields += "," + field;
                }
            }
            first = true;
            foreach (String value in values)
            {
                if (first)
                {
                    strValues = value;
                    first = false;
                }
                else
                {
                    strValues += "," + value;
                }
            }
            return Insert(into, strFields, strValues);
        }
        /// <summary>
        /// Inserts an entire dataset into a table.
        /// </summary>
        /// <param name="into"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Insert(String into, DataSet data)
        {
            if (!data.isReadOnly())
            {
                for (int i = 0; i < data.NumberOfRows(); i++)
                {
                    String[] parts = data.GetInsertString(i);
                    if (!Insert(into, parts[0], parts[1]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Dataset provided is read only and can not be used for inserting!");
            }
            return true;
        }
        #endregion

        #region Update Commands
        /// <summary>
        /// Issues an Update command using a Where and Limit claus.
        /// </summary>
        /// <param name="update"></param>
        /// <param name="set"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(String update, String set, String where, String limit)
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            String cmdStr = "UPDATE " + update + " SET " + set;
            if (where != null)
            {
                cmdStr += " WHERE " + where;
            }
            if (limit != null)
            {
                cmdStr += " LIMIT " + limit;
            }
            cmd.CommandText = cmdStr;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// Issues an Update command using a Where claus.
        /// </summary>
        /// <param name="update"></param>
        /// <param name="set"></param>
        /// <param name="where"></param>
        public void Update(String updateTable, String set, String where)
        {
            Update(updateTable, set, where, null);
        }
        /// <summary>
        /// Issues an Update command to the Entire Table
        /// </summary>
        /// <param name="update"></param>
        /// <param name="set"></param>
        public void Update(String update, String set)
        {
            Update(update, set, null, null);
        }
        #endregion

        #region Delete Commands
        /// <summary>
        /// Issues a Delete command with a Where claus and Limit claus.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="where"></param>
        /// <param name="limit"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(String from, String where, String limit)
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            String cmdStr = "DELETE FROM " + from;
            if (where != null)
            {
                cmdStr += " WHERE " + where;
            }
            if (limit != null)
            {
                cmdStr += " LIMIT " + limit;
            }
            cmd.CommandText = cmdStr;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// Issues a Delete Command with a Where claus.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="where"></param>
        public void Delete(String from, String where)
        {
            Delete(from, where, null);
        }
        /// <summary>
        /// Deletes all rows from the specified table.
        /// </summary>
        /// <param name="from"></param>
        public void Delete(String from)
        {
            Delete(from, null, null);
        }
        #endregion

        #region Utility Methods
        /// <summary>
        /// Checks if a connection can be made to the database server specified upon initialization
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CanConnect()
        {
            openConnection();
            bool success = conn.State.ToString().Equals("Open");
            return success;
        }
        /// <summary>
        /// Checks if the connection is already open and if not it opens a new connection.
        /// </summary>
        private void openConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                    bgThread.Start();
                }
                catch (MySqlException e)
                {
                    throw new DatabaseConnectionException("Database Connection Error: " + e.Message, e);
                }
            }
        }
        /// <summary>
        /// Closes the database connection. Should be used only on application exit.
        /// </summary>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CloseConnection()
        {
            conn.Close();
        }
        /// <summary>
        /// Takes the provided String and returns a hexidecimal MD5 hash.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public String GetMD5Hash(String value)
        {
            //Hash the provided String
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            //Use StringBuilder to assemble the new String
            StringBuilder sBuilder = new StringBuilder();
            //Iterate through each byte and convert to Hexidecimal value
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2",CultureInfo.InvariantCulture));
            }
            //Return assembled String
            return sBuilder.ToString();
        }

        /// <summary>
        /// Begins the transaction. All Modifications after this method will not commit until a CommitTransaction
        /// is called. Also if there is an error it is recommened to rollback the changes.
        /// </summary>
        public void BeginTransaction()
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            cmd.CommandText = "BEGIN";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// This rolls back all the modifications since the beginning of the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            cmd.CommandText = "ROLLBACK";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// Commits the transaction to the database.
        /// </summary>
        public void CommitTransaction()
        {
            MySqlCommand cmd = new MySqlCommand();
            openConnection();
            cmd.Connection = conn;
            cmd.CommandText = "COMMIT";
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        private static void KeepAlive()
        {
            while (true)
            {
                instance.Select("*", "audit_events", null, null, "1");
                Thread.Sleep(KeepAliveTime * 60 * 1000);
            }
        }
        #endregion

        #region IDisposable Members
        /// <summary>
        /// Cleans up and closes the Database object.
        /// </summary>
        void IDisposable.Dispose()
        {
            bgThread.Abort();
            connStr = null;
            conn.Close();
            conn.Dispose();
            conn = null;
            md5Hasher.Clear();
            md5Hasher = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
