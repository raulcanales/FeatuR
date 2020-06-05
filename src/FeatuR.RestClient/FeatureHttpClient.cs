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
        private readonly FeatuRSettings _settings;

        public FeatureHttpClient(HttpClient httpClient, FeatuRSettings settings)
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
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            SetHeadersFromContext(request, context);
            return await Send<TResponse>(request, token).ConfigureAwait(false);
        }

        private async Task<TResponse> Post<TResponse>(object requestObject, string endpoint, IFeatureContext context, CancellationToken token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            SetHeadersFromContext(request, context);
            request.Content = new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json");
            return await Send<TResponse>(request, token).ConfigureAwait(false);
        }

        private async Task<TResponse> Send<TResponse>(HttpRequestMessage request, CancellationToken token)
        {
            try
            {
                using (var response = await _httpClient.SendAsync(request, token).ConfigureAwait(false))
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

        private void SetHeadersFromContext(HttpRequestMessage request, IFeatureContext context)
        {
            if (context == null)
                return;

            foreach (var kv in context.Parameters)
                request.Headers.Add(kv.Key, kv.Value);
        }
    }
}
