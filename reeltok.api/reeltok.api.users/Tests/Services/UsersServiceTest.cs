using Moq;
using Xunit;
using reeltok.api.users.Entities;
using reeltok.api.users.Services;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.utils;
using reeltok.api.users.factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IUsersRepository> _mockUserRepository;
        private readonly Mock<IExternalApiService> _mockExternalApiService;
        private readonly Mock<IStorageService> _mockStorageService;
        private readonly Mock<ISubscriptionsRepository> _mockSubscriptionsRepository;
        private readonly UsersService _usersService;

        public UsersServiceTests()
        {
            _mockUserRepository = new Mock<IUsersRepository>();
            _mockExternalApiService = new Mock<IExternalApiService>();
            _mockStorageService = new Mock<IStorageService>();
            _mockSubscriptionsRepository = new Mock<ISubscriptionsRepository>();
            _usersService = new UsersService(
                _mockUserRepository.Object,
                _mockExternalApiService.Object,
                _mockStorageService.Object,
                _mockSubscriptionsRepository.Object
            );
        }

        #region CreateUserAsync

        [Fact]
        public async Task CreateUserAsync_WithValidDetails_ReturnsCreatedUser()
        {
            // Arrange
            string username = "testUser";
            string email = "test@example.com";
            string password = "password123";
            byte interests = 5;
            UserEntity newUser = new UserEntity(Guid.NewGuid(), new UserDetails(username, "bio", "ProfilePictureUrl"), new HiddenUserDetails(email));

            _mockUserRepository.Setup(x => x.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(newUser);
            _mockExternalApiService.Setup(x => x.CreateUserInAuthApiAsync(It.IsAny<Guid>(), password)).Returns(Task.CompletedTask);
            _mockExternalApiService.Setup(x => x.CreateUserInRecommendationsApiAsync(It.IsAny<Guid>(), interests)).Returns(Task.CompletedTask);

            // Act
            UserEntity result = await _usersService.CreateUserAsync(username, email, password, interests);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.UserDetails.Username);
            Assert.Equal(email, result.HiddenUserDetails.Email);
        }

        [Fact]
        public async Task CreateUserAsync_WithInvalidEmail_ThrowsArgumentException()
        {
            // Arrange
            string username = "testUser";
            string email = "invalidEmail";
            string password = "password123";
            byte interests = 5;

            // Act & Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => _usersService.CreateUserAsync(username, email, password, interests));
            Assert.Contains("invalid username or email", exception.Message, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region UpdateUserProfilePictureAsync

        [Fact]
        public async Task UpdateUserProfilePictureAsync_WithValidImage_ReturnsUpdatedUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string fileName = "testImage.png";
            string contentType = "image/png";
            byte[] fileContent = new byte[] { 0x89, 0x50, 0x4E, 0x47 }; // PNG file signature

            Mock<IFormFile> mockFile = new Mock<IFormFile>();
            MemoryStream ms = new MemoryStream(fileContent);

            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.Length).Returns(ms.Length);
            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.ContentType).Returns(contentType);

            UserEntity user = new UserEntity(userId, new UserDetails("testUser", "bio", "oldProfileUrl"), new HiddenUserDetails("test@example.com"));
            string newProfilePictureUrl = "newProfilePictureUrl";

            _mockStorageService.Setup(x => x.UploadProfilePictureToFileServerAsync(mockFile.Object, userId)).ReturnsAsync(newProfilePictureUrl);
            _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockUserRepository
                .Setup(x => x.UpdateUserAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync((UserEntity u) => u); // Return the updated user object


            // Act
            UserEntity updatedUser = await _usersService.UpdateUserProfilePictureAsync(mockFile.Object, userId);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(newProfilePictureUrl, updatedUser.UserDetails.ProfilePictureUrlPath);
        }

        #endregion

        #region UpdateUserAsync

        [Fact]
        public async Task UpdateUserAsync_WithValidUsernameAndEmail_ReturnsUpdatedUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string newUsername = "newUsername";
            string newEmail = "newemail@example.com";
            UserEntity user = new UserEntity(userId, new UserDetails("oldUsername", "bio", "oldProfileUrl"), new HiddenUserDetails("oldemail@example.com"));

            _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockUserRepository.Setup(x => x.UpdateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync((UserEntity u) => u);

            // Act
            UserEntity updatedUser = await _usersService.UpdateUserAsync(userId, newUsername, newEmail);

            // Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(newUsername, updatedUser.UserDetails.Username);
            Assert.Equal(newEmail, updatedUser.HiddenUserDetails.Email);
        }

        [Fact]
        public async Task UpdateUserAsync_WithInvalidUsername_ThrowsInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string invalidUsername = "inv@lidUser"; // Invalid username
            string newEmail = "newemail@example.com";
            UserEntity user = new UserEntity(userId, new UserDetails("oldUsername", "bio", "oldProfileUrl"), new HiddenUserDetails("oldemail@example.com"));

            // Mock repository to return the user
            _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId)).ThrowsAsync(new ArgumentException("Invalid username"));

            // Act & Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => _usersService.UpdateUserAsync(userId, invalidUsername, newEmail));
            Assert.Contains("Invalid username", exception.Message);  // Ensure the exception contains the expected message
        }

        #endregion

        #region GetUserByIdAsync

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUserWithSubscriptionCounts()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            UserEntity user = new UserEntity(userId, new UserDetails("testUser", "bio", "profilePic"), new HiddenUserDetails("test@example.com"));
            int subscriberCount = 10;
            int subscriptionCount = 5;

            _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _mockSubscriptionsRepository.Setup(x => x.GetSubscribersCountAsync(userId)).ReturnsAsync(subscriberCount);
            _mockSubscriptionsRepository.Setup(x => x.GetSubscriptionsCountAsync(userId)).ReturnsAsync(subscriptionCount);

            // Act
            UserWithSubscriptionCounts result = await _usersService.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(subscriberCount, result.TotalSubscribers);
            Assert.Equal(subscriptionCount, result.TotalSubscriptions);
        }

        [Fact]
        public async Task GetUserByIdAsync_WithInvalidUserId_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _mockUserRepository.Setup(x => x.GetUserByIdAsync(userId)).ThrowsAsync(new Exception("User not found"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _usersService.GetUserByIdAsync(userId));
            Assert.Contains("User not found", exception.Message);
        }

        #endregion

        #region GetUsersByIdsAsync

        [Fact]
        public async Task GetUsersByIdsAsync_ReturnsUsers()
        {
            // Arrange
            List<Guid> userIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
            List<UserEntity> users = new List<UserEntity>
            {
                new UserEntity(userIds[0], new UserDetails("testUser1", "bio", "profilePic1"), new HiddenUserDetails("test1@example.com")),
                new UserEntity(userIds[1], new UserDetails("testUser2", "bio", "profilePic2"), new HiddenUserDetails("test2@example.com"))
            };

            _mockUserRepository.Setup(x => x.GetUsersByUserIdsAsync(userIds)).ReturnsAsync(users);

            // Act
            List<UserEntity> result = await _usersService.GetUsersByIdsAsync(userIds);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userIds.Count, result.Count);
        }

        [Fact]
        public async Task GetUsersByIdsAsync_WithEmptyList_ThrowsException()
        {
            // Arrange
            List<Guid> userIds = new List<Guid>();

            _mockUserRepository.Setup(x => x.GetUsersByUserIdsAsync(userIds))
                .ThrowsAsync(new ArgumentException("User ids list cannot be empty"));


            // Act & Assert
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => _usersService.GetUsersByIdsAsync(userIds));
            Assert.Contains("User ids list cannot be empty", exception.Message);
        }

        #endregion

        #region GetUserByEmail

        [Fact]
        public async Task GetUserByEmail_ReturnsUser()
        {
            // Arrange
            string email = "test@example.com";
            UserEntity user = new UserEntity(Guid.NewGuid(), new UserDetails("testUser", "bio", "profilePic"), new HiddenUserDetails(email));

            _mockUserRepository.Setup(x => x.GetUserByEmailAsync(email)).ReturnsAsync(user);

            // Act
            UserEntity result = await _usersService.GetUserByEmail(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.HiddenUserDetails.Email);
        }

        [Fact]
        public async Task GetUserByEmail_WithInvalidEmail_ThrowsException()
        {
            // Arrange
            string email = "invalid@example.com";

            _mockUserRepository.Setup(x => x.GetUserByEmailAsync(email)).ThrowsAsync(new Exception("User not found"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _usersService.GetUserByEmail(email));
            Assert.Contains("User not found", exception.Message);
        }

        #endregion
    }
}
