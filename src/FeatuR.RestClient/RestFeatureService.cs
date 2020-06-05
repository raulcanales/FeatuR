using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FeatuR.RestClient
{
    public class RestFeatureService : IFeatureService
    {
        private readonly FeatureHttpClient _httpClient;

        public RestFeatureService(FeatureHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #region EvaluateFeatures

        public IDictionary<string, bool> EvaluateFeatures(IEnumerable<string> featureIds, IFeatureContext context)
            => EvaluateFeaturesAsync(featureIds, context, CancellationToken.None).Result;

        public Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context)
            => EvaluateFeaturesAsync(featureIds, context, CancellationToken.None);

        public async Task<IDictionary<string, bool>> EvaluateFeaturesAsync(IEnumerable<string> featureIds, IFeatureContext context, CancellationToken token)
        {
            if (featureIds == null || !featureIds.Any())
                throw new ArgumentNullException(nameof(featureIds));

            return await _httpClient.EvaluateFeaturesAsync(featureIds, context, token);
        }

        #endregion


        #region GetEnabledFeatures

        public IEnumerable<string> GetEnabledFeatures(IFeatureContext context)
            => GetEnabledFeaturesAsync(context, CancellationToken.None).Result;
        public Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context)
            => GetEnabledFeaturesAsync(context, CancellationToken.None);
        public async Task<IEnumerable<string>> GetEnabledFeaturesAsync(IFeatureContext context, CancellationToken token)
            => await _httpClient.GetEnabledFeaturesAsync(context, token);

        #endregion


        #region IsFeatureEnabled

        public bool IsFeatureEnabled(string featureId)
            => IsFeatureEnabledAsync(featureId, null, CancellationToken.None).Result;

        public bool IsFeatureEnabled(string featureId, IFeatureContext context)
            => IsFeatureEnabledAsync(featureId, context, CancellationToken.None).Result;

        public Task<bool> IsFeatureEnabledAsync(string featureId)
            => IsFeatureEnabledAsync(featureId, null, CancellationToken.None);

        public Task<bool> IsFeatureEnabledAsync(string featureId, CancellationToken token)
            => IsFeatureEnabledAsync(featureId, null, token);

        public Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context)
            => IsFeatureEnabledAsync(featureId, context, CancellationToken.None);

        public async Task<bool> IsFeatureEnabledAsync(string featureId, IFeatureContext context, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(featureId))
                throw new ArgumentNullException(nameof(featureId));

            return await _httpClient.IsFeatureEnabledAsync(featureId, context, token);
        }

        #endregion
    }
}
