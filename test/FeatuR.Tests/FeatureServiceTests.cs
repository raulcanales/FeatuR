using FeatuR.Tests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _sut = new FeatureService(_store);
        }

        [Fact]
        public void IsFeatureEnabled_HasNoStrategies_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>> { ["default"] = new Dictionary<string, string>() }
            });
            var act = _sut.IsFeatureEnabled(featureId);
            Assert.True(act);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void IsFeatureEnabled_NullOrEmptyFeatureid_ThrowsException(string featureId)
        {
            Assert.Throws<ArgumentNullException>(() => _sut.IsFeatureEnabled(featureId));
        }

        [Fact]
        public void IsFeatureEnabled_FeatureDoesntExist_ReturnsFalse()
        {
            var act = _sut.IsFeatureEnabled("doesnt-exist");
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_FeatureWithoutActivationStrategies_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId
            });
            var act = _sut.IsFeatureEnabled(featureId);
            Assert.False(act);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void IsFeatureEnabled_FeatureWithInvalidActivationStrategies_ReturnsFalse(string strategyId)
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
            var act = _sut.IsFeatureEnabled(featureId);
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_NonExistingStrategyHandler_ReturnsFalse()
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
            var act = _sut.IsFeatureEnabled(featureId);
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_DefaultStrategyHandler_ReturnsTrue()
        {
            var featureId = Guid.NewGuid().ToString();
            InMemoryFeatureStore.Features.TryAdd(featureId, new Feature
            {
                Id = featureId,
                ActivationStrategies = new Dictionary<string, Dictionary<string, string>>
                {
                    ["default"] = new Dictionary<string, string>()
                }
            });
            var act = _sut.IsFeatureEnabled(featureId);
            Assert.True(act);
        }

        [Fact]
        public void GetEnabledFeatures_DefaultStrategyHandler_ReturnsTrue()
        {
            var featureId = string.Empty;
            var enabledFeatures = 4;
            var disabledFeatures = 3;
            for (var i = 0; i < enabledFeatures; i++)
            {
                featureId = Guid.NewGuid().ToString();
                InMemoryFeatureStore.Features.TryAdd(featureId, FeatureExtensions.CreateFeatureWithDefaultStrategy(featureId, enabled: true));
            }

            for (var i = 0; i < disabledFeatures; i++)
            {
                featureId = Guid.NewGuid().ToString();
                InMemoryFeatureStore.Features.TryAdd(featureId, FeatureExtensions.CreateFeatureWithDefaultStrategy(featureId, enabled: false));
            }

            var act = _sut.GetEnabledFeatures(context: null);

            Assert.Equal(enabledFeatures, act.Count());
        }
    }
}
