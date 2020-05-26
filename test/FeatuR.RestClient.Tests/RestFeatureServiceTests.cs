using System;
using System.Linq;
using System.Net.Http;
using WireMock.Server;
using Xunit;

namespace FeatuR.RestClient.Tests
{
    public class RestFeatureServiceTests
    {
        private readonly WireMockServer _mockApi;

        public RestFeatureServiceTests()
        {
            _mockApi = WireMockServer.Start();
        }

        [Fact]
        public void IsFeatureEnabled_ServerIsDown_ReturnsFalse()
        {
            var client = new FeatureHttpClient(new HttpClient(), new RestFeatureServiceSettings
            {
                BaseUrl = "http://doesntexist"
            });
            var sut = new RestFeatureService(client);
            var act = sut.IsFeatureEnabled("test");
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_FeatureIsEnabled_ReturnsTrue()
        {
            var client = new FeatureHttpClient(new HttpClient(), new RestFeatureServiceSettings
            {
                BaseUrl = _mockApi.Urls.First()
            });
            var sut = new RestFeatureService(client);
            var act = sut.IsFeatureEnabled("test");
            Assert.False(act);
        }
    }
}
