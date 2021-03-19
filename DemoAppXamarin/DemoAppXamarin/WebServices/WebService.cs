using DemoAppXamarin.Helpers;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DemoAppXamarin.WebServices
{
    public class WebService : IWebService
    {
        static HttpClient httpClient = ApiHelper.GetHttpClient();

        public async Task<string> GetData<T>(string url) where T : class, new()
        {
            string apiResult = null;
            try
            {
                //var authHeader = new AuthenticationHeaderValue("bearer", token);
                //httpClient.DefaultRequestHeaders.Authorization = authHeader;

                string apiRequest = APIEndPoints.ServiceURI + url;
                Debug.WriteLine($"API Start {url}" + DateTime.Now);

                var response = await httpClient.GetAsync(apiRequest).ConfigureAwait(false);

                Debug.WriteLine($"API END {url}" + DateTime.Now);

                if (HttpStatusCode.OK == response.StatusCode)
                {
                    apiResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }

            }
            catch (JsonReaderException)
            {
                return null;
            }
            catch (JsonException)
            {
                return null;
            }
            catch (System.IO.IOException)
            {
                return null;
            }
            catch (System.Exception)
            {
                //throw;
            }


            return apiResult;
        }
    }
}
