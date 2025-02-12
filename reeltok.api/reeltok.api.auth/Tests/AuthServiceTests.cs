using Moq;
using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Services;
using reeltok.api.auth.Utils;

namespace reeltok.api.auth.Tests
{
  public class AuthServiceTests
  {
    private readonly Mock<IAuthRepository> _mockAuthRepository;
    private readonly Mock<IConfiguration> _mockConfiguration;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
      _mockAuthRepository = new Mock<IAuthRepository>();
      _mockConfiguration = new Mock<IConfiguration>();
      _authService = new AuthService(_mockAuthRepository.Object);
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
        _mockAuthRepository.Verify(repo => repo.CreateUser(It.IsAny<UserAuthentication>()), Times.Never);
    }

    [Fact]
    public async Task CreateUser_WithWeakPassword_ShouldThrowValidationException()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        CreateDetails CreateDetails = new CreateDetails(userId, "weakpassword");

        string expectedExceptionMessage = "Password does not follow the minimum requirements!";

        // Act & Assert
        ValidationException exception = await Assert.ThrowsAsync<ValidationException>(() => _authService.CreateUser(CreateDetails));
        Assert.Equal(expectedExceptionMessage, exception.Message);

        _mockAuthRepository.Verify(repo => repo.DoesUserExist(userId), Times.Once);
        _mockAuthRepository.Verify(repo => repo.CreateUser(It.IsAny<UserAuthentication>()), Times.Never);
    }

    [Fact]
    public async Task CreateUser_WithValidUser_ShouldReturnSuccess()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        CreateDetails createDetails = new CreateDetails(userId, "VeryStroongPassword566");

        _mockAuthRepository
          .Setup(repo => repo.CreateUser(It.IsAny<UserAuthentication>()))
          .ReturnsAsync(userAuth);

        // Act
        Tokens tokens = await _authService.CreateUser(createDetails);

        // Assert (implicit): If no exception is thrown, the test passes.
        Assert.NotNull(tokens);
        Assert.NotNull(tokens.AccessToken);
        Assert.NotEmpty(tokens.AccessToken.Token);
        Assert.NotNull(tokens.RefreshToken);
        Assert.NotEmpty(tokens.RefreshToken.Token);
    }

    [Fact]
    public async Task LoginUser_WithIncorrectPassword_ShouldThrowInvalidCredentialException()
    {
        // Arrange
        HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword("DifferentPassword555");
        LoginCredentials loginCredentials = new LoginCredentials(Guid.NewGuid(), "password");
        UserAuthentication userAuth = new UserAuthentication(loginCredentials.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);

        _mockAuthRepository
          .Setup(repo => repo.GetUserAuthenticationByUserId(loginCredentials.UserId))
          .ReturnsAsync(userAuth);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialException>(() => _authService.LoginUser(loginCredentials));
    }

    [Fact]
    public async Task LoginUser_WithValidCredentials_ShouldReturnTokens()
    {
      // Arrange
      LoginCredentials loginCredentials = new LoginCredentials(Guid.NewGuid(), "VeryStroongPassword566");
      HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword(loginCredentials.PlainTextPassword);
      UserAuthentication userAuth = new UserAuthentication(loginCredentials.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);

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
