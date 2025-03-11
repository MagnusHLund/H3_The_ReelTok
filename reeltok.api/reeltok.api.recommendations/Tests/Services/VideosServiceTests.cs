using Moq;
using Xunit;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Tests.Factories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;


namespace reeltok.api.recommendations.Tests.Services
{
    public class VideosServiceTests
    {
        private readonly Mock<IVideoCategoriesRepository> _mockVideoCategoriesRepository;
        private readonly Mock<IRecommendationsService> _mockRecommendationsService;
        private readonly VideosService _videosService;

        public VideosServiceTests()
        {
            _mockVideoCategoriesRepository = new Mock<IVideoCategoriesRepository>();
            _mockRecommendationsService = new Mock<IRecommendationsService>();
            _videosService = new VideosService(
                _mockVideoCategoriesRepository.Object,
                _mockRecommendationsService.Object
            );
        }

        #region Success Tests

        [Fact]
        public async Task GetRecommendedVideosForUsersFeedAsync_WithValidParameters_ReturnsRecommendedVideos()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            byte amount = 2;
            List<Guid> recommendedVideoIds = TestDataFactory.CreateVideoIds(amount);

            _mockRecommendationsService.Setup(x => x.GetVideoRecommendationsForUserAsync(userId, amount)).ReturnsAsync(recommendedVideoIds);

            // Act
            List<Guid> result = await _videosService.GetRecommendedVideosForUsersFeedAsync(userId, amount);

            // Assert
            Assert.Equal(recommendedVideoIds.Count, result.Count);
            for (int i = 0; i < recommendedVideoIds.Count; i++)
            {
                Assert.Equal(recommendedVideoIds[i], result[i]);
            }
        }

        [Fact]
        public async Task AddVideoCategoryAsync_WithValidParameters_ReturnsSavedCategory()
        {
            // Arrange
            Guid videoId = TestDataFactory.CreateGuid();
            CategoryType categoryType = CategoryType.Gaming;
            CategoryEntity categoryEntity = TestDataFactory.CreateCategoryEntity(categoryType);

            _mockVideoCategoriesRepository
                .Setup(x => x.AddVideoCategoryAsync(It.IsAny<CategoryVideoCategoryEntity>()))
                .ReturnsAsync((uint) 1); 

            // Act
            CategoryType result = await _videosService.AddVideoCategoryAsync(videoId, categoryType);

            // Assert
            Assert.Equal(categoryType, result);
        }


        #endregion

        #region Failure Tests

        [Fact]
        public async Task GetRecommendedVideosForUsersFeedAsync_WithInvalidAmount_ReturnsEmptyList()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            byte amount = 0; 
            List<Guid> recommendedVideoIds = new List<Guid>();

            _mockRecommendationsService.Setup(x => x.GetVideoRecommendationsForUserAsync(userId, amount)).ReturnsAsync(recommendedVideoIds);

            // Act
            List<Guid> result = await _videosService.GetRecommendedVideosForUsersFeedAsync(userId, amount);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task AddVideoCategoryAsync_WithInvalidCategory_ThrowsException()
        {
            // Arrange
            Guid videoId = TestDataFactory.CreateGuid();
            CategoryType invalidCategory = (CategoryType) 999; 

            _mockVideoCategoriesRepository.Setup(x => x.AddVideoCategoryAsync(It.IsAny<CategoryVideoCategoryEntity>())).ThrowsAsync(new Exception("Invalid category"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _videosService.AddVideoCategoryAsync(videoId, invalidCategory));
        }

        #endregion
    }
}
