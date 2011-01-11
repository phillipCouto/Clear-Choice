using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Stemstudios_File_Service
{
    public class WebFile
    {
        private String fileID;
        private String filePath;
        private String uploadURL;
        private long totalBytes = 0;
        private long totalSent = 0;
        private bool isComplete = false;
        private int rate = 0;

        public String FileID
        {
            get { return fileID; }
        }
        
        public String FileName
        {
            get { return Path.GetFileName(filePath); }
        }

        public bool IsComplete
        {
            get { return true; }
        }

        public long TotalBytesSent
        {
            get { return totalSent; }
        }

        public long TotalBytesSize
        {
            get { return totalBytes; }
        }

        public int UploadRate
        {
            get { return rate; }
            set { rate = value; }
        }

        internal WebFile(String file, String uploadURL)
        {
            filePath = file;
            this.uploadURL = uploadURL;
            
        }
        /// <summary>
        /// Uploads the file to the server at the specificed rate
        /// </summary>
        public void UploadFile(int rate)
        {
            if (isComplete)
            {
                return;
            }
            Thread uploadThread = new Thread(uploadFile);
            uploadThread.SetApartmentState(ApartmentState.STA);
            uploadThread.Start();
 
        }
        private void uploadFile()
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
            try
            {
                HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(uploadURL);
                httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
                httpWebRequest2.Method = "POST";
                httpWebRequest2.KeepAlive = true;

                Stream memStream = new System.IO.MemoryStream();

                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");


                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

                /*foreach (string key in nvc.Keys)
                {
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }*/


                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: " + GetMimeType(filePath) + "\r\n\r\n";


                string header = string.Format(headerTemplate, "file", FileName);

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);


                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[1024];


                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }

                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                fileStream.Close();

                httpWebRequest2.ContentLength = memStream.Length;

                Stream requestStream = httpWebRequest2.GetRequestStream();

                memStream.Position = 0;

                byte[] tempBuffer;
                if (rate > 0)
                {
                    tempBuffer = new byte[rate];
                }
                else
                {
                    tempBuffer = new byte[1024];
                }
                long nextUpdate = DateTime.Now.AddSeconds(1).Ticks;
                bytesRead = 0;
                totalSent = 0;
                totalBytes = memStream.Length;
                while ((bytesRead = memStream.Read(tempBuffer, 0, tempBuffer.Length)) != 0)
                {
                    if (rate > 0)
                    {
                        while (DateTime.Now.Ticks < nextUpdate)
                        {
                            Thread.Sleep(1);
                        }
                        nextUpdate = DateTime.Now.AddSeconds(1).Ticks;
                    }
                    totalSent += bytesRead;
                    requestStream.Write(tempBuffer, 0, bytesRead);
                    requestStream.Flush();
                }

                memStream.Close();
                requestStream.Close();


                WebResponse webResponse2 = httpWebRequest2.GetResponse();

                Stream stream2 = webResponse2.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);

                fileID = reader2.ReadLine().Split('=')[1];

                webResponse2.Close();
                httpWebRequest2 = null;
                webResponse2 = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload Excetpion: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            isComplete = true;
        }

        private string GetMimeType(string Filename)
        {
            string mime = "application/octetstream";
            string ext = System.IO.Path.GetExtension(Filename).ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;
        }
    }
}
