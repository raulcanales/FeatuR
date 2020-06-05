using System;
using System.Collections.Generic;

namespace FeatuR.RestClient
{
    public class FeatuRSettings
    {
        public static readonly string DefaultIsFeatureEnabledEndpoint = "feature/{featureId}";
        public static readonly string DefaultGetAllEnabledFeaturesEndpoint = "features";
        public static readonly string DefaultEvaluateFeaturesEndpoint = "features/evaluate";

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
