using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Stemstudios_File_Service
{
    public class FileService
    {

        public static FileService Instance
        {
            get { return instance; }
        }

        private static FileService instance = new FileService();

        static FileService()
        { }
        FileService()
        { }

        public String UploadFile(String appID, String file)
        {
            return "";
        }

        public String GetDownloadURL(String appID, String fileID)
        {
            return "";
        }
    }
}
