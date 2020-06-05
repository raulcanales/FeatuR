using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace FeatuR.RestClient.Tests.Fixtures
{
    public class WireMockFixture
    {
        public WireMockServer MockServer;
        public FeatuRSettings Settings;

        public WireMockFixture()
        {
            MockServer = WireMockServer.Start();
            Settings = new FeatuRSettings
            {
                BaseUrl = MockServer.Urls.First()
            };
        }

        public void MockIsFeatureEnabledResponse(string featureId, bool isEnabled, Dictionary<string, string> parameters = null)
        {
            var request = Request.Create()
                .WithPath("/" + FeatuRSettings.DefaultIsFeatureEnabledEndpoint.Replace("{featureId}", featureId))
                .UsingGet();

            if (parameters != null)
                foreach (var parameter in parameters)
                    request.WithHeader(parameter.Key, parameter.Value, WireMock.Matchers.MatchBehaviour.AcceptOnMatch);

            MockServer
              .Given(request)
              .RespondWith(
                Response.Create()
                  .WithStatusCode(200)
                  .WithBody(isEnabled.ToString().ToLower())
              );
        }

        public void MockEvaluateFeaturesResponse(Dictionary<string, bool> features)
        {
            MockServer
              .Given(
                Request.Create()
                .WithPath("/" + FeatuRSettings.DefaultEvaluateFeaturesEndpoint)
                .UsingPost()
              )
              .RespondWith(
                Response.Create()
                  .WithStatusCode(200)
                  .WithHeader("Content-Type", "application/json")
                  .WithBody(JsonConvert.SerializeObject(features))
              );
        }

        public void MockGetAllEnabledFeaturesResponse(IList<string> features)
        {
            MockServer
              .Given(
                Request.Create()
                .WithPath("/" + FeatuRSettings.DefaultGetAllEnabledFeaturesEndpoint)
                .UsingGet()
              )
              .RespondWith(
                Response.Create()
                  .WithStatusCode(200)
                  .WithHeader("Content-Type", "application/json")
                  .WithBody(JsonConvert.SerializeObject(features))
              );
        }
    }
}
