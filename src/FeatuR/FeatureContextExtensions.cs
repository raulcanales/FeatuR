using System;

namespace FeatuR.Strategies
{
    public static class FeatureContextExtensions
    {
        public static IFeatureContext AddUserId(this IFeatureContext context, string userId, string userIdKey = UserIdStrategyHandler.UserIdParameterName)
        {
            if (string.IsNullOrWhiteSpace(userIdKey))
                throw new ArgumentNullException(nameof(userIdKey));

            if (context.Parameters.ContainsKey(userIdKey))
                context.Parameters[userIdKey] = userId;
            else
                context.Parameters.Add(userIdKey, userId);

            return context;
        }
    }
}
