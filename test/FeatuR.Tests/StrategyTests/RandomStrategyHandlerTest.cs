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
        private readonly Dictionary<string, string> _activationStrategies;

        public RandomStrategyHandlerTest()
        {
            _sut = new RandomStrategyHandler();
            _context = new FeatureContext();
            _activationStrategies = new Dictionary<string, string>();
        }

        [Fact]
        public async Task IsEnabled_ShouldBeRandomlyEnabled()
        {
            var results = new List<bool>();
            _context.Parameters.Add("", "");
            _activationStrategies.Add("", "");

            for (int i = 0; i < 100; i++)
            {
                results.Add(_sut.IsEnabled(_activationStrategies, _context));
                results.Add(await _sut.IsEnabledAsync(_activationStrategies, _context, CancellationToken.None));
            }

            Assert.Contains(true, results);
            Assert.Contains(false, results);
        }
    }
}