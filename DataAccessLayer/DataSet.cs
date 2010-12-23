using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using MySql.Data.MySqlClient;
using Stemstudios.DataAccessLayer.DataObjects.Bindings;
using System.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Stemstudios.DataAccessLayer
{

    /// <summary>
    /// DataSet object used for containing a complete record set in memory to minimize database connections.
    /// </summary>
    public class DataSet
    {
        #region Constants
        public const int FIELDTITLES = 1;
        public const int FIELDTYPES = 2;
        #endregion

        #region Variable Definition
        private Hashtable fields;
        private Hashtable fieldTypes;
        private Hashtable primaryKeyIndex = new Hashtable();
        private Object[,] dataSet;
        private bool ReadOnly;
        private int pos;
        private Boolean fresh;
        private String dataTable;
        private ArrayList primaryKeys = new ArrayList();
        #endregion

        /// <summary>
        /// Intializes a new DataSet object.
        /// </summary>
        /// <param name="data"></param>
        public DataSet(MySqlDataReader data, String table)
        {
            pos = 0;
            fresh = true;
            dataTable = table;
            ReadOnly = true;
            fields = new Hashtable();
            fieldTypes = new Hashtable();
            LinkedList<Object[]> temp = new LinkedList<Object[]>();

            for (int i = 0; i < data.FieldCount; i++)
            {
                fields.Add(data.GetName(i), i);
                fields.Add(i, data.GetName(i));
                fieldTypes.Add(data.GetName(i), data.GetFieldType(i));
            }
            while (data.Read())
            {
                Object[] rowData = new Object[fieldTypes.Count];
                for (int i = 0; i < rowData.Length; i++)
                {
                    rowData[i] = data.GetValue(i);
                }
                temp.AddLast(rowData);
            }
            int rows, cols;
            rows = temp.Count;
            cols = fieldTypes.Count;
            dataSet = new Object[rows, cols];
            int x = 0;
            foreach (Object[] row in temp)
            {
                for (int y = 0; y < fieldTypes.Count; y++)
                {
                    dataSet[x, y] = row[y];
                }
                x++;
            }

        }
        /// <summary>
        /// Creates an editible DataSet using a table schema and a set number of rows
        /// </summary>
        /// <param name="db"></param>
        /// <param name="table"></param>
        public DataSet(Database db, String table,int numRows)
        {
            dataTable = table;
            ReadOnly = false;
            pos = 0;
            DataSet temp = db.Select("*", table, null, null, "1");

            this.fields = temp.ReturnFieldInfo(DataSet.FIELDTITLES);
            this.fieldTypes = temp.ReturnFieldInfo(DataSet.FIELDTYPES);
            dataSet= new Object[numRows,fieldTypes.Count];
        }
        /// <summary>
        /// Creates a single row DataSet that is editiable, used for DataObjects.
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="fieldTypes"></param>
        /// <param name="data"></param>
        public DataSet(Hashtable fields,Hashtable fieldTypes, Object[] data,String table)
        {
            dataTable = table;
            this.ReadOnly = false;
            this.fields = fields;
            this.fieldTypes = fieldTypes;
            dataSet = new Object[1, fieldTypes.Count];
            for (int i = 0; i < data.Length; i++)
            {
                dataSet[0,i] = data[i];
            }

        }

        #region Number Handling Get methods

        /// <summary>
        /// Gets the selected column in the row as an integer using index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int getInt(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(Int32)))
            {
                return Int32.Parse(dataSet[pos, index].ToString());
            }
            else
            {
                throw new FormatException("Column type is not a 32bit Integer.");
            }
        }
        /// <summary>
        /// Gets the selected column in the row as an integer using column title.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public int getInt(String column)
        {
            try
            {
                return getInt(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a 64bit Integer using an index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public long getLong(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(Int64)))
            {
                return Int64.Parse(dataSet[pos, index].ToString());
            }
            else
            {
                throw new FormatException("Column type is not a 64bit Integer.");
            }
        }
        /// <summary>
        /// Gets a 64 bit Integer using a column title.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public long getLong(String column)
        {
            try
            {
                return getLong(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }

        /// <summary>
        /// Gets a floating point number using an index number
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float getFloat(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(Single)))
            {
                return Single.Parse(dataSet[pos, index].ToString());
            }
            else
            {
                throw new FormatException("Column type is not a Single Percision floating point number.");
            }
        }
        /// <summary>
        /// Gets a floating point number using a column title
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public float getFloat(String column)
        {
            try
            {
                return getFloat(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Gets a double number using an index number
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double getDouble(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(Double)))
            {
                return Double.Parse(dataSet[pos, index].ToString());
            }
            else
            {
                throw new FormatException("Column type is not a Double Percision floating point number.");
            }
        }
        /// <summary>
        /// Gets a double number using a column title
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public double getDouble(String column)
        {
            try
            {
                return getDouble(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }

        #endregion

        #region Text and Data Getting Methods

        /// <summary>
        /// Gets a String using an index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public String getString(int index)
        {
            return dataSet[pos, index].ToString();
        }
        /// <summary>
        /// Gets a String using a column title.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public String getString(String column)
        {
            return getString(Int32.Parse(fields[column].ToString()));
        }
        /// <summary>
        /// Gets an Object using an index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Object getObject(int index)
        {
            return dataSet[pos, index];
        }
        /// <summary>
        /// Gets an Object using a column title.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Object getObject(String column)
        {
            return getObject(Int32.Parse(fields[column].ToString()));
        }
        /// <summary>
        /// Gets a Char array using an index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char[] getChars(int index)
        {
            return getString(index).ToCharArray();
        }
        /// <summary>
        /// Gets Char array using a column title.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public char[] getChars(String column)
        {
            return getChars(Int32.Parse(fields[column].ToString()));
        }
        /// <summary>
        /// Gets a boolean using an index number.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool getBoolean(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(Boolean)))
            {
                return Boolean.Parse(dataSet[pos, index].ToString());
            }
            else
            {
                throw new FormatException("Column type is not a Boolean data type.");
            }

        }
        /// <summary>
        /// Gets a boolean using a column title.
        /// </summary>
        /// <param name="coloumn"></param>
        /// <returns></returns>
        public bool getBoolean(String column)
        {
            try
            {
                return getBoolean(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Used by the Database Object to create an Insert String for a row
        /// </summary>
        /// <returns></returns>
        public String[] GetInsertString(int row)
        {
            String strFields = "";
            String strData = "";
            bool first = true;
            for (int i = 0; i < fieldTypes.Count; i++)
            {
                if (dataSet[row, i] != null)
                {
                if (!first)
                {
                    strFields += ",";
                    strData += ",";
                }
                else
                {
                    first = false;
                }
                    strFields += fields[i].ToString();
                    if (((Type)fieldTypes[fields[i]]).Equals(typeof(Int32)) | ((Type)fieldTypes[fields[i]]).Equals(typeof(Int64)) | ((Type)fieldTypes[fields[i]]).Equals(typeof(Single)) | ((Type)fieldTypes[fields[i]]).Equals(typeof(Double)))
                    {
                        strData += dataSet[row, i].ToString();
                    }
                    else
                    {
                        strData += "'" + dataSet[row, i].ToString() + "'";
                    }
                }
            }
            String [] data = {strFields,strData};
            return data;
        }
        /// <summary>
        /// Will create a DataSet for a specific row. Used By DataObject and its child classes for creating objects.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public DataSet GetRecordDataSet(int row)
        {
            Object[] rowData = new Object[fieldTypes.Count];
            for (int i = 0; i < rowData.Length; i++)
            {
                rowData[i] = dataSet[row, i];
            }
            return new DataSet(fields, fieldTypes, rowData, GetTableName());
        }
        /// <summary>
        /// Will create a Dataset for the row at the current Dataset position
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecordDataSet()
        {
            if (pos == -1)
            {
                throw new InvalidOperationException("Dataset Read method has not been called to start traversale");
            }
            return GetRecordDataSet(pos);
        }
        /// <summary>
        /// Returns a DateTime object using a column index number
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DateTime getDateTime(int index)
        {
            if (((Type)(fieldTypes[fields[index]])).Equals(typeof(DateTime)))
            {
                if (!dataSet[pos, index].Equals(DBNull.Value))
                {
                    return DateTime.Parse(dataSet[pos, index].ToString());
                }
                return DateTime.MinValue;
            }
            else
            {
                throw new FormatException("Column type is not a DateTime data type.");
            }
        }
        /// <summary>
        /// Returns a Data Time Object using a Column Title
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public DateTime getDateTime(String column)
        {
            try
            {
                return getDateTime(Int32.Parse(fields[column].ToString()));
            }
            catch (FormatException e)
            {
                throw new FormatException(e.Message);
            }
        }
        /// <summary>
        /// Returns a Collection which is used with a DataGridView
        /// </summary>
        /// <returns></returns>
        public Collection<T> getBindableCollection<T>()
        {
            Collection<T> dataRows = new Collection<T>();
            Type bindingClass = typeof(T);
            object[] attributes = bindingClass.GetCustomAttributes(true);
            bool bindable = false;
            foreach (object attribute in attributes)
            {
                if(attribute.GetType().Equals(typeof(BindableObject)))
                {
                    bindable = true;
                }
            }
            if(!bindable)
            {
                throw new System.InvalidOperationException("The binding class provided is not declared with a BindableObject attribute.");
            }
            int savepos = this.pos;
            pos = 0;
            while (Read() & NumberOfRows() > 0)
            {
                Object data = Activator.CreateInstance(bindingClass);
                PropertyInfo[] properties = data.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    object[] propAttrs = property.GetCustomAttributes(true);
                    foreach (object attribute in propAttrs)
                    {
                        if (attribute.GetType().Equals(typeof(BindableProperty)))
                        {
                            MethodInfo setMethod = property.GetSetMethod();
                            Object[] parameters = new Object[1];

                            if (GetColoumnDataType(property.Name).Equals(typeof(Int32)))
                            {parameters[0] = getInt(property.Name);}
                            else if (GetColoumnDataType(property.Name).Equals(typeof(Boolean)))
                            { parameters[0] = this.getBoolean(property.Name); }
                            else if (GetColoumnDataType(property.Name).Equals(typeof(DateTime)))
                            {
                                if (getDateTime(property.Name).Equals(DateTime.MinValue))
                                {
                                    parameters[0] = "";
                                }
                                else
                                {
                                    parameters[0] = this.getDateTime(property.Name).ToShortDateString();
                                }
                            }
                            else if (GetColoumnDataType(property.Name).Equals(typeof(Double)))
                            {
                                if (property.PropertyType.Name.Equals("String"))
                                {
                                    parameters[0] = "$" + getDouble(property.Name).ToString("#0.00");
                                }
                                else
                                {
                                    parameters[0] = this.getDouble(property.Name);
                                }
                            }
                            else if (GetColoumnDataType(property.Name).Equals(typeof(float)))
                            {
                                if (property.PropertyType.Name.Equals("String"))
                                {
                                    parameters[0] = "$"+getFloat(property.Name).ToString("#0.00");
;
                                }
                                else
                                {
                                    parameters[0] = this.getFloat(property.Name);
                                }
                            }
                            else if (GetColoumnDataType(property.Name).Equals(typeof(Int64)))
                            { parameters[0] = this.getLong(property.Name); }
                            else
                            {parameters[0] = getString(property.Name);}

                            setMethod.Invoke(data, parameters);
                        }
                    }
                }
                dataRows.Add((T)data);
            }
            ResetDataSet();
            return dataRows;
        }

        /// <summary>
        /// Creates a flow document which can be passed to the Document Previewer.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public FlowDocument GetFlowDocument(String title)
        {
            return GetFlowDocument(title, null, null, null);
        }
        /// <summary>
        /// Creates a flow document which can be passed to the Document Previewer.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="hiddenColumns"></param>
        /// <param name="displayTextMap"></param>
        /// <returns></returns>
        public FlowDocument GetFlowDocument(String title, ArrayList hiddenColumns)
        {
            return GetFlowDocument(title, hiddenColumns, null, null);
        }
        /// <summary>
        /// Creates a flow document which can be passed to the Document Previewer.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="hiddenColumns"></param>
        /// <param name="displayTextMap"></param>
        /// <returns></returns>
        public FlowDocument GetFlowDocument(String title, ArrayList hiddenColumns, Hashtable displayTextMap)
        {
            return GetFlowDocument(title, hiddenColumns, displayTextMap, null);
        }
        /// <summary>
        /// Creates a flow document which can be passed to the Document Previewer.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="hiddenColumns"></param>
        /// <param name="displayTextMap"></param>
        /// <param name="currencyFields"></param>
        /// <returns></returns>
        public FlowDocument GetFlowDocument(String title, ArrayList hiddenColumns,Hashtable displayTextMap, ArrayList currencyFields)
        {
            if (hiddenColumns == null)
            {
                hiddenColumns = new ArrayList();
            }
            if (displayTextMap == null)
            {
                displayTextMap = new Hashtable();
            }
            if (currencyFields == null)
            {
                currencyFields = new ArrayList();
            }
            hiddenColumns.Add("lastModified");
            hiddenColumns.Add("modifiedBy");

            FlowDocument doc = new FlowDocument();

            Paragraph pTitle = new Paragraph();
            pTitle.FontSize = 18;
            pTitle.Inlines.Add(new Run(title));
            pTitle.TextAlignment = System.Windows.TextAlignment.Center;
            pTitle.FontFamily = new FontFamily("Verdana");

            doc.Blocks.Add(pTitle);

            Table dataGrid = new Table();
            dataGrid.FontFamily = new FontFamily("Verdana");

            TableRowGroup rowGroup = new TableRowGroup();
            TableRow hRow = new TableRow();
            
            for (int i = 0; i < NumberOfColumns(); i++)
            {
                if (!hiddenColumns.Contains(fields[i].ToString()))
                {
                    TableColumn tColumn = new TableColumn();
                    tColumn.Width = System.Windows.GridLength.Auto;
                    dataGrid.Columns.Add(tColumn);

                    TableCell tCell = new TableCell();
                    tCell.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    tCell.BorderThickness = new System.Windows.Thickness(1);
                    tCell.Padding = new System.Windows.Thickness(4, 2, 2, 2);
                    tCell.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                    tCell.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));

                    Paragraph cell = new Paragraph();
                    if (displayTextMap[fields[i].ToString()] != null)
                    {
                        cell.Inlines.Add(new Run(displayTextMap[fields[i].ToString()].ToString()));
                    }
                    else
                    {
                        cell.Inlines.Add(new Run(fields[i].ToString()));
                    }
                    tCell.Blocks.Add(cell);
                    hRow.Cells.Add(tCell);
                }
            }
            rowGroup.Rows.Add(hRow);
            for (int i = 0; i < NumberOfRows(); i++)
            {
                TableRow row = new TableRow();
                row.FontSize = 12;
                for (int x = 0; x < NumberOfColumns(); x++)
                {
                    if (!hiddenColumns.Contains(fields[x].ToString()))
                    {
                        TableCell tCell = new TableCell();
                        Paragraph cell = new Paragraph();
                        tCell.Padding = new System.Windows.Thickness(4,2,2,2);
                        if (!currencyFields.Contains(fields[x].ToString()))
                        {
                            cell.Inlines.Add(new Run(dataSet[i,x].ToString()));
                        }
                        else
                        {
                            cell.Inlines.Add(new Run("$"+Single.Parse(dataSet[i, x].ToString()).ToString("#0.00")));
                        }
                        tCell.Blocks.Add(cell);
                        row.Cells.Add(tCell);
                    }
                }
                if (i % 2 != 0)
                {
                    row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DDDDDD"));
                }
                
                rowGroup.Rows.Add(row);
            }
            dataGrid.RowGroups.Add(rowGroup);
            doc.Blocks.Add(dataGrid);
            ResetDataSet();
            return doc;
        }
        #endregion

        #region Setting information Into a Dataset
        /// <summary>
        /// Sets value without type checking. Do not use this method unless the Data Type is verified!
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetCellValue(int row, int column, Object value)
        {
            dataSet[row, column] = value;
        }
        /// <summary>
        /// Sets value without type checking. Do not use this method unless the Data Type is verified!
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetCellValue(int row, String column, Object value)
        {
            int col = Int32.Parse(fields[column].ToString());
            SetCellValue(row, col, value);
        }

        /// <summary>
        /// Adds a String to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, String value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(String)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store strings.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Adds a 32 bit integer to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, int value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(Int32)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store 32 bit Integers.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Adds a 64 bit Integer to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, long value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(Int64)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store 64 bit integers.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Adds a floating point number to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, float value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(Single)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store floating point numbers.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Adds a double percision floating point number to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, double value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(Double)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store double percision floating point numbers.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Add a boolean value to the DataSet
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, bool value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(Boolean)))
                {
                    SetCellValue(row, col, value);
                }
                else
                {
                    throw new FormatException("Column does not store boolean values.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        /// <summary>
        /// Adds a DateTime object to the Dataset
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        public void SetValue(int row, String column, DateTime value)
        {
            if (!this.ReadOnly)
            {
                int col = Int32.Parse(fields[column].ToString());
                if (((Type)fieldTypes[column]).Equals(typeof(DateTime)))
                {
                     SetCellValue(row, col, value.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    throw new FormatException("Column does not store DateTime objects.");
                }
            }
            else
            {
                throw new InvalidOperationException("Can not modify a read only dataset.");
            }
        }
        #endregion

        #region Utility Methods
       
        /// <summary>
        /// Moves to first record in the DataSet
        /// </summary>
        public void MoveToFirst()
        {
            pos = 0;
            fresh = true;
        }
        /// <summary>
        /// Moves to last record in the DataSet
        /// </summary>
        public void MoveToLast()
        {
            pos = NumberOfRows() - 1;
        }
        /// <summary>
        /// Moves to next record or will return false if at end of set
        /// </summary>
        /// <returns></returns>
        public bool Read()
        {
            if (fresh & NumberOfRows() != 0)
            {
                fresh = false;
                return true;
            }
            else if (pos < NumberOfRows()-1)
            {
                pos++;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Will seek to row number specified. Will return false if row number is out of bounds.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool Seek(int row)
        {
            if (row < NumberOfRows())
            {
                pos = row;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Takes the primary key provided and moves the DataSet to the row of that primary key.
        /// </summary>
        /// <param name="key"></param>
        public void SeekToPrimaryKey(String key)
        {
            if (primaryKeyIndex.ContainsKey(key))
            {
                Seek(Int32.Parse(primaryKeyIndex[key].ToString()));
            }
            else
            {
                throw new NullReferenceException("Primary Key provided does not exist in this DataSet.");
            }
        }
        /// <summary>
        /// Gets the ReadOnly value of the DataSet
        /// </summary>
        /// <returns></returns>
        public bool isReadOnly()
        {
            return ReadOnly;
        }
        /// <summary>
        /// Returns the number of rows stored in the dataset
        /// </summary>
        /// <returns></returns>
        public int NumberOfRows()
        {
            return (dataSet.Length / fieldTypes.Count);
        }
        /// <summary>
        /// Returns the number of columns stored in the dataset
        /// </summary>
        /// <returns></returns>
        public int NumberOfColumns()
        {
            return (fieldTypes.Count);
        }
        /// <summary>
        /// Returns the Coloumn Datatype
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Type GetColoumnDataType(int column)
        {
            return ((Type)fieldTypes[fields[column]]);
        }
        /// <summary>
        /// Returns the Coloumn Datatype
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Type GetColoumnDataType(String column)
        {
            return ((Type)fieldTypes[column]);
        }
        /// <summary>
        /// Checks if the column is a number data type or not.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool IsColumnNumberDataType(int column)
        {
            if (GetColoumnDataType(column).Equals(typeof(Int32)) | GetColoumnDataType(column).Equals(typeof(Int64)) | GetColoumnDataType(column).Equals(typeof(Single)) | GetColoumnDataType(column).Equals(typeof(Double)) | GetColoumnDataType(column).Equals(typeof(Boolean)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Checks if the column is a number data type or not.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool IsColumnNumberDataType(String column)
        {
            return IsColumnNumberDataType(Int32.Parse(fields[column].ToString()));
        }
        /// <summary>
        /// Returns the hashtables containing the field information for the current dataset.
        /// </summary>
        /// <param name="InfoType"></param>
        /// <returns></returns>
        public Hashtable ReturnFieldInfo(int InfoType)
        {
            if (FIELDTITLES == InfoType)
            {
                return fields;
            }
            else
            {
                return fieldTypes;
            }
        }
        /// <summary>
        /// Returns the table that the information was derived from.
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return dataTable;
        }
        /// <summary>
        /// Sets the Table for the dataset. Used for overriding Join Statements.
        /// </summary>
        /// <param name="table"></param>
        public void SetTableName(String table)
        {
            dataTable = table;
        }
        /// <summary>
        /// Builds the Primary Key index so you can fetch a record by the primary keys.
        /// </summary>
        /// <param name="fields"></param>
        public void BuildPrimaryKeyIndex(String[] fields)
        {
            primaryKeyIndex.Clear();
            for (int i = 0; i < NumberOfRows(); i++)
            {
                Seek(i);
                String key = "";
                foreach (String field in fields)
                {
                    key += getObject(field).ToString();
                }
                primaryKeyIndex.Add(key, i);
            }
        }
        /// <summary>
        /// Builds the Primary Key index so you can fetch by the primary key.
        /// </summary>
        /// <param name="field"></param>
        public void BuildPrimaryKeyIndex(String field)
        {
            BuildPrimaryKeyIndex(new String[1] { field });
        }
        /// <summary>
        /// Resets the dataset to being fresh and ready from processing.
        /// </summary>
        public void ResetDataSet()
        {
            pos = 0;
            fresh = true;
        }
        /// <summary>
        /// Returns the list of Keys in the Primary Key Index.
        /// </summary>
        /// <returns></returns>
        public ICollection GetPrimaryKeySet()
        {
            if (primaryKeyIndex.Count > 0)
            {
                return primaryKeyIndex.Keys;
            }
            return null;
        }
        #endregion

   }
}
