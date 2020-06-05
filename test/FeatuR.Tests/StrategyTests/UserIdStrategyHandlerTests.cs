using FeatuR.Strategies;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task IsEnabled_UserIdNotSet_ReturnsFalse()
        {
            _activationParameters.Add(UserIdStrategyHandler.AllowedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}user-id{UserIdStrategyHandler.Separator}");
            Assert.False(_sut.IsEnabled(_activationParameters));
            Assert.False(_sut.IsEnabled(_activationParameters, _context));
            Assert.False(await _sut.IsEnabledAsync(_activationParameters, CancellationToken.None));
            Assert.False(await _sut.IsEnabledAsync(_activationParameters, _context, CancellationToken.None));
        }

        [Fact]
        public async Task IsEnabled_HasAllowedUserId_ReturnsTrue()
        {
            var allowedUserId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, allowedUserId);
            _activationParameters.Add(UserIdStrategyHandler.AllowedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}{allowedUserId}{UserIdStrategyHandler.Separator}");
            Assert.True(_sut.IsEnabled(_activationParameters, _context));
            Assert.True(await _sut.IsEnabledAsync(_activationParameters, _context, CancellationToken.None));
        }

        [Fact]
        public async Task IsEnabled_HasExcludedUserId_ReturnsFalse()
        {
            var excludedUserId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, excludedUserId);
            _activationParameters.Add(UserIdStrategyHandler.ExcludedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}{excludedUserId}{UserIdStrategyHandler.Separator}");
            Assert.False(_sut.IsEnabled(_activationParameters, _context));
            Assert.False(await _sut.IsEnabledAsync(_activationParameters, _context, CancellationToken.None));
        }

        [Fact]
        public async Task IsEnabled_UserNotExcluded_ReturnsTrue()
        {
            var userId = "abcd123";
            _context.Parameters.Add(UserIdStrategyHandler.UserIdParameterName, userId);
            _activationParameters.Add(UserIdStrategyHandler.ExcludedUserIdsParameterName, $"{UserIdStrategyHandler.Separator}another-user-id{UserIdStrategyHandler.Separator}");
            Assert.True(_sut.IsEnabled(_activationParameters, _context));
            Assert.True(await _sut.IsEnabledAsync(_activationParameters, _context, CancellationToken.None));
        }
    }
}
