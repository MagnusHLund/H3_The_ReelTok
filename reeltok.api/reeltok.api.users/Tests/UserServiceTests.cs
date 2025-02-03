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
    }
}