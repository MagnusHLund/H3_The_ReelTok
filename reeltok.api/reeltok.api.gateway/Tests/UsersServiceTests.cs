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

            ServiceGetUserProfileDataResponseDto successResponseDto = new ServiceGetUserProfileDataResponseDto(username, profileUrl, profilePictureUrl);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(
                It.IsAny<ServiceGetUserProfileDataRequestDto>(), $"{BaseTestUrl}/GetProfileData", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.GetUserProfileData(userId);

            // Assert
            Assert.Equal(userId, result.UserId);
            Assert.Equal(username, result.UserDetails.Username);
            Assert.Equal(profileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(profilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task UpdateUserDetails_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";

            FailureResponseDto failureResponseDto = new FailureResponseDto("User does not exist!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(
                It.IsAny<ServiceUpdateUserDetailsRequestDto>(), $"{BaseTestUrl}/UpdateDetails", HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.UpdateUserDetails(username, email));
            Assert.Equal("User does not exist!", exception.Message);
        }

        [Fact]
        public async Task UpdateUserDetails_WithValidParameters_ReturnUserDetails()
        {
            // Arrange
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";

            ServiceUpdateUserDetailsResponseDto successResponseDto = new ServiceUpdateUserDetailsResponseDto(username, email);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(
                It.IsAny<ServiceUpdateUserDetailsRequestDto>(), $"{BaseTestUrl}/UpdateDetails", HttpMethod.Put))
                .ReturnsAsync(successResponseDto);

            // Act
            EditableUserDetails result = await _usersService.UpdateUserDetails(username, email);

            // Assert
            Assert.Equal(username, result.Username);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Stream stream = new MemoryStream();
            IFormFile image = new FormFile(stream, 0, 0, "file", "file name");

            FailureResponseDto failureResponseDto = new FailureResponseDto("Error uploading profile picture!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(
                It.IsAny<ServiceUpdateProfilePictureRequestDto>(), $"{BaseTestUrl}/UpdateProfilePicture", HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.UpdateProfilePicture(image));
            Assert.Equal("Error uploading profile picture!", exception.Message);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithValidParameters_ReturnProfilePicture()
        {
            // Arrange
            string profilePictureUrl = "pictureUrl";
            Stream stream = new MemoryStream();
            IFormFile image = new FormFile(stream, 0, 0, "file", "file name");

            ServiceUpdateProfilePictureResponseDto successResponseDto = new ServiceUpdateProfilePictureResponseDto(profilePictureUrl);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(
                It.IsAny<ServiceUpdateProfilePictureRequestDto>(), $"{BaseTestUrl}/UpdateProfilePicture", HttpMethod.Put))
                .ReturnsAsync(successResponseDto);

            // Act
            string result = await _usersService.UpdateProfilePicture(image);

            // Assert
            Assert.Equal(profilePictureUrl, result);
        }

        [Fact]
        public async Task GetAllSubscriptionsForUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to retrieve subscriptions!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(
                It.IsAny<ServiceGetAllSubscriptionsForUserRequestDto>(), $"{BaseTestUrl}/GetSubscriptions", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.GetAllSubscriptionsForUser(userId));
            Assert.Equal("Unable to retrieve subscriptions!", exception.Message);
        }

        [Fact]
        public async Task GetAllSubscriptionsForUser_WithValidParameters_ReturnAllSubscriptionsForUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<UserDetails> usersDetails = new List<UserDetails>()
            {
                new UserDetails( "username1", "profilePictureUrl1", "profileUrl1"),
                new UserDetails( "username2", "profilePictureUrl2", "profileUrl2")
            };
            ServiceGetAllSubscriptionsForUserResponseDto successResponseDto = new ServiceGetAllSubscriptionsForUserResponseDto(usersDetails);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(
                It.IsAny<ServiceGetAllSubscriptionsForUserRequestDto>(), $"{BaseTestUrl}/GetSubscriptions", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            List<UserDetails> result = await _usersService.GetAllSubscriptionsForUser(userId);

            // Assert
            Assert.Equal(usersDetails.Count, result.Count);
            for (int i = 0; i < usersDetails.Count; i++)
            {
                Assert.Equal(usersDetails[i].Username, result[i].Username);
                Assert.Equal(usersDetails[i].ProfilePictureUrl, result[i].ProfilePictureUrl);
                Assert.Equal(usersDetails[i].ProfileUrl, result[i].ProfileUrl);

            }
        }

        [Fact]
        public async Task GetAllSubscribingToUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to retrieve subscribers!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(
                It.IsAny<ServiceGetAllSubscribingToUserRequestDto>(), $"{BaseTestUrl}/GetSubscribers", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.GetAllSubscribingToUser(userId));
            Assert.Equal("Unable to retrieve subscribers!", exception.Message);
        }

        [Fact]
        public async Task GetAllSubscribingToUser_WithValidParameters_ReturnAllSubscribingToUser()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            List<UserDetails> usersDetails = new List<UserDetails>()
            {
                new UserDetails( "username1", "profilePictureUrl1", "profileUrl1"),
                new UserDetails( "username2", "profilePictureUrl2", "profileUrl2")
            };
            ServiceGetAllSubscribingToUserResponseDto successResponseDto = new ServiceGetAllSubscribingToUserResponseDto(usersDetails);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(
                It.IsAny<ServiceGetAllSubscribingToUserRequestDto>(), $"{BaseTestUrl}/GetSubscribers", HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            List<UserDetails> result = await _usersService.GetAllSubscribingToUser(userId);

            // Assert
            Assert.Equal(usersDetails.Count, result.Count);
            for (int i = 0; i < usersDetails.Count; i++)
            {
                Assert.Equal(usersDetails[i].Username, result[i].Username);
                Assert.Equal(usersDetails[i].ProfilePictureUrl, result[i].ProfilePictureUrl);
                Assert.Equal(usersDetails[i].ProfileUrl, result[i].ProfileUrl);

            }
        }
    }
}