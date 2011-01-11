using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Stemstudios_File_Service
{
    public class FileService
    {

        internal static String URLBase
        {
            get { return "http://stemfileservice.appspot.com/"; }
        }

        public static FileService Instance
        {
            get { return instance; }
        }

        private static FileService instance = new FileService();

        static FileService()
        { }
        FileService()
        { }

        public WebFile GetWebFile(String appID, String file)
        {
            AuthToken token = new AuthToken(appID);
            WebFile webFile = new WebFile(file, token.UploadURL);

            return webFile;
        }

        public String GetDownloadURL(String appID, String fileID)
        {
            return "";
        }
    }
}
