using Moq;
using Xunit;
using reeltok.api.auth.Utils;
using System.Security.Claims;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Services;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using System.Security.Authentication;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly Mock<ITokensService> _mockTokensService;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
          _mockTokensService = new Mock<ITokensService>();
          _mockAuthRepository = new Mock<IAuthRepository>();
          _authService = new AuthService(_mockAuthRepository.Object, _mockTokensService.Object);
        }

        [Fact]
        public async Task CreateUser_WithExistingUser_ThrowInvalidOperationException()
        {
            // Arrange
            Guid existingUserId = Guid.NewGuid();
            bool userExists = true;

            _mockAuthRepository
              .Setup(repo => repo.DoesUserExist(existingUserId))
              .ReturnsAsync(userExists);

            CreateDetails CreateDetails = new CreateDetails(existingUserId, "password123");

            string expectedExceptionMessage = "User already exists!";

            // Act & Assert
            InvalidOperationException exception =  await Assert.ThrowsAsync<InvalidOperationException>(() => _authService.CreateUser(CreateDetails));
            Assert.Equal(expectedExceptionMessage, exception.Message);

            _mockAuthRepository.Verify(repo => repo.DoesUserExist(existingUserId), Times.Once);
            _mockAuthRepository.Verify(repo => repo.CreateUser(It.IsAny<UserCredentialsEntity>()), Times.Never);
        }

        [Fact]
        public async Task CreateUser_WithWeakPassword_ThrowValidationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            CreateDetails CreateDetails = new CreateDetails(userId, "weakpassword");

            string expectedExceptionMessage = "Password does not follow the minimum requirements!";

            // Act & Assert
            ValidationException exception = await Assert.ThrowsAsync<ValidationException>(() => _authService.CreateUser(CreateDetails));
            Assert.Equal(expectedExceptionMessage, exception.Message);

            _mockAuthRepository.Verify(repo => repo.DoesUserExist(userId), Times.Once);
            _mockAuthRepository.Verify(repo => repo.CreateUser(It.IsAny<UserCredentialsEntity>()), Times.Never);
        }

        [Fact]
        public async Task CreateUser_WithValidUser_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            CreateDetails createDetails = new CreateDetails(userId, "VeryStroongPassword566");

            UserCredentialsEntity userCredentials = new UserCredentialsEntity(userId, "hashed password", "salt");

            _mockAuthRepository
              .Setup(repo => repo.CreateUser(It.IsAny<UserCredentialsEntity>()))
              .ReturnsAsync(userCredentials);

            // Act
            Tokens tokens = await _authService.CreateUser(createDetails);

            // Assert
            Assert.NotNull(tokens);
            Assert.NotNull(tokens.AccessToken);
            Assert.NotEmpty(tokens.AccessToken.Token);
            Assert.NotNull(tokens.RefreshToken);
            Assert.NotEmpty(tokens.RefreshToken.Token);
        }

        [Fact]
        public async Task LoginUser_WithIncorrectPassword_ThrowInvalidCredentialException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword("DifferentPassword555");
            LoginCredentials loginCredentials = new LoginCredentials(userId, "password");
            UserCredentialsEntity userCredentials = new UserCredentialsEntity(loginCredentials.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);

            string expectedExceptionMessage = "Invalid credentials!";

            _mockAuthRepository
              .Setup(repo => repo.GetUserAuthenticationByUserId(loginCredentials.UserId))
              .ReturnsAsync(userCredentials);

            // Act & Assert
            InvalidCredentialException exception = await Assert.ThrowsAsync<InvalidCredentialException>(() => _authService.LoginUser(loginCredentials));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async Task LoginUser_WithValidCredentials_ReturnTokens()
        {
          // Arrange
          Guid userId = Guid.NewGuid();
          LoginCredentials loginCredentials = new LoginCredentials(userId, "VeryStroongPassword566");
          HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword(loginCredentials.PlainTextPassword);
          UserCredentialsEntity userAuth = new UserCredentialsEntity(loginCredentials.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);

          _mockAuthRepository
              .Setup(repo => repo.GetUserAuthenticationByUserId(loginCredentials.UserId))
              .ReturnsAsync(userAuth);

          // Act
          Tokens tokens = await _authService.LoginUser(loginCredentials);

          // Assert
          Assert.NotNull(tokens);
          Assert.NotNull(tokens.AccessToken);
          Assert.NotEmpty(tokens.AccessToken.Token);
          Assert.NotNull(tokens.RefreshToken);
          Assert.NotEmpty(tokens.RefreshToken.Token);
        }

        [Fact]
        public async Task RefreshAccessToken_WithInvalidRefreshToken_ThrowInvalidTokenException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task RefreshAccessToken_WithExpiredToken_ThrowUnauthorizedException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task RefreshAccessToken_WithValidToken_ReturnNewTokens()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task DeleteUser_WithInvalidUserId_ThrowKeyNotFoundException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string expectedExceptionMessage = $"Unable to find user: {userId} in the database!";

            _mockAuthRepository
                .Setup(repo => repo.DeleteUser(userId))
                .ThrowsAsync(new KeyNotFoundException(expectedExceptionMessage));

            // Act & Assert
            KeyNotFoundException exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _authService.DeleteUser(userId));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }

        [Fact]
        public async Task DeleteUser_WithValidUser_RemoveUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _mockAuthRepository
                .Setup(repo => repo.DeleteUser(userId));

            // Act
            Exception exception = await Record.ExceptionAsync(() => _authService.DeleteUser(userId));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void GetUserIdByToken_WithValidAccessToken_ReturnsUserId()
        {
            // Arrange
            string accessTokenValue = "Token";
            Guid expectedUserId = Guid.NewGuid();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserId.ToString()) //TODO: Modify this
            }));

            _mockTokensService
                .Setup(service => service.DecodeAccessToken(accessTokenValue))
                .Returns(claimsPrincipal);

            // Act
            Guid userId = _authService.GetUserIdByToken(accessTokenValue);

            // Assert
            Assert.Equal(expectedUserId, userId);
        }

        [Fact]
        public async Task LogoutUser_WithInvalidUserId_ThrowNotFoundException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task LogoutUser_WithValidUser_InvalidateTokens()
        {
            throw new NotImplementedException();
        }
    }
}
