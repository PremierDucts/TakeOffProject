using TakeOffAPI.Entities;
using TakeOffAPI.WebAPIClient.Commons;
using TakeOffAPI.WebAPIClient.Utility;

namespace TakeOffAPI.WebAPIClient
{
    public class ServerAPI
    {
        public static ResponseData RequestApiProc<T>(ServerConnectInfo svrInfo,
            API_TYPE apiType, HttpType httpType, Dictionary<String, String> listParams = null, Object postObj = null)
        {
            var res = CommonFuncs.RequestApi<T>(svrInfo, APIPath.HttpCommands[apiType], GetApiTimeOut(apiType), httpType, listParams, postObj);
            if (res.IsFailed())
                return new ResponseData { Code = (int)ERROR_CODE.FAIL, Data = res.Data };
            else
                return new ResponseData { Code = res.Code, Data = res.Data };
        }

        public static int GetApiTimeOut(API_TYPE apiType)
        {
            switch (apiType)
            {
                default: return 30000;
            }
        }
    }
}
