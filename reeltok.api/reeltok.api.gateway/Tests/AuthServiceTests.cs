using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Tests
{
    public class AuthServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5003/auth";
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _authService = new AuthService(_mockHttpService.Object);
        }

        [Fact]
        public async Task LogOutUser_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            FailureResponseDto failureResponseDto = new FailureResponseDto("Already logged out");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(
                It.IsAny<ServiceLogOutUserRequestDto>(), $"{BaseTestUrl}/logout", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.LogOutUser());
            Assert.Equal("Already logged out", exception.Message);
        }

        [Fact]
        public async Task LogOutUser_WithLoggedInUser_ReturnsSuccess()
        {
            // Arrange
            bool success = true;
            ServiceLogOutUserResponseDto successResponseDto = new ServiceLogOutUserResponseDto(success);

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(
                It.IsAny<ServiceLogOutUserRequestDto>(), $"{BaseTestUrl}/logout", HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            bool result = await _authService.LogOutUser();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetUserIdByToken_WithBadResponse_ThrowsInvalidOperationException()
        {
            // Arrange
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid authentication token");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(
                It.IsAny<ServiceGetUserIdByTokenRequestDto>(), $"{BaseTestUrl}/getUserIdByToken", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.GetUserIdByToken());
            Assert.Equal("Invalid authentication token", exception.Message);
        }

        [Fact]
        public async Task GetUserIdByToken_WithValidToken_ReturnsUserId()
        {
            // Arrange
            bool success = true;
            Guid validUserId = Guid.NewGuid();
            ServiceGetUserIdByTokenResponseDto successResponseDto = new ServiceGetUserIdByTokenResponseDto(validUserId, success);

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(
                It.IsAny<ServiceGetUserIdByTokenRequestDto>(), $"{BaseTestUrl}/getUserIdByToken", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            Guid result = await _authService.GetUserIdByToken();

            // Assert
            Assert.Equal(validUserId, result);
        }
    }
}
