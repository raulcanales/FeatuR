using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FeatuR.EntityFramework.Tests
{
    public class EntityFrameworkFeatureStoreTests : IClassFixture<EntityFrameworkFixture>
    {
        private readonly EntityFrameworkFixture _fixture;

        public EntityFrameworkFeatureStoreTests(EntityFrameworkFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetEnabledFeatures_HasThreeFeatures_ReturnsTwoEnabled()
        {
            var act = _fixture.Sut.GetEnabledFeatures();
            Assert.Equal(2, act.Count());
        }

        [Fact]
        public async Task GetEnabledFeaturesAsync_HasThreeFeatures_ReturnsTwoEnabled()
        {
            var act = await _fixture.Sut.GetEnabledFeaturesAsync();
            Assert.Equal(2, act.Count());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void GetFeatureById_WrongId_ThrowsException(string featureId)
        {
            Assert.Throws<ArgumentNullException>(() => _fixture.Sut.GetFeatureById(featureId));
        }

        [Fact]
        public void GetFeatureById_Success()
        {
            var act = _fixture.Sut.GetFeatureById(Features.Feature1.Id);
            Assert.Equal(Features.Feature1, act);
        }

        [Fact]
        public void GetFeatureById_NonExistingFeature_ReturnsDefault()
        {
            var act = _fixture.Sut.GetFeatureById("i-dont-exist");
            Assert.Equal(default, act);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task GetFeatureByIdAsync_WrongId_ThrowsException(string featureId)
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _fixture.Sut.GetFeatureByIdAsync(featureId));
        }

        [Fact]
        public async Task GetFeatureByIdAsync_Success()
        {
            var act = await _fixture.Sut.GetFeatureByIdAsync(Features.Feature1.Id);
            Assert.Equal(Features.Feature1, act);
        }

        [Fact]
        public async Task GetFeatureByIdAsync_NonExistingFeature_ReturnsDefault()
        {
            var act = await _fixture.Sut.GetFeatureByIdAsync("i-dont-exist");
            Assert.Equal(default, act);
        }
    }
}
