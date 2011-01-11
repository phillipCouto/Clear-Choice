using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stemstudios_File_Service;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please Provide 2 Arguments (Data Importer, File Location)");
            }
            else if (args[0].Equals("InventoryItems"))
            {
                new InventoryItemImporter(args[1]);
            }
            else if (args[0].Equals("InventoryPriceSet"))
            {
                new InventoryAutoGenerator();
            }
            else if (args[0].Equals("FileUpload"))
            {
                FileService fs = FileService.Instance;
                WebFile webFile = fs.GetWebFile("10", args[1]);
                
                
            }
        }
    }
}
