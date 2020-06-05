using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
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
            if (_settings.TimeoutSeconds > 0)
                _httpClient.Timeout = TimeSpan.FromSeconds(_settings.TimeoutSeconds);
        }

        internal async Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context, CancellationToken token)
            => await Post<IDictionary<string, bool>>(featureIds, _settings.EvaluateFeaturesEndpoint, context, token).ConfigureAwait(false);

        internal async Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context, CancellationToken token)
            => await Get<IEnumerable<string>>(_settings.GetAllEnabledFeaturesEndpoint, context, token).ConfigureAwait(false);

        internal async Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context, CancellationToken token)
            => await Get<bool>(_settings.IsFeatureEnabledEndpoint.Replace("{featureId}", featureId), context, token).ConfigureAwait(false);

        private async Task<TResponse> Get<TResponse>(string endpoint, IFeatureContext context, CancellationToken token)
        {
            try
            {
                SetHeadersFromContext(context);
                using (var response = await _httpClient.GetAsync(endpoint, token))
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

        private async Task<TResponse> Post<TResponse>(object request, string endpoint, IFeatureContext context, CancellationToken token)
        {
            try
            {
                SetHeadersFromContext(context);
                var json = JsonConvert.SerializeObject(request);
                using (var response = await _httpClient.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"), token))
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
