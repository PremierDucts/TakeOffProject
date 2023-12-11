using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TakeOffAPI.Entities;
using TakeOffAPI.WebAPIClient.Commons;

namespace TakeOffAPI.WebAPIClient.Utility
{
    public enum HttpType
    {
        GET = 0,
        POST,
        PUT,
        DELETE
    }

    public class ServerConnectInfo
    {
        public String Address { set; get; } = "";
        public String Port { set; get; } = "";
        public String Token { set; get; } = "";
        public String UserName { set; get; } = "";
        public String Password { set; get; } = "";

        public ServerConnectInfo() { }
    }

    public class CommonFuncs
    {
        public static ResponseData RequestApi<T>(ServerConnectInfo svrInfo,
            String apiCommand, int timeOut, HttpType httpType, Dictionary<String, String> listParams = null, Object postObj = null)
        {
            var resData = new ResponseData();
            try
            {
                if (listParams == null)
                    listParams = new Dictionary<String, String>();
                String response = null;
                String requestUrl = $"{svrInfo.Address}{apiCommand}";

                if (requestUrl.Contains("station/all/duration"))
                {
                    foreach (var item in listParams)
                    {
                        if (item.Key.Equals("jobNo"))
                        {
                            requestUrl = $"{requestUrl}/{item.Value}";
                            break;
                        }
                    }
                    listParams.Remove("jobNo");
                }
                switch (httpType)
                {
                    case HttpType.GET:
                        response = HttpClientUti.SendGetRequest(requestUrl, listParams, timeOut);
                        break;
                    case HttpType.POST:
                        response = HttpClientUti.SendPostRequest(requestUrl, postObj, listParams, timeOut);
                        break;
                    case HttpType.PUT:
                        response = HttpClientUti.SendPutRequest(requestUrl, postObj, listParams, timeOut);
                        break;
                    case HttpType.DELETE:
                        response = HttpClientUti.SendDeleteRequest(requestUrl, listParams, timeOut);
                        break;
                    default:
                        break;
                }

                if (resData != null)
                {
                    resData = JsonConvert.DeserializeObject<ResponseData>(response);
                    if (resData != null)
                        ConvertData<T>(resData);
                    else
                        return new ResponseData { Data = "Can not parse response string!" };
                }
                else
                    resData.Data = "Connect Failed!";
            }
            catch (Exception ex)
            {
                resData.Data = ex.Message;
            }
            return resData;
        }

        private static bool ConvertData<T>(ResponseData resData)
        {
            try
            {
                if (typeof(T).IsValueType)
                    return true;
                else if (resData.Data.GetType() == typeof(JArray))
                    resData.Data = ((JArray)resData.Data).ToObject<T>();
                else if (resData.Data.GetType() == typeof(JContainer))
                    resData.Data = ((JObject)resData.Data).ToObject<T>();
                else if (resData.Data.GetType() == typeof(JObject))
                    resData.Data = ((JObject)resData.Data).ToObject<T>();
                else if (resData.Data.GetType() == typeof(Object))
                    resData.Data = ((JObject)resData.Data).ToObject<T>();
                return true;
            }
            catch (Exception ex)
            {
                resData.Data = ex.Message;
                resData.MakeFailed();
                return false;
            }
        }
    }
}
