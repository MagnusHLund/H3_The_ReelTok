using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Repositories;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Services;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.Tests.Factories;

namespace reeltok.api.auth.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<ITokenGenerationService> _tokenGenerationServiceMock;
        private readonly Mock<ITokenValidationService> _tokenValidationServiceMock;
        private readonly UsersService _usersService;

        public UsersServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _tokenGenerationServiceMock = new Mock<ITokenGenerationService>();
            _tokenValidationServiceMock = new Mock<ITokenValidationService>();
            _usersService = new UsersService(
                _authRepositoryMock.Object,
                _tokenGenerationServiceMock.Object,
                _tokenValidationServiceMock.Object
            );
        }

        [Fact]
        public void GetUserIdByAccessTokenAsync_Success()
        {
            // Arrange
            string accessTokenValue = "validAccessToken";
            Guid expectedUserId = TestDataFactory.GenerateGuid();
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserId.ToString())
            }));

            _tokenValidationServiceMock
                .Setup(service => service.DecodeAccessToken(accessTokenValue))
                .Returns(claimsPrincipal);

            // Act
            Guid actualUserId = _usersService.GetUserIdByAccessTokenAsync(accessTokenValue);

            // Assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public void GetUserIdByAccessTokenAsync_Failure()
        {
            // Arrange
            string accessTokenValue = "invalidAccessToken";

            _tokenValidationServiceMock
                .Setup(service => service.DecodeAccessToken(accessTokenValue))
                .Returns((ClaimsPrincipal) null);

            // Act & Assert
            Assert.Throws<FormatException>(() => _usersService.GetUserIdByAccessTokenAsync(accessTokenValue));
        }

        [Fact]
        public async Task SignUpAsync_Success()
        {
            // Arrange
            Guid userId = TestDataFactory.GenerateGuid();
            string plainTextPassword = "ValidPassword123!";
            Credentials userCredentials = new Credentials(userId, plainTextPassword);
            UserCredentialsEntity userCredentialsEntity = TestDataFactory
                .GenerateUserCredentialsEntity(userId, "hashedPassword", "salt");
            AccessToken accessToken = new AccessToken("accessTokenValue", 1234567890, 1234567990);
            RefreshToken refreshToken = new RefreshToken("refreshTokenValue", 1234567890, 1234569990);

            _authRepositoryMock
                .Setup(repo => repo.DoesUserExist(userId))
                .ReturnsAsync(false);
            _authRepositoryMock
                .Setup(repo => repo.CreateUser(It.IsAny<UserCredentialsEntity>()))
                .ReturnsAsync(userCredentialsEntity);
            _tokenGenerationServiceMock
                .Setup(service => service.GenerateAccessToken(userId))
                .ReturnsAsync(accessToken);
            _tokenGenerationServiceMock
                .Setup(service => service.GenerateRefreshToken(userId))
                .ReturnsAsync(refreshToken);

            // Act
            Tokens tokens = await _usersService.SignUpAsync(userCredentials);

            // Assert
            Assert.Equal(accessToken, tokens.AccessToken);
            Assert.Equal(refreshToken, tokens.RefreshToken);
        }

        [Fact]
        public async Task SignUpAsync_Failure_UserExists()
        {
            // Arrange
            Guid userId = TestDataFactory.GenerateGuid();
            string plainTextPassword = "ValidPassword123!";
            Credentials userCredentials = new Credentials(userId, plainTextPassword);

            _authRepositoryMock
                .Setup(repo => repo.DoesUserExist(userId))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.SignUpAsync(userCredentials));
        }

        [Fact]
        public async Task DeleteUser_Success()
        {
            // Arrange
            Guid userId = TestDataFactory.GenerateGuid();

            _authRepositoryMock
                .Setup(repo => repo.DeleteUser(userId))
                .Returns(Task.CompletedTask);

            // Act
            await _usersService.DeleteUser(userId);

            // Assert
            _authRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_Failure()
        {
            // Arrange
            Guid userId = TestDataFactory.GenerateGuid();

            _authRepositoryMock
                .Setup(repo => repo.DeleteUser(userId))
                .ThrowsAsync(new Exception("Deletion failed"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _usersService.DeleteUser(userId));
        }
    }
}
