using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        internal async Task<IEnumerable<string>> GetEnabledFeatures(IFeatureContext context)
        {
            try
            {
                SetHeadersFromContext(context);
                using (var response = await _httpClient.GetAsync(_settings.GetAllEnabledFeaturesEndpoint))
                {
                    if (!response.IsSuccessStatusCode)
                        return default;

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<string>>(content);
                }
            }
            catch
            {
                // Silent exception. Just return default
            }

            return default;
        }

        internal async Task<bool> IsFeatureEnabled(string featureId, IFeatureContext context)
        {
            try
            {
                SetHeadersFromContext(context);
                using (var response = await _httpClient.GetAsync(_settings.IsFeatureEnabledEndpoint.Replace("{featureId}", featureId)))
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

        private void SetHeadersFromContext(IFeatureContext context)
        {
            if (context == null)
                return;

            foreach (var kv in context.Parameters)
                _httpClient.DefaultRequestHeaders.Add(kv.Key, kv.Value);
        }
    }
}
