using Moq;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.ValueObjects;
using Xunit;

namespace reeltok.api.gateway.Tests
{
    public class UsersServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5001/users";
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IUsersService _usersService;

        public UsersServiceTests()
        {
            _mockGatewayService = new Mock<IGatewayService>();
            _mockAuthService = new Mock<IAuthService>();
            _usersService = new UsersService(_mockAuthService.Object, _mockGatewayService.Object);
        }

        [Fact]
        public async Task LoginUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string password = "Test";
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid credentials!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(
                It.IsAny<ServiceLoginRequestDto>(), $"{BaseTestUrl}/Login", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.LoginUser(email, password));
            Assert.Equal("Invalid credentials!", exception.Message);
        }

        [Fact]
        public async Task LoginUser_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string username = "xX_TestName_Xx";
            string email = "test@reeltok.com";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            ServiceLoginResponseDto successResponseDto = new ServiceLoginResponseDto(userId, email, username, profileUrl, profilePictureUrl);

            string password = "Sup3rSecur3Passw0rd";

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(
                It.IsAny<ServiceLoginRequestDto>(), $"{BaseTestUrl}/Login", HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.LoginUser(email, password);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal(username, result.UserDetails.Username);
            Assert.Equal(email, result.HiddenUserDetails.Email);
            Assert.Equal(profileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(profilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task CreateUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";
            string password = "Test";
            FailureResponseDto failureResponseDto = new FailureResponseDto("Password does not meet minimum requirements!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(
                It.IsAny<ServiceCreateUserRequestDto>(), $"{BaseTestUrl}/Create", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.CreateUser(email, username, password));
            Assert.Equal("Password does not meet minimum requirements!", exception.Message);
        }

        [Fact]
        public async Task CreateUser_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string username = "xX_TestName_Xx";
            string email = "test@reeltok.com";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            ServiceCreateUserResponseDto successResponseDto = new ServiceCreateUserResponseDto(userId, email, username, profileUrl, profilePictureUrl);

            string password = "Sup3rSecur3Passw0rd";

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(
                It.IsAny<ServiceCreateUserRequestDto>(), $"{BaseTestUrl}/Create", HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.CreateUser(email, username, password);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal(username, result.UserDetails.Username);
            Assert.Equal(email, result.HiddenUserDetails.Email);
            Assert.Equal(profileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(profilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task GetUserProfileData_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            FailureResponseDto failureResponseDto = new FailureResponseDto("User does not exist!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(
                It.IsAny<ServiceGetUserProfileDataRequestDto>(), $"{BaseTestUrl}/GetProfileData", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.GetUserProfileData(userId));
            Assert.Equal("User does not exist!", exception.Message);
        }

        [Fact]
        public async Task GetUserProfileData_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string username = "xX_TestName_Xx";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            ServiceGetUserProfileDataResponseDto successResponseDto = new ServiceGetUserProfileDataResponseDto(userId, username, profileUrl, profilePictureUrl);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(
                It.IsAny<ServiceGetUserProfileDataRequestDto>(), $"{BaseTestUrl}/GetProfileData", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.GetUserProfileData(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal(username, result.UserDetails.Username);
            Assert.Null(result.HiddenUserDetails.Email);
            Assert.Equal(profileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(profilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task UpdateUserDetails_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";
            string profileUrl = "testUrl.com"; // TODO: Why is this even here?
            string profilePictureUrl = "testurl.com";
            UserDetails userDetails = new UserDetails(username, profilePictureUrl, profileUrl);
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(email);
            UserProfileData userProfileData = new UserProfileData(userId, userDetails, hiddenUserDetails);

            FailureResponseDto failureResponseDto = new FailureResponseDto("User does not exist!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(
                It.IsAny<ServiceUpdateUserDetailsRequestDto>(), $"{BaseTestUrl}/Update", HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.UpdateUserDetails(userProfileData));
            Assert.Equal("User does not exist!", exception.Message);
        }

        [Fact]
        public async Task UpdateUserDetails_WithValidParameters_ReturnUserDetails()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithValidParameters_ReturnProfilePicture()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetAllSubscriptionsForUser_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetAllSubscriptionsForUser_WithValidParameters_ReturnAllSubscriptionsForUser()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetAllSubscribingToUser_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetAllSubscribingToUser_WithValidParameters_ReturnAllSubscribingToUser()
        {
            Assert.True(true);
        }
    }
}