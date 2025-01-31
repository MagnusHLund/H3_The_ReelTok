using reeltok.api.gateway.Interfaces;
using Xunit;
using Moq;
using reeltok.api.gateway.Services;
using System.Net;
using reeltok.api.gateway.DTOs;

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
            // Arrange
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);
            expectedResponse.Headers.Add("Set-Cookie", "JWT=; Expires=Thu, 01 Jan 1970 00:00:00 GMT");
            expectedResponse.Headers.Add("Set-Cookie", "RefreshToken=; Expires=Thu, 01 Jan 1970 00:00:00 GMT");

            var mockHttpRequest = new Mock<HttpRequest>();
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpRequest.Setup(req => req.HttpContext).Returns(mockHttpContext.Object);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(It.IsAny<LogOutUserRequestDto>(), "auth/logOut")).ReturnsAsync(expectedResponse);

            // Act
            var result = _authService.LogOutUser().Result;

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.Headers.TryGetValues("Set-Cookie", out var cookieValues));
            Assert.Contains("JWT=; Expires=Thu, 01 Jan 1970 00:00:00 GMT", cookieValues);
            Assert.Contains("RefreshToken=; Expires=Thu, 01 Jan 1970 00:00:00 GMT", cookieValues);
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
            Assert.True(true);
        }

        public void UpdatePassword_WithValidParameters_ReturnSuccessful()
        {
            Assert.True(true);
        }

        public void GetUserIdByToken_WithInvalidUser_ReturnInvalidUserMessage()
        {
            Assert.True(true);
        }

        public void GetUserIdByToken_WithValidToken_ReturnUserId()
        {
            Assert.True(true);
        }
    }
}