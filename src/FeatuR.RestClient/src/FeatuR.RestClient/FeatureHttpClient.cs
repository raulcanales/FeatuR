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
            if (_settings.TimeoutSeconds > 0)
                _httpClient.Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
        }

        internal async Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context)
            => await Post<IDictionary<string, bool>>(featureIds, _settings.EvaluateFeaturesEndpoint, context);

        internal async Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context)
            => await Get<IEnumerable<string>>(_settings.GetAllEnabledFeaturesEndpoint, context);

        internal async Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context)
            => await Get<bool>(_settings.IsFeatureEnabledEndpoint.Replace("{featureId}", featureId), context);

        private async Task<TResponse> Get<TResponse>(string endpoint, IFeatureContext context)
        {
            try
            {
                SetHeadersFromContext(context);
                using (var response = await _httpClient.GetAsync(endpoint))
                {
                    if (!response.IsSuccessStatusCode)
                        return default;

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }

            }
            catch
            {
                // silent exception
            }

            return default;
        }

        private async Task<TResponse> Post<TResponse>(object request, string endpoint, IFeatureContext context)
        {
            try
            {
                SetHeadersFromContext(context);
                using (var response = await _httpClient.GetAsync(endpoint))
                {
                    if (!response.IsSuccessStatusCode)
                        return default;

                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }

            }
            catch
            {
                // silent exception
            }

            return default;
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
