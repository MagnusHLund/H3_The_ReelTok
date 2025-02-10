using Moq;
using Xunit;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using reeltok.api.auth.Exceptions;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Services;

namespace reeltok.api.auth.Tests
{
  public class AuthServiceTests
  {
    private const string BaseTestUrl = "http://localhost:5003/auth";
    private readonly Mock<IAuthRepository> _mockAuthRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
      _mockAuthRepository = new Mock<IAuthRepository>();
      _mockConfiguration = new Mock<IConfiguration>();
      _authService = new AuthService(_mockAuthRepository.Object, _mockConfiguration.Object);
    }

    [Fact]
    public async Task RegisterUser_WithExistingUser_ShouldThrowUserAlreadyExistsException()
    {
      // Arrange
      var existingUserId = Guid.NewGuid();
      var existingAuth = new Auth(existingUserId, "hashedPassword123", "randomSalt");

      _mockAuthRepository
        .Setup(repo => repo.GetAuthByUserId(existingUserId))
        .ReturnsAsync(existingAuth);

      RegisterDetails registerDetails = new RegisterDetails(existingUserId, "password123");

      // Act & Assert
      await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _authService.RegisterUser(registerDetails));

      _mockAuthRepository.Verify(repo => repo.GetAuthByUserId(existingUserId), Times.Once);
      _mockAuthRepository.Verify(repo => repo.RegisterUser(It.IsAny<Auth>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUser_WithWeakPassword_ShouldThrowValidationException()
    {
      // Arrange
      RegisterDetails registerDetails = new RegisterDetails(Guid.NewGuid(), "weakpassword");

      // Act & Assert
      await Assert.ThrowsAsync<ValidationException>(() => _authService.RegisterUser(registerDetails));

      _mockAuthRepository.Verify(repo => repo.RegisterUser(It.IsAny<Auth>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUser_WithValidUser_ShouldReturnSuccess()
    {
      // Arrange
      RegisterDetails registerDetails = new RegisterDetails(Guid.NewGuid(), "VeryStroongPassword566");

      // Act
      await _authService.RegisterUser(registerDetails);

      // Assert (implicit): If no exception is thrown, the test passes.
      await Task.CompletedTask;
    }

    [Fact]
    public async Task LoginUser_WithIncorrectPassword_ShouldThrowInvalidCredentialException()
    {
      // Arrange
      var (hashedPassword, salt) = PasswordUtils.HashPassword("DifferentPassword555");
      LoginCredentials loginCredentials = new LoginCredentials(Guid.NewGuid(), "password");
      Auth userAuth = new Auth(loginCredentials.UserId, hashedPassword, salt);

      _mockAuthRepository
        .Setup(repo => repo.GetAuthByUserId(loginCredentials.UserId))
        .ReturnsAsync(userAuth);

      // Act & Assert
      await Assert.ThrowsAsync<InvalidCredentialException>(() => _authService.LoginUser(loginCredentials));
    }

    [Fact]
    public async Task LoginUser_WithNonExistentUser_ShouldThrowUserDoesNotExistException()
    {
      // Arrange
      LoginCredentials loginCredentials = new LoginCredentials(Guid.NewGuid(), "VeryStroongPassword566");

      _mockAuthRepository
        .Setup(repo => repo.GetAuthByUserId(loginCredentials.UserId))
        .ReturnsAsync((Auth?)null);

      // Act & Assert
      await Assert.ThrowsAsync<UserDoesNotExistException>(() => _authService.LoginUser(loginCredentials));
      _mockAuthRepository.Verify(repo => repo.GetAuthByUserId(loginCredentials.UserId), Times.Once);
    }

    [Fact]
    public async Task LoginUser_WithValidCredentials_ShouldReturnTokens()
    {
      // Arrange
      LoginCredentials loginCredentials = new LoginCredentials(Guid.NewGuid(), "VeryStroongPassword566");
      var (hashedPassword, salt) = PasswordUtils.HashPassword(loginCredentials.PlainTextPassword);
      Auth userAuth = new Auth(loginCredentials.UserId, hashedPassword, salt);

      _mockAuthRepository
          .Setup(repo => repo.GetAuthByUserId(loginCredentials.UserId))
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
    public async Task RefreshAccessToken_WithInvalidRefreshToken_ShouldThrowInvalidTokenException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task RefreshAccessToken_WithExpiredToken_ShouldThrowUnauthorizedException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task RefreshAccessToken_WithValidToken_ShouldReturnNewTokens()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task DeleteUser_WithInvalidUserId_ShouldThrowNotFoundException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task DeleteUser_WithValidUser_ShouldRemoveUser()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task DeleteUser_WithUnauthorizedUser_ShouldThrowForbiddenException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task GetUserIdByToken_WithInvalidToken_ShouldThrowUnauthorizedException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task GetUserIdByToken_WithExpiredToken_ShouldThrowUnauthorizedException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task GetUserIdByToken_WithValidToken_ShouldReturnUserId()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task LogoutUser_WithInvalidUserId_ShouldThrowNotFoundException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task LogoutUser_WithValidUser_ShouldInvalidateTokens()
    {
      throw new NotImplementedException();
    }
  }
}
