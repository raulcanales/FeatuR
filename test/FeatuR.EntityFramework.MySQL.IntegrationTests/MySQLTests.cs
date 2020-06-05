using FeatuR.EntityFramework.MySQL.IntegrationTests.Data;
using FeatuR.EntityFramework.MySQL.IntegrationTests.Fixtures;
using FeatuR.Strategies;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FeatuR.EntityFramework.MySQL.IntegrationTests
{
    public class MySQLTests : IClassFixture<MySQLFixture>
    {
        private readonly MySQLFixture _fixture;

        public MySQLTests(MySQLFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void IsFeatureEnabled_DefaultFeature_ReturnsTrue()
        {
            var context = new FeatureContext();
            var act = _fixture.Sut.IsFeatureEnabled(Features.DefaultFeature.Id, context);
            Assert.True(act);
        }

        [Fact]
        public void IsFeatureEnabled_OnlyUser444_UserIs444_ReturnsTrue()
        {
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.UserIdParameterName] = "444"
                }
            };
            var act = _fixture.Sut.IsFeatureEnabled(Features.OnlyForUser444.Id, context);
            Assert.True(act);
        }

        [Fact]
        public void IsFeatureEnabled_OnlyUser444_UserIsNot444_ReturnsFalse()
        {
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.UserIdParameterName] = "something-else"
                }
            };
            var act = _fixture.Sut.IsFeatureEnabled(Features.OnlyForUser444.Id, context);
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_ExcludeUsePepe_UserIsPepe_ReturnsFalse()
        {
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.UserIdParameterName] = "Pepe"
                }
            };
            var act = _fixture.Sut.IsFeatureEnabled(Features.ExcludeUserPepe.Id, context);
            Assert.False(act);
        }

        [Fact]
        public void IsFeatureEnabled_ExcludeUsePepe_UserIsNotPepe_ReturnsTrue()
        {
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.UserIdParameterName] = "something-else"
                }
            };
            var act = _fixture.Sut.IsFeatureEnabled(Features.ExcludeUserPepe.Id, context);
            Assert.True(act);
        }

        [Fact]
        public void IsFeatureEnabled_DisabledFeature_ReturnsFalse()
        {
            var context = new FeatureContext();
            var act = _fixture.Sut.IsFeatureEnabled(Features.DisabledFeature.Id, context);
            Assert.False(act);
        }

        [Fact]
        public void GetEnabledFeatures_UserIs444_ReturnsEnabledFeatures()
        {
            var context = new FeatureContext
            {
                Parameters = new Dictionary<string, string>
                {
                    [UserIdStrategyHandler.UserIdParameterName] = "444"
                }
            };
            var act = _fixture.Sut.GetEnabledFeatures(context);
            Assert.Equal(3, act.Count());
        }
    }
}
