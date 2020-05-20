using System.Collections.Generic;

namespace FeatuR.Strategies
{
    public class UserIdStrategyHandler : IStrategyHandler
    {
        public const string AllowedUserIdsParameterName = "allowed";
        public const string ExcludedUserIdsParameterName = "excluded";
        public const string UserIdParameterName = "UserId";
        public const string Separator = ",";
                
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context = null)
        {
            var userId = string.Empty;
            context?.Parameters?.TryGetValue(UserIdParameterName, out userId);

            if (string.IsNullOrWhiteSpace(userId))
                return false;

            if (parameters.TryGetValue(AllowedUserIdsParameterName, out var allowedUserIds))
            {
                if (!allowedUserIds.Contains($"{Separator}{userId}{Separator}"))
                    return false;
            }

            if (parameters.TryGetValue(ExcludedUserIdsParameterName, out var excludedUserIds))
            {
                if (excludedUserIds.Contains($"{Separator}{userId}{Separator}"))
                    return false;
            }

            return true;
        }
    }
}