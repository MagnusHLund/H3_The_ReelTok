using Moq;
using Xunit;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Tests.Factories;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Tests.Services
{
    public class RecommendationsServiceTests
    {
        private readonly Mock<IRecommendationsRepository> _mockRecommendationsRepository;
        private readonly RecommendationsService _recommendationsService;

        public RecommendationsServiceTests()
        {
            _mockRecommendationsRepository = new Mock<IRecommendationsRepository>();
            _recommendationsService = new RecommendationsService(_mockRecommendationsRepository.Object);
        }

        #region Success Tests

        [Fact]
        public async Task GetVideoRecommendationsForUserAsync_WithValidUser_ReturnsRecommendations()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            byte amountOfVideos = 5;
            List<Guid> recommendedVideoIds = TestDataFactory.CreateVideoIds(amountOfVideos);

            _mockRecommendationsRepository.Setup(x => x.GetRecommendedVideosByUserAsync(userId, amountOfVideos))
                .ReturnsAsync(recommendedVideoIds);

            // Act
            List<Guid> result = await _recommendationsService.GetVideoRecommendationsForUserAsync(userId, amountOfVideos);

            // Assert
            Assert.Equal(recommendedVideoIds, result);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task GetVideoRecommendationsForUserAsync_WithInvalidUser_ReturnsEmptyList()
        {
            // Arrange
            Guid invalidUserId = Guid.Empty;
            byte amountOfVideos = 5;

            _mockRecommendationsRepository.Setup(x => x.GetRecommendedVideosByUserAsync(invalidUserId, amountOfVideos))
                .ReturnsAsync(new List<Guid>());

            // Act
            List<Guid> result = await _recommendationsService.GetVideoRecommendationsForUserAsync(invalidUserId, amountOfVideos);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetVideoRecommendationsForUserAsync_WithZeroAmount_ReturnsEmptyList()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            byte amountOfVideos = 0;

            _mockRecommendationsRepository.Setup(x => x.GetRecommendedVideosByUserAsync(userId, amountOfVideos))
                .ReturnsAsync(new List<Guid>());

            // Act
            List<Guid> result = await _recommendationsService.GetVideoRecommendationsForUserAsync(userId, amountOfVideos);

            // Assert
            Assert.Empty(result);
        }

        #endregion
    }
}
