using Moq;
using Xunit;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Auth;

namespace reeltok.api.gateway.Tests
{
    public class AuthServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5003/auth";
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _mockGatewayService = new Mock<IGatewayService>();
            _authService = new AuthService(_mockGatewayService.Object);
        }

        [Fact]
        public async Task LogOutUser_WithRevokedAccess_ReturnAlreadyLoggedOutMessage()
        {
            // Arrange
            bool success = false;
            FailureResponseDto failureResponseDto = new FailureResponseDto(success, "Already logged out");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(
                It.IsAny<LogOutUserRequestDto>(), $"{BaseTestUrl}/logout", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.LogOutUser());
            Assert.Equal("Logout failed", exception.Message);
        }

        [Fact]
        public async Task LogOutUser_WithInvalidUser_ThrowsInvalidOperationException()
        {
            // Arrange
            bool success = false;
            FailureResponseDto failureResponseDto = new FailureResponseDto(success, "Invalid user");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(
                It.IsAny<LogOutUserRequestDto>(), $"{BaseTestUrl}/logout", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.LogOutUser());
            Assert.Equal("Logout failed", exception.Message);
        }

        [Fact]
        public async Task LogOutUser_WithLoggedInUser_ReturnsSuccess()
        {
            // Arrange
            bool success = true;
            LogOutUserResponseDto successResponseDto = new LogOutUserResponseDto(success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(
                It.IsAny<LogOutUserRequestDto>(), $"{BaseTestUrl}/logout", HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            bool result = await _authService.LogOutUser();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUserIdByToken_WithInvalidUser_ThrowsInvalidOperationException()
        {
            // Arrange
            bool success = false;
            FailureResponseDto failureResponseDto = new FailureResponseDto(success, "Invalid token");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<GetUserIdByTokenRequestDto, GetUserIdByTokenResponseDto>(
                It.IsAny<GetUserIdByTokenRequestDto>(), $"{BaseTestUrl}/getUserIdByToken", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.GetUserIdByToken());
            Assert.Equal("Invalid token", exception.Message);
        }

        [Fact]
        public async Task GetUserIdByToken_WithValidToken_ReturnsUserId()
        {
            // Arrange
            bool success = true;
            Guid validUserId = Guid.NewGuid();
            GetUserIdByTokenResponseDto successResponseDto = new GetUserIdByTokenResponseDto(success, validUserId);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<GetUserIdByTokenRequestDto, GetUserIdByTokenResponseDto>(
                It.IsAny<GetUserIdByTokenRequestDto>(), $"{BaseTestUrl}/getUserIdByToken", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            Guid result = await _authService.GetUserIdByToken();

            // Assert
            Assert.Equal(validUserId, result);
        }
    }
}