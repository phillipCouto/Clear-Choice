using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;

namespace DataImporter
{
    public class InventoryAutoGenerator
    {
        Database db = Database.Instance;

        public InventoryAutoGenerator()
        {
            DataSet data = db.Select("*", InventoryItem.Table);
            db.BeginTransaction();
            while (data.Read())
            {
                InventoryItem item = new InventoryItem(data.GetRecordDataSet());
                Random rnd = new Random();
                int quantity = rnd.Next(1, 1000);
                float price = (float)rnd.Next(1, 20) * (float)rnd.NextDouble();
                item.setQuantity(quantity);
                item.setCurrentCost(price);
                item.setAverageCost(price);
                item.SaveObject(db);
            }
            db.CommitTransaction();
        }
    }
}
