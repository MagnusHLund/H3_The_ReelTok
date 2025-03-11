using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Tests.Services
{
    public class AuthServiceTests
    {/*
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
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Already logged out");
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(
                It.IsAny<ServiceLogOutUserRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.LogOutUser());
            Assert.Equal("Already logged out", exception.Message);
        }

        [Fact]
        public async Task LogOutUser_WithLoggedInUser_ReturnsSuccess()
        {
            // Arrange
            ServiceLogOutUserResponseDto successResponseDto = TestDataFactory.CreateLogOutUserResponse();
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(
                It.IsAny<ServiceLogOutUserRequestDto>(), targetUrl, HttpMethod.Post))
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
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid authentication token");
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("getUserIdByToken");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(
                It.IsAny<ServiceGetUserIdByTokenRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.GetUserIdByToken());
            Assert.Equal("Invalid authentication token", exception.Message);
        }

        [Fact]
        public async Task GetUserIdByToken_WithValidToken_ReturnsUserId()
        {
            // Arrange
            ServiceGetUserIdByTokenResponseDto successResponseDto = TestDataFactory.CreateGetUserIdByTokenResponse();
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("getUserIdByToken");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(
                It.IsAny<ServiceGetUserIdByTokenRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            Guid result = await _authService.GetUserIdByToken();

            // Assert
            Assert.Equal(successResponseDto.UserId, result);
        } */
    }
}
