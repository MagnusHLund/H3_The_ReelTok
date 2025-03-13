using Moq;
using Xunit;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Services;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Tests.Factories;
using reeltok.api.videos.Interfaces.Services;

namespace reeltok.api.videos.Tests.Services
{
    public class LikesServiceTests
    {
        private readonly Mock<IExternalApiService> _mockExternalApiService;
        private readonly Mock<ILikesRepository> _mockLikesRepository;
        private readonly LikesService _likesService;

        public LikesServiceTests()
        {
            _mockExternalApiService = new Mock<IExternalApiService>();
            _mockLikesRepository = new Mock<ILikesRepository>();
            _likesService = new LikesService(_mockExternalApiService.Object, _mockLikesRepository.Object);
        }

        [Fact]
        public async Task LikeVideoAsync_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            _mockExternalApiService.Setup(x => x.LikeVideoAsync(userId, videoId)).ReturnsAsync(true);

            // Act
            bool result = await _likesService.LikeVideoAsync(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task LikeVideoAsync_WithInvalidParameters_ReturnFailure()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            _mockExternalApiService.Setup(x => x.LikeVideoAsync(userId, videoId)).ReturnsAsync(false);

            // Act
            bool result = await _likesService.LikeVideoAsync(userId, videoId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RemoveLikeFromVideoAsync_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            _mockExternalApiService.Setup(x => x.RemoveLikeFromVideoAsync(userId, videoId)).ReturnsAsync(true);

            // Act
            bool result = await _likesService.RemoveLikeFromVideoAsync(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveLikeFromVideoAsync_WithInvalidParameters_ReturnFailure()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            _mockExternalApiService.Setup(x => x.RemoveLikeFromVideoAsync(userId, videoId)).ReturnsAsync(false);

            // Act
            bool result = await _likesService.RemoveLikeFromVideoAsync(userId, videoId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetLikesForVideos_WithValidParameters_ReturnVideoLikes()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> videoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<HasUserLikedVideoEntity> hasUserLikedVideo = new List<HasUserLikedVideoEntity>
            {
                TestDataFactory.CreateHasUserLikedVideoEntity(true),
                TestDataFactory.CreateHasUserLikedVideoEntity(false)
            };
            List<TotalVideoLikesEntity> videoTotalLikes = new List<TotalVideoLikesEntity>
            {
                TestDataFactory.CreateTotalVideoLikesEntity(10),
                TestDataFactory.CreateTotalVideoLikesEntity(5)
            };

            _mockExternalApiService.Setup(x => x.HasUserLikedVideosAsync(userId, videoIds)).ReturnsAsync(hasUserLikedVideo);
            _mockLikesRepository.Setup(x => x.GetTotalLikesForVideosAsync(videoIds)).ReturnsAsync(videoTotalLikes);

            // Act
            List<VideoLikesEntity> result = await _likesService.GetLikesForVideosAsync(userId, videoIds);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal((uint)10, result[0].VideoLikes.TotalLikes);
            Assert.True(result[0].VideoLikes.UserHasLikedVideo);
            Assert.Equal((uint)5, result[1].VideoLikes.TotalLikes);
            Assert.False(result[1].VideoLikes.UserHasLikedVideo);
        }
    }
}
