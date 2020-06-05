using FeatuR.Strategies;
using System.Collections.Generic;

namespace FeatuR.EntityFramework.MySQL.IntegrationTests.Data
{
    public static class Features
    {
        public static Feature DefaultFeature = new Feature
        {
            Id = "test-1",
            Name = "Simple feature",
            Enabled = true,
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["default"] = new Dictionary<string, string>()
            }
        };
        public static Feature OnlyForUser444 = new Feature
        {
            Id = "test-2",
            Name = "Only for user 444",
            Enabled = true,
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["userid"] = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.AllowedUserIdsParameterName] = $"{UserIdStrategyHandler.Separator}444{UserIdStrategyHandler.Separator}"
                }
            }

        };
        public static Feature ExcludeUserPepe = new Feature
        {
            Id = "test-3",
            Name = "For everyone except for user Pepe",
            Enabled = true,
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["userid"] = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.ExcludedUserIdsParameterName] = $"{UserIdStrategyHandler.Separator}Pepe{UserIdStrategyHandler.Separator}"
                }
            }
        };
        public static Feature DisabledFeature = new Feature
        {
            Id = "test-4",
            Name = "Just a disabled feature",
            Enabled = false,
            ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
            {
                ["default"] = new Dictionary<string, string>()
            }
        };
    }
}
