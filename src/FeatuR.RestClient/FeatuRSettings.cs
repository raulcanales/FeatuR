using System;
using System.Collections.Generic;

namespace FeatuR.RestClient
{
    public class FeatuRSettings
    {
        public const string DefaultIsFeatureEnabledEndpoint = "feature/{featureId}";
        public const string DefaultGetAllEnabledFeaturesEndpoint = "features";
        public const string DefaultEvaluateFeaturesEndpoint = "features/evaluate";

        public string BaseUrl { get; set; }
        public string IsFeatureEnabledEndpoint { get; set; } = DefaultIsFeatureEnabledEndpoint;
        public string GetAllEnabledFeaturesEndpoint { get; set; } = DefaultGetAllEnabledFeaturesEndpoint;
        public string EvaluateFeaturesEndpoint { get; set; } = DefaultEvaluateFeaturesEndpoint;
        public Dictionary<string, string> Headers { get; set; }
        public int TimeoutSeconds { get; set; }

        public void ValidateSettings()
        {
            if (!IsFeatureEnabledEndpoint.Contains("{featureId}"))
                throw new InvalidOperationException("The FeatureEndpoint should contain {featureId} in it to be replaced by the service.");

            if (TimeoutSeconds < 0)
                TimeoutSeconds = 5;
        }
    }
}
