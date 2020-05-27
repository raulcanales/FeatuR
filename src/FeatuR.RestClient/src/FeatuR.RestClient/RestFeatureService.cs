using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatuR.RestClient
{
    public class RestFeatureService : IFeatureService
    {
        private readonly FeatureHttpClient _httpClient;

        public RestFeatureService(FeatureHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IDictionary<string, bool> EvaluateFeatures(IEnumerable<string> featureIds, IFeatureContext context)
        {
            if (featureIds == null || !featureIds.Any())
                throw new ArgumentNullException(nameof(featureIds));

            return _httpClient.EvaluateFeaturesAsync(featureIds, context).Result;
        }

        public IEnumerable<string> GetEnabledFeatures(IFeatureContext context) 
            => _httpClient.GetEnabledFeaturesAsync(context).Result;

        public bool IsFeatureEnabled(string featureId) 
            => IsFeatureEnabled(featureId, null);

        public bool IsFeatureEnabled(string featureId, IFeatureContext context)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(nameof(featureId));

            return _httpClient.IsFeatureEnabledAsync(featureId, context).Result;
        }
    }
}
