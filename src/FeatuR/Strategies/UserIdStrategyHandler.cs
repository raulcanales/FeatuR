using System.Collections.Generic;

namespace FeatuR.Strategies
{
    /// <summary>
    /// Evaluates the user id to determine if a feature should be enabled.
    /// </summary>
    public class UserIdStrategyHandler : IStrategyHandler
    {
        /// <summary>
        /// Dictionary key to add allowed user ids in the properties of a <see cref="Feature"/>.
        /// </summary>
        public const string AllowedUserIdsParameterName = "allowed";

        /// <summary>
        /// Dictionary key to add excluded user ids in the properties of a <see cref="Feature"/>.
        /// </summary>
        public const string ExcludedUserIdsParameterName = "excluded";

        /// <summary>
        /// Expected key in the <see cref="IFeatureContext"/>'s Parameters, containing the user id to be evaluated.
        /// </summary>
        public const string UserIdParameterName = "UserId";

        /// <summary>
        /// Separator used to split user ids that are allowed or excluded.
        /// </summary>
        public const string Separator = ",";

        /// <inheritdoc />
        public bool IsEnabled(Dictionary<string, string> parameters, IFeatureContext context = null)
        {
            var userId = string.Empty;
            context?.Parameters?.TryGetValue(UserIdParameterName, out userId);

            if (string.IsNullOrWhiteSpace(userId))
                return false;

            if (parameters.TryGetValue(AllowedUserIdsParameterName, out var allowedUserIds) && !allowedUserIds.Contains($"{Separator}{userId}{Separator}"))
                return false;

            if (parameters.TryGetValue(ExcludedUserIdsParameterName, out var excludedUserIds) && excludedUserIds.Contains($"{Separator}{userId}{Separator}"))
                return false;

            return true;
        }
    }
}