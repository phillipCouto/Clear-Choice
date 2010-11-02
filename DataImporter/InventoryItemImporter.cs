using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios.DataAccessLayer;
using System.IO;
using Stemstudios.DataAccessLayer.DataObjects;

namespace DataImporter
{
    class InventoryItemImporter
    {
        private Database db = Database.Instance;
        private int importCount = 0;
        public InventoryItemImporter(String filePath)
        {
            try
            {
                db.CanConnect();
                db.BeginTransaction();
                TextReader tr = new StreamReader(filePath);
                String line;
                while ((line = tr.ReadLine()) != null)
                {
                    String[] pieces = line.Split(',');

                    if (pieces[0].Length > 1 && pieces[1].Length > 1)
                    {
                        if (pieces[0][0].Equals('"'))
                        {
                            pieces[0].Substring(1, pieces[0].Length - 2);
                        }
                        if (pieces[1][0].Equals('"'))
                        {
                            pieces[1].Substring(1, pieces[1].Length - 2);
                        }
                        InventoryItem item = new InventoryItem(db);
                        item.setItemID(FormatString(pieces[0],true).ToUpper());
                        item.setItemName(FormatString(pieces[1], false).ToUpper());
                        item.setCategory("NOT SET");
                        item.SaveObject(db);
                        
                    }
                    importCount++;
                }
                db.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                db.RollbackTransaction();
            }
        }
        private String FormatString(string text,bool remove)
        {
            String formatted = text.Replace("''", "\"");
            if (remove)
            {
                formatted = formatted.Replace("\"", "");
            }
            return formatted;
        }
    }
}
