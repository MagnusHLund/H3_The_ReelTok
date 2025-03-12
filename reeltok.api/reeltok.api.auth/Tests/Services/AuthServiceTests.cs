using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Interfaces.Repositories;
using reeltok.api.auth.Services;
using System.Security.Authentication;
using reeltok.api.auth.Utils;

namespace reeltok.api.tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<ITokenGenerationService> _tokenGenerationServiceMock;
        private readonly Mock<ITokenManagementService> _tokenManagementServiceMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _tokenGenerationServiceMock = new Mock<ITokenGenerationService>();
            _tokenManagementServiceMock = new Mock<ITokenManagementService>();

            _authenticationService = new AuthenticationService(
                _authRepositoryMock.Object,
                _tokenGenerationServiceMock.Object,
                _tokenManagementServiceMock.Object
            );
        }

        [Fact]
        public async Task LoginUserAsync_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var credentials = new Credentials(userId, "plainPassword");

            var hashedPasswordDetails = new HashedPasswordDetails("hashedPassword", "salt");
            var userCredentials = new UserCredentialsEntity(userId, hashedPasswordDetails);

            var accessToken = new AccessToken("accessTokenValue", 1234567890, 1234567990);
            var refreshToken = new RefreshToken("refreshTokenValue", 1234567890, 1234569990);

            _authRepositoryMock
                .Setup(repo => repo.GetUserCredentialsByUserId(userId))
                .ReturnsAsync(userCredentials);
            _tokenGenerationServiceMock
                .Setup(service => service.GenerateAccessToken(userId))
                .ReturnsAsync(accessToken);
            _tokenGenerationServiceMock
                .Setup(service => service.GenerateRefreshToken(userId))
                .ReturnsAsync(refreshToken);

            // Act
            var tokens = await _authenticationService.LoginUserAsync(credentials);

            // Assert
            Assert.Equal(accessToken, tokens.AccessToken);
            Assert.Equal(refreshToken, tokens.RefreshToken);
        }

        [Fact]
        public async Task LoginUserAsync_InvalidCredentials_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var credentials = new Credentials(userId, "wrongPassword");

            // The password hash and salt that will NOT match the credentials
            var hashedPasswordDetails = new HashedPasswordDetails("correctHashedPassword", "randomSalt");
            var userCredentials = new UserCredentialsEntity(userId, hashedPasswordDetails);

            _authRepositoryMock
                .Setup(repo => repo.GetUserCredentialsByUserId(userId))
                .ReturnsAsync(userCredentials);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidCredentialException>(
                () => _authenticationService.LoginUserAsync(credentials)
            );
        }


        [Fact]
        public async Task LogoutUserAsync_Success()
        {
            // Arrange
            var accessTokenValue = "accessTokenValue";
            var refreshTokenValue = "refreshTokenValue";

            _tokenManagementServiceMock
                .Setup(service => service.RevokeTokens(accessTokenValue, refreshTokenValue))
                .Returns(Task.CompletedTask);

            // Act
            await _authenticationService.LogoutUserAsync(accessTokenValue, refreshTokenValue);

            // Assert
            _tokenManagementServiceMock.Verify(
                service => service.RevokeTokens(accessTokenValue, refreshTokenValue),
                Times.Once
            );
        }

        [Fact]
        public async Task LogoutUserAsync_Failure_DoesNotThrowException()
        {
            // Arrange
            var accessTokenValue = "accessTokenValue";
            var refreshTokenValue = "refreshTokenValue";

            _tokenManagementServiceMock
                .Setup(service => service.RevokeTokens(accessTokenValue, refreshTokenValue))
                .Throws<InvalidOperationException>();

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _authenticationService.LogoutUserAsync(accessTokenValue, refreshTokenValue)
            );
        }
    }
}
