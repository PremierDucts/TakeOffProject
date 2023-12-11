using System;
using Newtonsoft.Json;
using System.Net.Http;
using TakeOffAPI.Entities;
using MySqlX.XDevAPI.Common;

namespace TakeOffAPI
{
	public class ApiClient<T>
	{
		private readonly HttpClient httpClient;
		public ApiClient()
		{
			httpClient = new HttpClient();
		}
        public async Task<T> GetAsync(string apiUrl)
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            return await HandleResponse<T>(response);
        }



        public async Task<T> PostAsync(string apiUrl, HttpContent content)
        {
            HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
            return await HandleResponse<T>(response);
        }



        public async Task<T> PutAsync(string apiUrl, HttpContent content)
        {
            HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);
            return await HandleResponse<T>(response);
        }



        public async Task<T> DeleteAsync(string apiUrl)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);
            return await HandleResponse<T>(response);
        }



        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)(object)await response.Content.ReadAsStringAsync();
                }
                else if (typeof(T) == typeof(ResponseData))
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return (T)(object)JsonConvert.DeserializeObject<ResponseData>(result);

                }
                // You can add more handling for other types here.
                // Example: if (typeof(T) == typeof(int))
                //             return (T)(object)42;
                else
                {
                    throw new NotSupportedException("Unsupported response type.");
                }
            }
            else
            {
                throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
            }
        }
    }
}

