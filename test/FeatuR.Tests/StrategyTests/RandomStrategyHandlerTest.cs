using FeatuR.Strategies;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FeatuR.Tests.StrategyTests
{
    public class RandomStrategyHandlerTest
    {
        private readonly RandomStrategyHandler _sut;
        private readonly IFeatureContext _context;
        private readonly Dictionary<string, string> _activationParameters;

        public RandomStrategyHandlerTest()
        {
            _sut = new RandomStrategyHandler();
            _context = new FeatureContext();
            _activationParameters = new Dictionary<string, string>();
        }

        [Fact]
        public async Task IsEnabled_ShouldBeRandomlyEnabled()
        {
            var results = new List<bool>();

            for (int i = 0; i < 100; i++)
            {
                results.Add(_sut.IsEnabled(_activationParameters, _context));
                results.Add(await _sut.IsEnabledAsync(_activationParameters, _context, CancellationToken.None));
            }

            Assert.Contains(true, results);
            Assert.Contains(false, results);
        }
    }
}