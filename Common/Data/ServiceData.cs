using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Data
{
    public static class ServiceData
    {
        public static async Task<T> GetAsync<T>(Uri uri,
            CancellationToken cancelToken = default(CancellationToken))
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await httpClient.GetAsync(uri.OriginalString);
                }
                catch (Exception)
                {
                    response = null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(responseContent, settings));
            }
        }

        public static async Task<TOut> PostAsync<T, TOut>(Uri uri, T value, CancellationToken cancelToken = default(CancellationToken))
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri.OriginalString, jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOut>(result);
            }
        }
    }
}
