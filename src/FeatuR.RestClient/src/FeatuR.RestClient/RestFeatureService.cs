using System;

namespace FeatuR.RestClient
{
    public class RestFeatureService : IFeatureService
    {
        private readonly FeatureHttpClient _httpClient;

        public RestFeatureService(FeatureHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsFeatureEnabled(string featureId) => IsFeatureEnabled(featureId, null);
        public bool IsFeatureEnabled(string featureId, FeatureContext context)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(nameof(featureId));

            return _httpClient.IsFeatureEnabled(featureId, context).Result;
        }
    }
}
