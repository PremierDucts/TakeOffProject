using Microsoft.AspNetCore.Hosting;
using System.Data.Common;
using System.Data;
using TakeOffAPI.Entities.Request;
using TakeOffAPI.Entities.Response;
using TakeOffAPI.Service.Interface;
using TakeOffAPI.DBClient;
using Newtonsoft.Json;
using System.Text;
using TakeOffAPI.Entities;
using Google.Protobuf.WellKnownTypes;
using System.Xml.Linq;

namespace TakeOffAPI.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly QldDataContext _dbContextClass;
        private readonly IConfiguration _configuration;

        public AuthenticationService(QldDataContext dbContextClass, IConfiguration configuration)
        {
            _dbContextClass = dbContextClass;
            _configuration = configuration;
        }

        public async Task<ResponseData> Login(AuthRequest authRequest)
        {
            string apiUrlLogin = _configuration.GetValue<string>("URL:API_Appusers") ?? throw new ArgumentNullException("Value of URL:API_Appusers is null");
            ApiClient<ResponseData> apiClient = new ApiClient<ResponseData>();
            var credential = new
            {
                username = authRequest.Username,
                password = authRequest.Password,
                deviceId = ""
            };
            string json = JsonConvert.SerializeObject(credential);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            try
            {
                var response = await apiClient.PostAsync(apiUrlLogin, content);
                //Login username password success
                if(response.Code == (int)ERROR_CODE.SUCCESS)
                {
                    if (_dbContextClass.ControlMachineLists.Any(i => i.ip_address.Equals(authRequest.IpAddress)
                    && i.mac_address.Equals(authRequest.MacAddress)
                    && i.uid.Equals(authRequest.UId))){
                        return new ResponseData
                        {
                            Code = (int)ERROR_CODE.SUCCESS,
                            Data = "OK!"
                        };
                    }
                    else
                    {
                        return new ResponseData
                        {
                            Code = (int)ERROR_CODE.ACCESS_DENIED,
                            Data = "Username can not push data on this machine"
                        };
                    }
                }
                return new ResponseData
                {
                    Code = (int)ERROR_CODE.INTERNAL_ERROR,
                    Data = "Cannot call api login!"
                };
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("API request error: " + e.Message);
                throw e;
            }
           
        }
    }
}
