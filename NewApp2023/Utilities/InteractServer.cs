using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewApp2023.Utilities
{
    public class InteractServer
    {
        public static string SendPost(string url, string postData)
        {
            string webpageContent = string.Empty;

            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (Stream webpageStream = webRequest.GetRequestStream())
                {
                    webpageStream.Write(byteArray, 0, byteArray.Length);
                }

                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream()))
                    {
                        webpageContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw or return an appropriate response/exception
                throw ex;
            }

            return webpageContent;
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("https://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static string GetIPAddress(string domainName)
        {
            String address = "";
            WebRequest request = WebRequest.Create(domainName + "getIP.php");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }
    }
}
