using System;

namespace FeatuR.RestClient
{
    public class RestFeatureServiceSettings
    {
        public string BaseUrl { get; set; }
        public string IsFeatureEnabledEndpoint { get; set; } = "feature/{featureId}";
        public string GetAllEnabledFeaturesEndpoint { get; set; } = "features";
        public string EvaluateFeaturesEndpoint { get; set; } = "features/evaluate";
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
