using Moq;
using Xunit;
using reeltok.api.users.Entities;
using reeltok.api.users.Services;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Tests.Services
{
    public class LikesServiceTests
    {
        private readonly Mock<ILikesRepository> _mockLikesRepository;
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly LikesService _likesService;

        public LikesServiceTests()
        {
            _mockLikesRepository = new Mock<ILikesRepository>();
            _mockUsersService = new Mock<IUsersService>();
            _likesService = new LikesService(_mockLikesRepository.Object, _mockUsersService.Object);
        }

        #region Success

        [Fact]
        public async Task AddToLikedVideosAsync_WithValidDetails_ReturnsTrue()
        {
            // Arrange
            LikedDetails likedDetails = new LikedDetails(Guid.NewGuid(), Guid.NewGuid());

            // Create the required instances for the constructor of UserWithSubscriptionCounts
            Guid userId = Guid.NewGuid();
            UserDetails userDetails = new UserDetails("testUser", "test@example.com", "http://example.com/profile.jpg"); // Assuming this is a valid constructor
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails("true@mail.com"); // Assuming this is a valid constructor
            int subscriptionCount = 5;
            int otherCount = 10;

            ExternalUserEntity externalUserEntity = new ExternalUserEntity(userId, userDetails);

            // Manually create an instance of UserWithSubscriptionCounts
            UserWithSubscriptionCounts mockUserWithSubscriptionCounts = new UserWithSubscriptionCounts(externalUserEntity, subscriptionCount, otherCount);

            // Setup the mock to return the created object
            _mockUsersService.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockUserWithSubscriptionCounts);
            _mockLikesRepository.Setup(x => x.AddToLikedVideoAsync(It.IsAny<LikedVideoEntity>())).ReturnsAsync(true);

            // Act
            bool result = await _likesService.AddToLikedVideosAsync(likedDetails);

            // Assert
            Assert.True(result);
        }



        [Fact]
        public async Task RemoveFromLikedVideosAsync_WithValidUserIdAndVideoId_ReturnsTrue()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid likedVideoId = Guid.NewGuid();
            _mockLikesRepository.Setup(x => x.RemoveFromLikedVideoAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            bool result = await _likesService.RemoveFromLikedVideosAsync(userId, likedVideoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetHasUserLikedVideosAsync_WithValidData_ReturnsCorrectLikedStatus()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> videoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<HasUserLikedVideoEntity> likedVideos = new List<HasUserLikedVideoEntity>
            {
                new HasUserLikedVideoEntity(videoIds[0], true),
                new HasUserLikedVideoEntity(videoIds[1], false)
            };

            _mockLikesRepository.Setup(x => x.CheckUserLikesForVideosAsync(It.IsAny<Guid>(), It.IsAny<List<Guid>>())).ReturnsAsync(likedVideos);

            // Act
            List<HasUserLikedVideoEntity> result = await _likesService.GetHasUserLikedVideosAsync(userId, videoIds);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.True(result[0].HasUserLikedVideo);
            Assert.False(result[1].HasUserLikedVideo);
        }

        #endregion Success

        #region Failure

        [Fact]
        public async Task AddToLikedVideosAsync_WithNonExistingUser_ThrowsException()
        {
            // Arrange
            LikedDetails likedDetails = new LikedDetails(Guid.NewGuid(), Guid.NewGuid());

            // Mock the user service to return null (simulating a non-existing user)
            _mockUsersService.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync((UserWithSubscriptionCounts) null);

            _mockLikesRepository.Setup(x => x.AddToLikedVideoAsync(It.IsAny<LikedVideoEntity>()))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _likesService.AddToLikedVideosAsync(likedDetails));

            // Assert that the exception message contains the expected message
            Assert.Contains("Repository error", exception.Message);
        }



        [Fact]
        public async Task RemoveFromLikedVideosAsync_WithInvalidVideoId_ReturnsFalse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid likedVideoId = Guid.NewGuid();
            _mockLikesRepository.Setup(x => x.RemoveFromLikedVideoAsync(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(false);

            // Act
            bool result = await _likesService.RemoveFromLikedVideosAsync(userId, likedVideoId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetHasUserLikedVideosAsync_WithInvalidData_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> videoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            _mockLikesRepository.Setup(x => x.CheckUserLikesForVideosAsync(It.IsAny<Guid>(), It.IsAny<List<Guid>>()))
                .ThrowsAsync(new Exception("Failed to check likes"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _likesService.GetHasUserLikedVideosAsync(userId, videoIds));
        }

        #endregion Failure
    }
}
