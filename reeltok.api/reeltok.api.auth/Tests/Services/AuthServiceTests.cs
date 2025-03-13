using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Moq;
using Xunit;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Repositories;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Services;
using reeltok.api.auth.Tests.Factories;

namespace reeltok.api.auth.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<ITokenGenerationService> _tokenGenerationServiceMock;
        private readonly Mock<ITokenManagementService> _tokenManagementServiceMock;
        private readonly AuthenticationService _authService;

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _tokenGenerationServiceMock = new Mock<ITokenGenerationService>();
            _tokenManagementServiceMock = new Mock<ITokenManagementService>();
            _authService = new AuthenticationService(
                _authRepositoryMock.Object,
                _tokenGenerationServiceMock.Object,
                _tokenManagementServiceMock.Object
            );
        }

        [Fact]
        public async Task LoginUserAsync_Failure_InvalidCredentials()
        {
            // Arrange
            Guid userId = TestDataFactory.GenerateGuid();
            string plainTextPassword = "InvalidPassword123!";
            Credentials loginCredentials = new Credentials(userId, plainTextPassword);
            UserCredentialsEntity userCredentialsEntity = TestDataFactory.GenerateUserCredentialsEntity(userId, "hashedPassword", "salt");

            _authRepositoryMock
                .Setup(repo => repo.GetUserCredentialsByUserId(userId))
                .ReturnsAsync(userCredentialsEntity);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(() => _authService.LoginUserAsync(loginCredentials));
        }

        [Fact]
        public async Task LogoutUserAsync_Success()
        {
            // Arrange
            string accessTokenValue = "validAccessToken";
            string refreshTokenValue = "validRefreshToken";

            _tokenManagementServiceMock
                .Setup(service => service.RevokeTokens(accessTokenValue, refreshTokenValue))
                .Returns(Task.CompletedTask);

            // Act
            await _authService.LogoutUserAsync(accessTokenValue, refreshTokenValue);

            // Assert
            _tokenManagementServiceMock.Verify(service => service.RevokeTokens(accessTokenValue, refreshTokenValue), Times.Once);
        }

        [Fact]
        public async Task LogoutUserAsync_Failure()
        {
            // Arrange
            string accessTokenValue = "invalidAccessToken";
            string refreshTokenValue = "invalidRefreshToken";

            _tokenManagementServiceMock
                .Setup(service => service.RevokeTokens(accessTokenValue, refreshTokenValue))
                .ThrowsAsync(new Exception("Token revocation failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _authService.LogoutUserAsync(accessTokenValue, refreshTokenValue));
        }
    }
}
