using Moq;
using Xunit;
using reeltok.api.users.Entities;
using reeltok.api.users.Services;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Tests.Services
{
    public class LoginServiceTests
    {
        private readonly Mock<IUsersService> _mockUsersService;
        private readonly Mock<IExternalApiService> _mockExternalApiService;
        private readonly LoginService _loginService;

        public LoginServiceTests()
        {
            _mockUsersService = new Mock<IUsersService>();
            _mockExternalApiService = new Mock<IExternalApiService>();
            _loginService = new LoginService(_mockUsersService.Object, _mockExternalApiService.Object);
        }

        #region Success Tests

        [Fact]
        public async Task LoginUserAsync_WithValidCredentials_ReturnsUserWithInterest()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            Guid userId = Guid.NewGuid();
            byte userInterest = 5;
            UserDetails userDetails = new UserDetails("Manike", "httpsSomethingSomething", "ProfilePictureUrlPath");
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails("test@mail.com");
            UserEntity userEntity = new UserEntity(userId, userDetails, hiddenUserDetails);

            _mockUsersService.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(userEntity);
            _mockExternalApiService.Setup(x => x.LoginUserInAuthApiAsync(userId, password)).Returns(Task.CompletedTask);
            _mockExternalApiService.Setup(x => x.GetUserInterestFromRecommendationsApiAsync(userId)).ReturnsAsync(userInterest);

            // Act
            UserWithInterestEntity result = await _loginService.LoginUserAsync(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(userInterest, result.Interest);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task LoginUserAsync_WithInvalidEmail_ThrowsException()
        {
            // Arrange
            string email = "nonexistent@example.com";
            string password = "password";

            _mockUsersService.Setup(x => x.GetUserByEmail(email)).ThrowsAsync(new Exception("User not found"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _loginService.LoginUserAsync(email, password));
            Assert.Contains("User not found", exception.Message);
        }

        [Fact]
        public async Task LoginUserAsync_WithInvalidPassword_ThrowsException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "wrongPassword";
            Guid userId = Guid.NewGuid();
            UserDetails userDetails = new UserDetails("Manike", "httpsSomethingSomething", "ProfilePictureUrlPath");
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails("test@mail.com");
            UserEntity userEntity = new UserEntity(userId, userDetails, hiddenUserDetails);

            _mockUsersService.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(userEntity);
            _mockExternalApiService.Setup(x => x.LoginUserInAuthApiAsync(userId, password))
                .ThrowsAsync(new Exception("Invalid password"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _loginService.LoginUserAsync(email, password));
            Assert.Contains("Invalid password", exception.Message);
        }

        [Fact]
        public async Task LoginUserAsync_WithFailedInterestRetrieval_ThrowsException()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            Guid userId = Guid.NewGuid();
            UserDetails userDetails = new UserDetails("Manike", "httpsSomethingSomething", "ProfilePictureUrlPath");
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails("test@mail.com");
            UserEntity userEntity = new UserEntity(userId, userDetails, hiddenUserDetails);

            _mockUsersService.Setup(x => x.GetUserByEmail(email)).ReturnsAsync(userEntity);
            _mockExternalApiService.Setup(x => x.LoginUserInAuthApiAsync(userId, password)).Returns(Task.CompletedTask);
            _mockExternalApiService.Setup(x => x.GetUserInterestFromRecommendationsApiAsync(userId)).ThrowsAsync(new Exception("Failed to retrieve interest"));

            // Act & Assert
            Exception exception = await Assert.ThrowsAsync<Exception>(() => _loginService.LoginUserAsync(email, password));
            Assert.Contains("Failed to retrieve interest", exception.Message);
        }

        #endregion
    }
}
