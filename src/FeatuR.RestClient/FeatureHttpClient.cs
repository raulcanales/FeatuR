using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FeatuR.RestClient
{
    public class FeatureHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly RestFeatureServiceSettings _settings;

        public FeatureHttpClient(HttpClient httpClient, RestFeatureServiceSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
        }

        public async Task<bool> IsFeatureEnabled(string featureId, IFeatureContext context)
        {
            try
            {
                if (context != null)                
                    foreach(var kv in context.Parameters)
                    {
                        _httpClient.DefaultRequestHeaders.Add(kv.Key, kv.Value);
                    }                                    

                using (var response = await _httpClient.GetAsync(_settings.FeatureEndpoint.Replace("{featureId}", featureId)))
                {
                    if (!response.IsSuccessStatusCode)
                        return false;

                    var content = await response.Content.ReadAsStringAsync();
                    return Convert.ToBoolean(content);
                }                
            }
            catch 
            {
                // Silent exception. Just return false
            }

            return false;
        }
    }
}
