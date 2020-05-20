using FeatuR.Strategies;
using System.Collections.Generic;
using Xunit;

namespace FeatuR.Tests.StrategyTests
{
    public class UserIdStrategyHandlerTests
    {
        private readonly UserIdStrategyHandler _sut;
        private readonly IFeatureContext _context;
        private readonly Dictionary<string, string> _activationParameters;

        public UserIdStrategyHandlerTests()
        {
            _sut = new UserIdStrategyHandler();
            _context = new FeatureContext();
            _activationParameters = new Dictionary<string, string>();
        }

        [Fact]
        public void IsEnabled_UserIdNotSet_ReturnsFalse()
        {
            _activationParameters.Add(UserIdStrategyHandler.AllowedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}user-id{UserIdStrategyHandler.Separator}");
            var act = _sut.IsEnabled(_activationParameters, _context);
            Assert.False(act);
        }

        [Fact]
        public void IsEnabled_HasAllowedUserId_ReturnsTrue()
        {
            var allowedUserId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, allowedUserId);
            _activationParameters.Add(UserIdStrategyHandler.AllowedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}{allowedUserId}{UserIdStrategyHandler.Separator}");
            var act = _sut.IsEnabled(_activationParameters, _context);
            Assert.True(act);
        }

        [Fact]
        public void IsEnabled_HasExcludedUserId_ReturnsFalse()
        {
            var excludedUserId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, excludedUserId);
            _activationParameters.Add(UserIdStrategyHandler.ExcludedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}{excludedUserId}{UserIdStrategyHandler.Separator}");
            var act = _sut.IsEnabled(_activationParameters, _context);
            Assert.False(act);
        }

        [Fact]
        public void IsEnabled_UserNotExcluded_ReturnsTrue()
        {
            var userId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, userId);
            _activationParameters.Add(UserIdStrategyHandler.ExcludedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}another-user-id{UserIdStrategyHandler.Separator}");
            var act = _sut.IsEnabled(_activationParameters, _context);
            Assert.True(act);
        }
    }
}
