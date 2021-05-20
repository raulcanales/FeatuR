using FeatuR.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FeatuR.Tests
{
    public class FeatureServiceTests
    {
        private FeatureService _sut;
        private InMemoryFeatureStore _store;

        public FeatureServiceTests()
        {
            _store = new InMemoryFeatureStore();
            _sut = new FeatureService(_store, new StrategyHandlerStore());
        }

        [Fact]
        public async Task IsFeatureEnabled_HasNoStrategies_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                Enabled = true,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>> { ["default"] = new Dictionary<string, string>() }
            });
            Assert.True(_sut.IsFeatureEnabled(featureId));
            Assert.True(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task IsFeatureEnabled_NullOrEmptyFeatureid_ThrowsException(string featureId)
        {
            Assert.Throws<ArgumentNullException>(() => _sut.IsFeatureEnabled(featureId));
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_FeatureDoesntExist_ReturnsFalse()
        {
            var featureId = "doesnt-exist";
            Assert.False(_sut.IsFeatureEnabled(featureId));
            Assert.False(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_FeatureWithoutActivationStrategies_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId
            });
            Assert.False(_sut.IsFeatureEnabled(featureId));
            Assert.False(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public async Task IsFeatureEnabled_FeatureWithInvalidActivationStrategies_ReturnsFalse(string strategyId)
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                {
                    [strategyId] = new Dictionary<string, string>()
                }
            });
            Assert.False(_sut.IsFeatureEnabled(featureId));
            Assert.False(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_NonExistingStrategyHandler_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                {
                    ["doesntexist"] = new Dictionary<string, string>()
                }
            });
            Assert.False(_sut.IsFeatureEnabled(featureId));
            Assert.False(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_DefaultStrategyHandler_ReturnsTrue()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                Enabled = true,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                {
                    ["default"] = new Dictionary<string, string>()
                }
            });
            Assert.True(_sut.IsFeatureEnabled(featureId));
            Assert.True(await _sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task GetEnabledFeatures_DefaultStrategyHandler_ReturnsTrue()
        {
            string featureId;
            var enabledFeatures = 4;
            var disabledFeatures = 3;
            var disabledFeatureIds = new List<string>();
            for (var i = 0; i < enabledFeatures; i++)
            {
                featureId = Guid.NewGuid().ToString();
                InMemoryFeatureStore.Features.TryAdd(featureId, FeatureExtensions.CreateFeatureWithDefaultStrategy(featureId, enabled: true));
            }

            for (var i = 0; i < disabledFeatures; i++)
            {
                featureId = Guid.NewGuid().ToString();
                disabledFeatureIds.Add(featureId);
                InMemoryFeatureStore.Features.TryAdd(featureId, FeatureExtensions.CreateFeatureWithDefaultStrategy(featureId, enabled: false));
            }

            var features = _sut.GetEnabledFeatures(context: null);
            var featuresAsync = await _sut.GetEnabledFeaturesAsync(context: null, token: CancellationToken.None);
            Assert.Equal(0, features.Count(p => disabledFeatureIds.Any(f => f == p)));
            Assert.Equal(0, featuresAsync.Count(p => disabledFeatureIds.Any(f => f == p)));
        }
    }
}
