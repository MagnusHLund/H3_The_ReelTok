using Moq;
using Xunit;
using reeltok.api.users.DTOs;
using reeltok.api.users.Services;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Factories;
using reeltok.api.users.Exceptions;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.DTOs.Login;
using reeltok.api.users.DTOs.GetUserInterest;

namespace reeltok.api.users.Tests.Services
{
    public class ExternalApiServiceTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IEndpointFactory> _mockEndpointFactory;
        private readonly ExternalApiService _externalApiService;

        public ExternalApiServiceTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _mockEndpointFactory = new Mock<IEndpointFactory>();
            _externalApiService = new ExternalApiService(_mockHttpService.Object, _mockEndpointFactory.Object);
        }

        #region Success

        [Fact]
        public async Task CreateUserInAuthApiAsync_WithValidRequest_Succeeds()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string password = "testPassword";
            Uri targetUrl = new Uri("https://testauthapi.com/users");
            AuthServiceCreateUserResponseDto responseDto = new AuthServiceCreateUserResponseDto { Success = true };

            _mockEndpointFactory.Setup(x => x.GetAuthApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<AuthServiceCreateUserRequestDto, AuthServiceCreateUserResponseDto>(
                It.IsAny<AuthServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            await _externalApiService.CreateUserInAuthApiAsync(userId, password);

            // Assert
            _mockHttpService.Verify(x => x.ProcessRequestAsync<AuthServiceCreateUserRequestDto, AuthServiceCreateUserResponseDto>(
                It.IsAny<AuthServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserInRecommendationsApiAsync_WithValidRequest_Succeeds()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte userInterests = 5;
            Uri targetUrl = new Uri("https://testrecommendationsapi.com/users");
            RecommendationsServiceCreateUserResponseDto responseDto = new RecommendationsServiceCreateUserResponseDto { Success = true };

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceCreateUserRequestDto, RecommendationsServiceCreateUserResponseDto>(
                It.IsAny<RecommendationsServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            await _externalApiService.CreateUserInRecommendationsApiAsync(userId, userInterests);

            // Assert
            _mockHttpService.Verify(x => x.ProcessRequestAsync<RecommendationsServiceCreateUserRequestDto, RecommendationsServiceCreateUserResponseDto>(
                It.IsAny<RecommendationsServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()), Times.Once);
        }

        [Fact]
        public async Task LoginUserInAuthApiAsync_WithValidRequest_Succeeds()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string password = "testpassword";
            Uri targetUrl = new Uri("https://testauthapi.com/auth");
            AuthServiceLoginResponseDto responseDto = new AuthServiceLoginResponseDto { Success = true };

            _mockEndpointFactory.Setup(x => x.GetAuthApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<AuthServiceLoginRequestDto, AuthServiceLoginResponseDto>(
                It.IsAny<AuthServiceLoginRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            await _externalApiService.LoginUserInAuthApiAsync(userId, password);

            // Assert
            _mockHttpService.Verify(x => x.ProcessRequestAsync<AuthServiceLoginRequestDto, AuthServiceLoginResponseDto>(
                It.IsAny<AuthServiceLoginRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()), Times.Once);
        }

        [Fact]
        public async Task GetUserInterestFromRecommendationsApiAsync_WithValidRequest_ReturnsInterest()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Uri targetUrl = new Uri("https://testrecommendationsapi.com/users");
            RecommendationServiceGetUserInterestResponseDto responseDto = new RecommendationServiceGetUserInterestResponseDto(5);

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationServiceGetUserInterestRequestDto, RecommendationServiceGetUserInterestResponseDto>(
                It.IsAny<RecommendationServiceGetUserInterestRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            byte result = await _externalApiService.GetUserInterestFromRecommendationsApiAsync(userId);

            // Assert
            Assert.Equal(responseDto.UserInterest, result);
        }

        #endregion

        #region failure

        [Fact]
        public async Task CreateUserInAuthApiAsync_WithFailureResponse_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string password = "testpassword";
            Uri targetUrl = new Uri("https://testauthapi.com/users");
            FailureResponseDto responseDto = new FailureResponseDto("Error");

            _mockEndpointFactory.Setup(x => x.GetAuthApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<AuthServiceCreateUserRequestDto, BaseResponseDto>(
                It.IsAny<AuthServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act & Assert
            FailureNetworkResponseException exception = await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.CreateUserInAuthApiAsync(userId, password));
            Assert.Contains("Error", exception.Message);
        }

        [Fact]
        public async Task CreateUserInRecommendationsApiAsync_WithFailureResponse_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte userInterests = 5;
            Uri targetUrl = new Uri("https://testrecommendationsapi.com/users");
            FailureResponseDto responseDto = new FailureResponseDto("Error");

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceCreateUserRequestDto, BaseResponseDto>(
                It.IsAny<RecommendationsServiceCreateUserRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act & Assert
            FailureNetworkResponseException exception = await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.CreateUserInRecommendationsApiAsync(userId, userInterests));
            Assert.Contains("Error", exception.Message);
        }

        [Fact]
        public async Task LoginUserInAuthApiAsync_WithFailureResponse_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string password = "testpassword";
            Uri targetUrl = new Uri("https://testauthapi.com/auth");
            FailureResponseDto responseDto = new FailureResponseDto("Error");

            _mockEndpointFactory.Setup(x => x.GetAuthApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<AuthServiceLoginRequestDto, BaseResponseDto>(
                It.IsAny<AuthServiceLoginRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act & Assert
            FailureNetworkResponseException exception = await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.LoginUserInAuthApiAsync(userId, password));
            Assert.Contains("Error", exception.Message);
        }

        [Fact]
        public async Task GetUserInterestFromRecommendationsApiAsync_WithFailureResponse_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Uri targetUrl = new Uri("https://testrecommendationsapi.com/users");
            FailureResponseDto responseDto = new FailureResponseDto("Error");

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationServiceGetUserInterestRequestDto, BaseResponseDto>(
                It.IsAny<RecommendationServiceGetUserInterestRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act & Assert
            FailureNetworkResponseException exception = await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.GetUserInterestFromRecommendationsApiAsync(userId));
            Assert.Contains("Error", exception.Message);
        }

        #endregion
    }
}
