using System.Diagnostics;
using System.Net.Security;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TakeOffAPI.WebAPIClient.Utility
{
    public class HttpClientUti
    {
        public static String SendGetRequest(String requestUrl, Dictionary<String, String> headerParams = null,
            int timeOut = 3000)
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient(clientHandler))
                {
                    PreProcess(client, headerParams, timeOut);
                    requestUrl = BuildParams(requestUrl, headerParams);
                    var response = client.GetAsync(requestUrl).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex) { return null; }
        }

        public static String SendPostRequest(String requestUrl, Object postObj, Dictionary<String, String> headerParams = null, int timeOut = 30000)
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient(clientHandler))
                {
                    PreProcess(client, headerParams, timeOut);
                    requestUrl = BuildParams(requestUrl, headerParams);
                    String jsonData = JsonConvert.SerializeObject(postObj);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(requestUrl, content).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return null; }
        }

        public static String SendPutRequest(String requestUrl, Object postObj, Dictionary<String, String> headerParams = null, int timeOut = 30000)
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient(clientHandler))
                {
                    PreProcess(client, headerParams, timeOut);
                    requestUrl = BuildParams(requestUrl, headerParams);
                    String jsonData = JsonConvert.SerializeObject(postObj);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(requestUrl, content).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); return null; }
        }

        public static String SendDeleteRequest(String requestUrl, Dictionary<String, String> headerParams = null,
            int timeOut = 3000)
        {
            try
            {
                var clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                using (var client = new HttpClient(clientHandler))
                {
                    PreProcess(client, headerParams, timeOut);
                    requestUrl = BuildParams(requestUrl, headerParams);
                    var response = client.DeleteAsync(requestUrl).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex) { return null; }
        }

        private static void PreProcess(HttpClient client, Dictionary<String, String> headerParams, int timeOut)
        {
            client.Timeout = TimeSpan.FromMilliseconds(timeOut);
            if (headerParams != null)
            {
                foreach (var pr in headerParams)
                {
                    if (pr.Key.Equals("Token"))
                    {
                        client.DefaultRequestHeaders.Add(pr.Key, pr.Value.ToString());
                        break;
                    }
                }
            }
        }
        private static String BuildParams(String requestUrl, Dictionary<String, String> headerParams)
        {
            StringBuilder requestUrlBuild = new StringBuilder();
            requestUrlBuild.Append(requestUrl);
            if (headerParams != null)
            {
                int index = 0;
                foreach (var pr in headerParams)
                {
                    if (!pr.Key.Equals("Token"))
                    {
                        if (index > 0)
                            requestUrlBuild.Append($"&{pr.Key}={pr.Value}");
                        else
                            requestUrlBuild.Append($"?{pr.Key}={pr.Value}");

                        index++;
                    }

                }
            }
            return requestUrlBuild.ToString();
        }

        #region [Custom http request]

        public static String SendPostRequestLogin(ServerConnectInfo svInfo, int timeOut = 30000)
        {
            try
            {
                var postObj = $"username={svInfo.UserName}&token={svInfo.Token}";
                var request = WebRequest.Create($"https://{svInfo.Address}");
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                byte[] data = Encoding.ASCII.GetBytes(postObj);

                request.Timeout = timeOut;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Credentials = new NetworkCredential(svInfo.UserName, svInfo.Token);
                using (var stream = request.GetRequestStream())
                    stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                return responseString;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SendPostRequest() failed: {svInfo.Address}\n{ex.Message}\n{ex.StackTrace}");
                return null;
            }
        }
        #endregion
    }
}
