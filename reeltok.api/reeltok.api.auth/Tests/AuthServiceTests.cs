using Moq;
using Xunit;
using System.Threading.Tasks;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Services;
using reeltok.api.auth.DTOs;

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
  }
}
