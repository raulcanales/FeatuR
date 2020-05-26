using System.Collections.Generic;

namespace FeatuR.Tests.Helpers
{
    public static class FeatureExtensions
    {
        public static Feature CreateFeatureWithDefaultStrategy(string id, bool enabled)
         => new Feature
         {
             Id = id,
             Enabled = enabled,
             ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
             {
                 ["default"] = new Dictionary<string, string>()
             }
         };
    }
}
