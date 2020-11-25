using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinChallenge.Services.Interfaces;

namespace XamarinChallenge.Services
{
    public class ClientFactory : IClientFactory
    {
        private readonly JsonSerializerSettings _settings;
        private readonly Dictionary<string, string> _headers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settings">JsonSerielizer Configuration</param>
        /// <param name="headers">The custom headers are added with value-key</param>
        public ClientFactory(JsonSerializerSettings settings, Dictionary<string, string> headers)
        {
            this._settings = settings ?? new JsonSerializerSettings();
            this._headers = headers ?? new Dictionary<string, string>();
        }


        /// <summary>
        /// Generic get
        /// </summary>
        /// <typeparam name="T">Expected type or object</typeparam>
        /// <param name="url">Endpoint url string</param>
        /// <returns></returns>
        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            using (var client = new HttpClient())
            {
                _settings.NullValueHandling = NullValueHandling.Ignore;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (_headers.Any())
                {
                    foreach (var item in _headers)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(responseBody, _settings);
                }
            }
        }
    }
}
