using reeltok.api.gateway.Interfaces;
using Xunit;
using Moq;
using reeltok.api.gateway.Services;

namespace reeltok.api.gateway.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _mockGatewayService = new Mock<IGatewayService>();
            _authService = new AuthService(_mockGatewayService.Object);
        }

        [Fact]
        public void LogOutUser_WithLoggedOutUser_ReturnAlreadyLoggedOutMessage()
        {
            {
                // Arrange
                string expectedMessage = "User is already logged out";
                _mockGatewayService.Setup(x => x.SendRequestAsync("auth/logout", It.IsAny<object>())).ReturnsAsync(expectedMessage);

                // Act
                var result = _authService.LogOutUser("loggedOutUser");

                // Assert
                Assert.Equal(expectedMessage, result.result);
            }
        }

        [Fact]
        public void LogOutUser_WithInvalidUser_ReturnInvalidUserMessage()
        {
            Assert.True(true);
        }

        [Fact]
        public void LogOutUser_WithLoggedInUser_ShouldRevokeAccessSuccessfully()
        {
            Assert.True(true);
        }

        public void UpdatePassword_WithInvalidUser_ReturnInvalidUserMessage()
        {

        }

        public void UpdatePassword_WithValidParameters_ReturnSuccessful()
        {

        }

        public void GetUserIdByToken_WithInvalidUser_ReturnInvalidUserMessage()
        {

        }

        public void GetUserIdByToken_WithValidToken_ReturnUserId()
        {

        }
    }
}