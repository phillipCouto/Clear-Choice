using System;

namespace Stemstudios.DataAccessLayer.DataObjects
{
    public class InventoryItem : DataObject
    {
        public enum Fields {itemID,ItemName,ItemDescription,Category,AverageCost,CurrentCost,Quantity}

        public const String PrimaryKey = "itemID";
        public const String Table = "inventory_items";
        public InventoryItem(Database db)
            : base(Table, db)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
        }

        public InventoryItem(DataSet data)
            : base(data)
        {
            PrimaryKeyColumns = new String[1] { PrimaryKey };
            base.SetTable(Table);
        }

        /// <summary>
        /// Sets the quantity of the current item.
        /// </summary>
        /// <param name="quantity"></param>
        public void setQuantity(float quantity)
        {
            SetValue(Fields.Quantity.ToString(), quantity);
        }
        /// <summary>
        /// Returns the quantity of the item in stock.
        /// </summary>
        /// <returns></returns>
        public float getQuantity()
        {
            return getFloat(Fields.Quantity.ToString());
        }
        /// <summary>
        /// Sets the current cost of the Item.
        /// </summary>
        /// <param name="cost"></param>
        public void setCurrentCost(float cost)
        {
            SetValue(Fields.CurrentCost.ToString(), cost);
        }
        /// <summary>
        /// Returns the current cost of the item.
        /// </summary>
        /// <returns></returns>
        public float getCurrentCost()
        {
            return getFloat(Fields.CurrentCost.ToString());
        }
        /// <summary>
        /// Sets the Average cost of the Item.
        /// </summary>
        /// <param name="cost"></param>
        public void setAverageCost(float cost)
        {
            SetValue(Fields.AverageCost.ToString(), cost);
        }
        /// <summary>
        /// Returns the average cost for the item.
        /// </summary>
        /// <returns></returns>
        public float getAverageCost()
        {
            return getFloat(Fields.AverageCost.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the Category for the Item.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int setCategory(String category)
        {
            if (Formating.TitleCheck(category))
            {
                SetValue(Fields.Category.ToString(), category);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the items category.
        /// </summary>
        /// <returns></returns>
        public String getCategory()
        {
            return getString(Fields.Category.ToString());
        }
        /// <summary>
        /// Sets the Item Description.
        /// </summary>
        /// <param name="desc"></param>
        public void setItemDescription(String desc)
        {
            SetValue(Fields.ItemDescription.ToString(),desc.Replace("'","\\'"));
        }
        /// <summary>
        /// Returns the Items Description
        /// </summary>
        /// <returns></returns>
        public String getItemDescription()
        {
            return getString(Fields.ItemDescription.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the item name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int setItemName(String name)
        {
            if (Formating.TitleCheck(name))
            {
                SetValue(Fields.ItemName.ToString(), name);
                return 0;
            }
            return 1102;
        }
        /// <summary>
        /// Returns the Items unqiue name.
        /// </summary>
        /// <returns></returns>
        public String getItemName()
        {
            return getString(Fields.ItemName.ToString());
        }
        /// <summary>
        /// Performs a format check and if the check clears sets the item id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int setItemID(String id)
        {
            if (Formating.ItemIDCheck(id))
            {
                SetValue(Fields.itemID.ToString(), id);
                return 0;
            }
            return 1104;
        }
        /// <summary>
        /// Returns the Items unique ID
        /// </summary>
        /// <returns></returns>
        public String getItemID()
        {
            return getString(Fields.itemID.ToString());
        }
    }
}
