using Moq;
using Xunit;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Services;

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

        [Fact]
        public async Task GetVideoRecommendationsForUserAsync_WithValidParameters_ReturnsRecommendedVideos()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amountOfVideos = 3;
            List<Guid> recommendedVideos = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            _mockRecommendationsRepository
                .Setup(repo => repo.GetRecommendedVideosByUserAsync(userId, amountOfVideos))
                .ReturnsAsync(recommendedVideos);

            // Act
            List<Guid> result = await _recommendationsService.GetVideoRecommendationsForUserAsync(userId, amountOfVideos);

            // Assert
            Assert.Equal(recommendedVideos, result);
            _mockRecommendationsRepository.Verify(repo => repo.GetRecommendedVideosByUserAsync(userId, amountOfVideos), Times.Once);
        }

        [Fact]
        public async Task GetVideoRecommendationsForUserAsync_WithInvalidUserId_ThrowsException()
        {
            // Arrange
            Guid invalidUserId = Guid.Empty;
            byte amountOfVideos = 3;

            _mockRecommendationsRepository
                .Setup(repo => repo.GetRecommendedVideosByUserAsync(invalidUserId, amountOfVideos))
                .ThrowsAsync(new ArgumentException("Invalid user ID."));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _recommendationsService.GetVideoRecommendationsForUserAsync(invalidUserId, amountOfVideos));
        }
    }
}
