using System;
using System.Collections.Generic;

namespace FeatuR.RestClient
{
    public class RestFeatureService : IFeatureService
    {
        private readonly FeatureHttpClient _httpClient;

        public RestFeatureService(FeatureHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IEnumerable<string> GetEnabledFeatures(IFeatureContext context) => _httpClient.GetEnabledFeatures(context).Result;
        public bool IsFeatureEnabled(string featureId) => IsFeatureEnabled(featureId, null);
        public bool IsFeatureEnabled(string featureId, IFeatureContext context)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(nameof(featureId));

            return _httpClient.IsFeatureEnabled(featureId, context).Result;
        }
    }
}
