using System.Collections.Generic;

namespace FeatuR.EntityFramework.Tests
{
    public class Features
    {
        public static Feature DefaultFeature = new Feature
        {
            Id = "default",
            Enabled = true,
            Name = "Default",
            Description = "This is a default feature",
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["default"] = new Dictionary<string, string>()
            }
        };

        public static Feature DisabledFeature = new Feature
        {
            Id = "test",
            Enabled = false,
            Name = "Test",
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["default"] = new Dictionary<string, string>()
            }
        };

        public static Feature Feature1 = new Feature
        {
            Id = "feature-1",
            Enabled = true,
            Name = "Feature 1",
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["default"] = new Dictionary<string, string>()
            }
        };
    }
}
