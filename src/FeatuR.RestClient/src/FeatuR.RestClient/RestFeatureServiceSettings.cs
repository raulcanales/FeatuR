using System;

namespace FeatuR.RestClient
{
    public class RestFeatureServiceSettings
    {
        public string BaseUrl { get; set; }
        public string FeatureEndpoint { get; set; } = "feature/{featureId}";
        public int TimeoutSeconds { get; set; }

        public void ValidateSettings()
        {
            if (!FeatureEndpoint.Contains("{featureId}"))
                throw new InvalidOperationException("The FeatureEndpoint should contain {featureId} in it to be replaced by the service.");

            if (TimeoutSeconds < 0)
                TimeoutSeconds = 5;
        }
    }
}
