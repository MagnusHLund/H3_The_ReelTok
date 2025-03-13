using Moq;
using Xunit;
using reeltok.api.users.Entities;
using reeltok.api.users.Services;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reeltok.api.users.Tests.Factories;

namespace reeltok.api.users.Tests.Services
{
    public class LikesServiceTests
    {
        private readonly Mock<ILikesRepository> _mockLikesRepository;
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly LikesService _likesService;

        public LikesServiceTests()
        {
            _mockLikesRepository = TestDataFactory.CreateMockLikesRepository();
            _mockUsersService = new Mock<IUsersService>();
            _likesService = new LikesService(_mockLikesRepository.Object, _mockUsersService.Object);
        }

        #region Success

        [Fact]
        public async Task AddToLikedVideosAsync_WithValidDetails_ReturnsTrue()
        {
            // Arrange
            LikedDetails likedDetails = TestDataFactory
                .CreateLikedDetails(TestDataFactory.GenerateGuid(), TestDataFactory.GenerateGuid());

            UserWithSubscriptionCounts mockUserWithSubscriptionCounts = TestDataFactory
                .CreateMockUserWithSubscriptionCounts(TestDataFactory.GenerateGuid(), "testUser", "test@example.com", 5, 10);
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
            Guid userId = TestDataFactory.GenerateGuid();
            Guid likedVideoId = TestDataFactory.GenerateGuid();
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
            Guid userId = TestDataFactory.GenerateGuid();
            List<Guid> videoIds = new List<Guid> { TestDataFactory.GenerateGuid(), TestDataFactory.GenerateGuid() };
            List<HasUserLikedVideoEntity> likedVideos = TestDataFactory.CreateHasUserLikedVideoEntities(videoIds);

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
            LikedDetails likedDetails = TestDataFactory
                .CreateLikedDetails(TestDataFactory.GenerateGuid(), TestDataFactory.GenerateGuid());

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
            Guid userId = TestDataFactory.GenerateGuid();
            Guid likedVideoId = TestDataFactory.GenerateGuid();
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
            Guid userId = TestDataFactory.GenerateGuid();
            List<Guid> videoIds = new List<Guid> { TestDataFactory.GenerateGuid(), TestDataFactory.GenerateGuid() };

            _mockLikesRepository.Setup(x => x.CheckUserLikesForVideosAsync(It.IsAny<Guid>(), It.IsAny<List<Guid>>()))
                .ThrowsAsync(new Exception("Failed to check likes"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _likesService.GetHasUserLikedVideosAsync(userId, videoIds));
        }

        #endregion Failure
    }
}
