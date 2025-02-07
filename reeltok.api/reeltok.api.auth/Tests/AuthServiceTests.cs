using Moq;
using Xunit;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
      _mockAuthRepository = new Mock<IAuthRepository>();
      _authService = new AuthService(_mockAuthRepository.Object);
    }

    [Fact]
    public async Task RegisterUser_WithExistingUser_ShouldThrowArgumentException()
    {
      var existingUserId = Guid.NewGuid();
      var existingAuth = new Auth(existingUserId, "hashedPassword123", "randomSalt");

      _mockAuthRepository
        .Setup(repo => repo.GetAuthByUserId(existingUserId))
        .ReturnsAsync(existingAuth);

      RegisterDetails registerDetails = new RegisterDetails(existingUserId, "password123");

      await Assert.ThrowsAsync<ArgumentException>(() => _authService.RegisterUser(registerDetails));

      _mockAuthRepository.Verify(repo => repo.GetAuthByUserId(existingUserId), Times.Once);
      _mockAuthRepository.Verify(repo => repo.RegisterUser(It.IsAny<Auth>()), Times.Never);
    }
    
    [Fact]
    public async Task RegisterUser_WithWeakPassword_ShouldThrowValidationException()
    {
      RegisterDetails registerDetails = new RegisterDetails(Guid.NewGuid(), "weakpassword");
      
      await Assert.ThrowsAsync<ValidationException>(() => _authService.RegisterUser(registerDetails));

      _mockAuthRepository.Verify(repo => repo.RegisterUser(It.IsAny<Auth>()), Times.Never);
    }

    [Fact]
    public async Task RegisterUser_WithValidUser_ShouldReturnSuccess()
    {
      RegisterDetails registerDetails = new RegisterDetails(Guid.NewGuid(), "VeryStroooongPassword5666");

      await Assert.
    }

    [Fact]
    public async Task LoginUser_WithIncorrectPassword_ShouldThrowUnauthorizedException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task LoginUser_WithNonExistentUser_ShouldThrowNotFoundException()
    {
      throw new NotImplementedException();
    }

    [Fact]
    public async Task LoginUser_WithValidCredentials_ShouldReturnTokens()
    {
      throw new NotImplementedException();
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
