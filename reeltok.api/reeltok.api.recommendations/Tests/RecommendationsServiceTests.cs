using Moq;
using Xunit;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Tests
{
    public class RecommendationsServiceTests
    {
        private readonly Mock<IWatchedVideoService> _watchedVideoServiceMock;
        private readonly Mock<IRecommendationsService> _recommendationsServiceMock;
        private readonly Mock<IUsersService> _userRecommendationServiceMock;
        private readonly Mock<IVideoRecommendationService> _videoRecommendationServiceMock;

        public RecommendationsServiceTests()
        {
            _watchedVideoServiceMock = new Mock<IWatchedVideoService>();
            _recommendationsServiceMock = new Mock<IRecommendationsService>();
            _userRecommendationServiceMock = new Mock<IUsersService>();
            _videoRecommendationServiceMock = new Mock<IVideoRecommendationService>();
        }

        #region SUCCESS CASES

        // Test to check if GetAllCategoriesAsync returns a list of categories
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsCategoriesList()
        {
            // Arrange
            List<string> categories = new List<string> { "Action", "Comedy", "Drama" };
            _recommendationsServiceMock.Setup(service => service.GetAllCategoriesAsync())
                .ReturnsAsync(categories);

            // Act
            List<string> result = await _recommendationsServiceMock.Object.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories.Count, result.Count);
        }

        // Test to check if GetTopVideoByUserInterestAsync returns top videos based on user interest
        [Fact]
        public async Task GetTopVideoByUserInterestAsync_ReturnsTopVideos()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> topVideos = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            int amount = 2;

            _recommendationsServiceMock.Setup(service => service.GetTopVideoByUserInterestAsync(userId, amount))
                .ReturnsAsync(topVideos);

            // Act
            List<Guid> result = await _recommendationsServiceMock.Object.GetTopVideoByUserInterestAsync(userId, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(topVideos.Count, result.Count);
        }

        // Test to check if AddRecommendationForUserAsync successfully adds a recommendation and returns true
        [Fact]
        public async Task AddRecommendationForUserAsync_ReturnsTrue()
        {
            // Arrange
            UserInterestDetails userInterestDetails = new UserInterestDetails(Guid.NewGuid());
            UserInterestEntity userInterest = new UserInterestEntity(userInterestDetails);
            int categoryId = 1;

            _userRecommendationServiceMock.Setup(service => service.AddRecommendationForUserAsync(userInterest, categoryId))
                .ReturnsAsync(true);

            // Act
            bool result = await _userRecommendationServiceMock.Object.AddRecommendationForUserAsync(userInterest, categoryId);

            // Assert
            Assert.True(result);
        }

        // Test to check if GetUserInterestAsync returns the correct user interest details
        [Fact]
        public async Task GetUserInterestAsync_ReturnsUserInterest()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            UserInterestDetails userInterestDetails = new UserInterestDetails(userId);
            UserInterestEntity userInterest = new UserInterestEntity(userInterestDetails);

            _userRecommendationServiceMock.Setup(service => service.GetUserInterestAsync(userId))
                .ReturnsAsync(userInterest);

            // Act
            UserInterestEntity? result = await _userRecommendationServiceMock.Object.GetUserInterestAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userInterest.UserInterestDetails.UserId, result.UserInterestDetails.UserId);
        }

        // Test to check if UpdateRecommendationForUserAsync updates a recommendation and returns true
        [Fact]
        public async Task UpdateRecommendationForUserAsync_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int oldCategoryId = 1;
            int newCategoryId = 2;

            _userRecommendationServiceMock.Setup(service => service.UpdateRecommendationForUserAsync(userId, oldCategoryId, newCategoryId))
                .ReturnsAsync(true);

            // Act
            bool result = await _userRecommendationServiceMock.Object.UpdateRecommendationForUserAsync(userId, oldCategoryId, newCategoryId);

            // Assert
            Assert.True(result);
        }

        // Test to check if AddRecommendationForVideoAsync successfully adds a recommendation for a video and returns true
        [Fact]
        public async Task AddRecommendationForVideoAsync_ReturnsTrue()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            VideoCategoryDetails videoCategoryDetails = new VideoCategoryDetails(videoId);
            VideoCategoryEntity videoCategory = new VideoCategoryEntity(videoCategoryDetails);
            int categoryId = 1;

            _videoRecommendationServiceMock.Setup(service => service.AddRecommendationForVideoAsync(videoCategory, categoryId))
                .ReturnsAsync(true);

            // Act
            bool result = await _videoRecommendationServiceMock.Object.AddRecommendationForVideoAsync(videoCategory, categoryId);

            // Assert
            Assert.True(result);
        }

        // Test to check if GetVideoCategoryAsync returns the correct video category details
        [Fact]
        public async Task GetVideoCategoryAsync_ReturnsVideoCategory()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            VideoCategoryDetails videoCategoryDetails = new VideoCategoryDetails(videoId);
            VideoCategoryEntity videoCategory = new VideoCategoryEntity(videoCategoryDetails);

            _videoRecommendationServiceMock.Setup(service => service.GetVideoCategoryAsync(videoId))
                .ReturnsAsync(videoCategory);

            // Act
            VideoCategoryEntity result = await _videoRecommendationServiceMock.Object.GetVideoCategoryAsync(videoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(videoCategory.VideoCategoryDetails.VideoId, result.VideoCategoryDetails.VideoId);
        }

        // Test to check if AddOrUpdateWatchedVideoAsync successfully updates a watched video and returns success
        [Fact]
        public async Task AddOrUpdateWatchedVideoAsync_ReturnsSuccess()
        {
            // Arrange
            Guid dummy = new Guid();
            WatchedVideoDetails watchedVideoDetails = new WatchedVideoDetails(dummy, dummy, 1, 1);
            WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(watchedVideoDetails);

            _watchedVideoServiceMock.Setup(service => service.AddOrUpdateWatchedVideoAsync(watchedVideoEntity))
                .ReturnsAsync((true, "Successfully updated"));

            // Act
            (bool, string) result = await _watchedVideoServiceMock.Object.AddOrUpdateWatchedVideoAsync(watchedVideoEntity);

            // Assert
            Assert.True(result.Item1);
            Assert.Equal("Successfully updated", result.Item2);
        }

        #endregion

        #region  FAILURE CASES

        // Test to check if GetAllCategoriesAsync returns an empty list when no categories exist
        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsEmptyList()
        {
            // Arrange
            _recommendationsServiceMock.Setup(service => service.GetAllCategoriesAsync())
                .ReturnsAsync(new List<string>());

            // Act
            List<string> result = await _recommendationsServiceMock.Object.GetAllCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test to check if GetTopVideoByUserInterestAsync returns an empty list for an invalid user
        [Fact]
        public async Task GetTopVideoByUserInterestAsync_ReturnsEmptyList()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int amount = 2;

            _recommendationsServiceMock.Setup(service => service.GetTopVideoByUserInterestAsync(userId, amount))
                .ReturnsAsync(new List<Guid>());

            // Act
            List<Guid> result = await _recommendationsServiceMock.Object.GetTopVideoByUserInterestAsync(userId, amount);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test to check if AddRecommendationForUserAsync fails and returns false
        [Fact]
        public async Task AddRecommendationForUserAsync_ReturnsFalse()
        {
            // Arrange
            UserInterestEntity userInterest = new UserInterestEntity(new UserInterestDetails(Guid.NewGuid()));
            int categoryId = 1;

            _userRecommendationServiceMock.Setup(service => service.AddRecommendationForUserAsync(userInterest, categoryId))
                .ReturnsAsync(false);

            // Act
            bool result = await _userRecommendationServiceMock.Object.AddRecommendationForUserAsync(userInterest, categoryId);

            // Assert
            Assert.False(result);
        }

        // Test to check if GetUserInterestAsync returns null for an invalid user
        [Fact]
        public async Task GetUserInterestAsync_ReturnsNull()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _userRecommendationServiceMock.Setup(service => service.GetUserInterestAsync(userId))
                .ReturnsAsync((UserInterestEntity?)null);

            // Act
            UserInterestEntity? result = await _userRecommendationServiceMock.Object.GetUserInterestAsync(userId);

            // Assert
            Assert.Null(result);
        }

        // Test to check if UpdateRecommendationForUserAsync fails and returns false
        [Fact]
        public async Task UpdateRecommendationForUserAsync_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            int oldCategoryId = 1;
            int newCategoryId = 2;

            _userRecommendationServiceMock.Setup(service => service.UpdateRecommendationForUserAsync(userId, oldCategoryId, newCategoryId))
                .ReturnsAsync(false);

            // Act
            bool result = await _userRecommendationServiceMock.Object.UpdateRecommendationForUserAsync(userId, oldCategoryId, newCategoryId);

            // Assert
            Assert.False(result);
        }

        // Test to check if GetVideoCategoryAsync returns null for an invalid video
        [Fact]
        public async Task GetVideoCategoryAsync_ReturnsNull()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();

            _videoRecommendationServiceMock.Setup(service => service.GetVideoCategoryAsync(videoId))
                .ReturnsAsync((VideoCategoryEntity?)null);

            // Act
            VideoCategoryEntity? result = await _videoRecommendationServiceMock.Object.GetVideoCategoryAsync(videoId);

            // Assert
            Assert.Null(result);
        }

        // Test to check if AddOrUpdateWatchedVideoAsync fails and returns a failure message
        [Fact]
        public async Task AddOrUpdateWatchedVideoAsync_ReturnsFailure()
        {
            // Arrange
            WatchedVideoEntity watchedVideoEntity = new WatchedVideoEntity(new WatchedVideoDetails(Guid.NewGuid(), Guid.NewGuid(), 1, 1));

            _watchedVideoServiceMock.Setup(service => service.AddOrUpdateWatchedVideoAsync(watchedVideoEntity))
                .ReturnsAsync((false, "Update failed"));

            // Act
            (bool success, string message) result = await _watchedVideoServiceMock.Object.AddOrUpdateWatchedVideoAsync(watchedVideoEntity);

            // Assert
            Assert.False(result.success);
            Assert.Equal("Update failed", result.message);
        }

        #endregion

    }
}
