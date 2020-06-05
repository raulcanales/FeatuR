using FeatuR.RestClient.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FeatuR.RestClient.Tests
{
    public class RestFeatureServiceTests : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _fixture;

        public RestFeatureServiceTests(WireMockFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsFeatureEnabled_ServerIsDown_ReturnsFalse()
        {
            var client = new FeatureHttpClient(new HttpClient(), new FeatuRSettings { BaseUrl = "http://doesntexist" });
            var sut = new RestFeatureService(client);
            var act = sut.IsFeatureEnabled("test");
            Assert.False(act);
        }

        [Fact]
        public async Task IsFeatureEnabled_FeatureIsEnabled_ReturnsTrue()
        {
            var featureId = Guid.NewGuid().ToString();
            _fixture.MockIsFeatureEnabledResponse(featureId, isEnabled: true);
            var client = new FeatureHttpClient(new HttpClient(), _fixture.Settings);
            var sut = new RestFeatureService(client);
            Assert.True(sut.IsFeatureEnabled(featureId));
            Assert.True(await sut.IsFeatureEnabledAsync(featureId));
            Assert.True(await sut.IsFeatureEnabledAsync(featureId, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_FeatureIsEnabled_NotForTheUser_ReturnsFalse()
        {
            var featureId = Guid.NewGuid().ToString();
            _fixture.MockIsFeatureEnabledResponse(featureId, isEnabled: true, parameters: new Dictionary<string, string>
            {
                ["userid"] = "1"
            });
            var client = new FeatureHttpClient(new HttpClient(), _fixture.Settings);
            var sut = new RestFeatureService(client);
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string> { ["userid"] = "2" }
            };
            Assert.False(sut.IsFeatureEnabled(featureId, context));
            Assert.False(await sut.IsFeatureEnabledAsync(featureId, context));
            Assert.False(await sut.IsFeatureEnabledAsync(featureId, context, CancellationToken.None));
        }

        [Fact]
        public async Task IsFeatureEnabled_FeatureIsEnabled_ForTheUser_ReturnsTrue()
        {
            var featureId = Guid.NewGuid().ToString();
            _fixture.MockIsFeatureEnabledResponse(featureId, isEnabled: true, parameters: new Dictionary<string, string>
            {
                ["userid"] = "1"
            });
            var client = new FeatureHttpClient(new HttpClient(), _fixture.Settings);
            var sut = new RestFeatureService(client);
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string> { ["userid"] = "1" }
            };
            Assert.True(sut.IsFeatureEnabled(featureId, context));
            Assert.True(await sut.IsFeatureEnabledAsync(featureId, context));
            Assert.True(await sut.IsFeatureEnabledAsync(featureId, context, CancellationToken.None));
        }

        [Fact]
        public async Task GetEnabledFeatures_ReturnsAllEnabled()
        {
            var features = new List<string>
            {
                "feature1",
                "feature2"
            };
            _fixture.MockGetAllEnabledFeaturesResponse(features);
            var client = new FeatureHttpClient(new HttpClient(), _fixture.Settings);
            var sut = new RestFeatureService(client);
            var context = new FeatureContext();
            Assert.Equal(features, sut.GetEnabledFeatures(context));
            Assert.Equal(features, await sut.GetEnabledFeaturesAsync(context));
            Assert.Equal(features, await sut.GetEnabledFeaturesAsync(context, CancellationToken.None));
        }

        [Fact]
        public async Task EvaluateFeatures_ReturnsAllEnabled()
        {
            var expected = new Dictionary<string, bool>
            {
                ["feature1"] = true,
                ["feature2"] = false,
            };
            var features = new List<string>
            {
                "feature1",
                "feature2"
            };
            _fixture.MockEvaluateFeaturesResponse(expected);
            var client = new FeatureHttpClient(new HttpClient(), _fixture.Settings);
            var sut = new RestFeatureService(client);
            var context = new FeatureContext();
            Assert.Equal(expected, sut.EvaluateFeatures(features, context));
            Assert.Equal(expected, await sut.EvaluateFeaturesAsync(features, context));
            Assert.Equal(expected, await sut.EvaluateFeaturesAsync(features, context, CancellationToken.None));
        }
    }
}
