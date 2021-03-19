using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DemoAppXamarin.Helpers
{
    public static class ApiHelper
    {
        private static HttpClient httpClient;
        static ApiHelper()
        {
            httpClient = new HttpClient();
        }

      
        /// <returns></returns>
        public static HttpClient GetHttpClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpClient();
            }
            if (httpClient.BaseAddress == null)
            {
                httpClient.BaseAddress = new Uri(APIEndPoints.ServiceURI);
            }

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.MaxResponseContentBufferSize = 2147483647;
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromMinutes(5);
            return httpClient;
        }
    }
}