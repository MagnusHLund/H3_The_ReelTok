using Moq;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.Services;
using reeltok.api.users.ValueObjects;
using Xunit;

namespace reeltok.api.users.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }
        
        #region Create

        [Fact]
        public async Task CreateAsync_CallsCreateUserAsync()
        {
            // Arrange
            var hiddenDetails = new HiddenUserDetails("testuser@example.com");
            var userDetails = new UserDetails("testuser", "https://example.com", "https://example.com/profile.jpg", hiddenDetails);
            var userProfileData = new UserProfileData(Guid.NewGuid(), userDetails);

            // Act
            await _userService.CreateAsync(userProfileData);

            // Assert
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.Is<UserDetails>(
                u => u.UserName == userDetails.UserName &&
                     u.ProfileUrl == userDetails.ProfileUrl &&
                     u.ProfilePictureUrl == userDetails.ProfilePictureUrl &&
                     u.HiddenDetails.Email == userDetails.HiddenDetails.Email
            )), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenInvalidDataProvided()
        {
            // Arrange: Create an invalid user (e.g., missing required fields)
            var invalidUser = new UserProfileData(Guid.NewGuid(), null); // 'null' Details should cause an error

            // Act & Assert: Expect an exception when calling CreateAsync
            await Assert.ThrowsAsync<ArgumentException>(() => _userService.CreateAsync(invalidUser));

            // Ensure the repository's CreateUserAsync method was never called
            _userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.IsAny<UserDetails>()), Times.Never);
        }

        #endregion

        #region GetById

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();  // We are only interested in the userId
            var userProfileData = new UserProfileData(userId, new UserDetails("testuser", "https://example.com", "https://example.com/profile.jpg", new HiddenUserDetails("testuser@example.com")));

            // Mock the repository to return the user when GetUserByIdAsync is called
            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync(userProfileData);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);  // Ensure user is returned
            Assert.Equal(userId, result.UserId);  // Ensure the returned user matches the expected userId
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Mock the repository to return null when GetUserByIdAsync is called
            _userRepositoryMock
                .Setup(repo => repo.GetUserByIdAsync(userId))
                .ReturnsAsync((UserProfileData?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.GetUserByIdAsync(userId));
            Assert.Equal("User not found", exception.Message);  // Ensure the exception message is correct
        }

        #endregion

        #region Subscribe

        [Fact]
        public async Task SubscribeAsync_ShouldSubscribeUserToAnotherUser()
        {
            // Arrange
            var userId = Guid.NewGuid(); // User who wants to subscribe
            var subscribeUserId = Guid.NewGuid(); // User to be subscribed to

            var user = new UserProfileData(userId, new UserDetails("user1", "https://example.com", "https://example.com/profile.jpg", new HiddenUserDetails("user1@example.com")));
            var subscribeUser = new UserProfileData(subscribeUserId, new UserDetails("user2", "https://example.com", "https://example.com/profile2.jpg", new HiddenUserDetails("user2@example.com")));

            // Mock repository responses
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync(subscribeUser);
            _userRepositoryMock.Setup(repo => repo.AddUserToSubscriptionAsync(userId, subscribeUserId)).Returns(Task.CompletedTask);

            // Act
            await _userService.SubscribeAsync(userId, subscribeUserId);

            // Assert
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once); // Ensure GetUserByIdAsync is called for the first user
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync(subscribeUserId), Times.Once); // Ensure GetUserByIdAsync is called for the second user
            _userRepositoryMock.Verify(repo => repo.AddUserToSubscriptionAsync(userId, subscribeUserId), Times.Once); // Ensure AddUserToSubscriptionAsync is called
        }

        [Fact]
        public async Task SubscribeAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid(); // User who wants to subscribe
            var subscribeUserId = Guid.NewGuid(); // User to be subscribed to

            // Mock the case where the first user doesn't exist
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((UserProfileData?)null); // user doesn't exist
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync(new UserProfileData(subscribeUserId, new UserDetails("user2", "https://example.com", "https://example.com/profile2.jpg", new HiddenUserDetails("user2@example.com"))));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.SubscribeAsync(userId, subscribeUserId));
            Assert.Equal("User does not exist.", exception.Message); // Ensure correct exception message
        }

        [Fact]
        public async Task SubscribeAsync_ShouldThrowException_WhenSubscribeUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid(); // User who wants to subscribe
            var subscribeUserId = Guid.NewGuid(); // User to be subscribed to

            // Mock the case where the subscribe user doesn't exist
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(new UserProfileData(userId, new UserDetails("user1", "https://example.com", "https://example.com/profile.jpg", new HiddenUserDetails("user1@example.com"))));
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync((UserProfileData?)null); // subscribeUser doesn't exist

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.SubscribeAsync(userId, subscribeUserId));
            Assert.Equal("User you want to subscribe to does not exist.", exception.Message); // Ensure correct exception message
        }

        #endregion

        #region Unsubscribe

        [Fact]
        public async Task UnsubscribeAsync_ShouldUnsubscribeUserFromAnotherUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var subscribeUserId = Guid.NewGuid();

            var user = new UserProfileData(userId, new UserDetails("user1", "url", "profileUrl", new HiddenUserDetails("email")));
            var subscribeUser = new UserProfileData(subscribeUserId, new UserDetails("user2", "url", "profileUrl", new HiddenUserDetails("email")));

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync(subscribeUser);
            _userRepositoryMock.Setup(repo => repo.RemoveUserFromSubscriptionAsync(userId, subscribeUserId)).Returns(Task.CompletedTask);

            // Act
            await _userService.UnsubscribeAsync(userId, subscribeUserId);

            // Assert
            _userRepositoryMock.Verify(repo => repo.RemoveUserFromSubscriptionAsync(userId, subscribeUserId), Times.Once);
        }


        [Fact]
        public async Task UnsubscribeAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var subscribeUserId = Guid.NewGuid();

            var subscribeUser = new UserProfileData(subscribeUserId, new UserDetails("user2", "url", "profileUrl", new HiddenUserDetails("email")));

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync((UserProfileData)null);  // User not found
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync(subscribeUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.UnsubscribeAsync(userId, subscribeUserId));
            Assert.Equal("User does not exist.", exception.Message);
        }

        [Fact]
        public async Task UnsubscribeAsync_ShouldThrowException_WhenSubscribeUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var subscribeUserId = Guid.NewGuid();

            var user = new UserProfileData(userId, new UserDetails("user1", "url", "profileUrl", new HiddenUserDetails("email")));

            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(subscribeUserId)).ReturnsAsync((UserProfileData)null);  // Subscribe user not found

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userService.UnsubscribeAsync(userId, subscribeUserId));
            Assert.Equal("User to unsubscribe from does not exist.", exception.Message);
        }


        #endregion

    }
}