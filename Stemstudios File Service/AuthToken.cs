using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Stemstudios_File_Service
{
    public class AuthToken
    {

        private String token;
        private String uploadURL;

        public String Token
        {
            get { return token; }
        }

        public String UploadURL
        {
            get { return uploadURL; }
        }

        private String authURL = FileService.URLBase + "getUploadURL";
        
        internal AuthToken(String appID)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(authURL);
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = false;

                String parameters = "appID=" + appID;

                Stream requestStream = httpWebRequest.GetRequestStream();
                byte[] data = System.Text.Encoding.UTF8.GetBytes(parameters);
                requestStream.Write(data, 0, data.Length);

                WebResponse webResponse = httpWebRequest.GetResponse();

                Stream responseStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream);
                while (!responseReader.EndOfStream)
                {
                    string[] nameValuePair = responseReader.ReadLine().Split('=');
                    if (nameValuePair[0].Equals("authToken"))
                    {
                        token = nameValuePair[1];
                    }
                    else
                    {
                        uploadURL = nameValuePair[1];
                    }
                }

                webResponse.Close();
                httpWebRequest = null;
                webResponse = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
