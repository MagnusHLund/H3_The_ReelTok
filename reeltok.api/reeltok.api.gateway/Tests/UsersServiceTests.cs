using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Tests
{
    public class UsersServiceTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IUsersService _usersService;

        public UsersServiceTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _mockAuthService = new Mock<IAuthService>();
            _usersService = new UsersService(_mockAuthService.Object, _mockHttpService.Object);
        }

        [Fact]
        public async Task LoginUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string password = "Test";
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid credentials!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("login");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(
                It.IsAny<ServiceLoginRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.LoginUser(email, password));
            Assert.Equal("Invalid credentials!", exception.Message);
        }

        [Fact]
        public async Task LoginUser_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            ServiceLoginResponseDto successResponseDto = TestDataFactory.CreateServiceLoginResponseDto();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("login");

            string password = "Sup3rSecur3Passw0rd";

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(
                It.IsAny<ServiceLoginRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.LoginUser(successResponseDto.Email, password);

            // Assert
            Assert.Equal(successResponseDto.UserId, result.UserId);
            Assert.Equal(successResponseDto.Username, result.UserDetails.Username);
            Assert.Equal(successResponseDto.Email, result.HiddenUserDetails.Email);
            Assert.Equal(successResponseDto.ProfileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(successResponseDto.ProfilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task CreateUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";
            string password = "Test";
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Password does not meet minimum requirements!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("create");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(
                It.IsAny<ServiceCreateUserRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.CreateUser(email, username, password));
            Assert.Equal("Password does not meet minimum requirements!", exception.Message);
        }

        [Fact]
        public async Task CreateUser_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            ServiceCreateUserResponseDto successResponseDto = TestDataFactory.CreateServiceCreateUserResponseDto();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("create");

            string password = "Sup3rSecur3Passw0rd";

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(
                It.IsAny<ServiceCreateUserRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.CreateUser(successResponseDto.Email, successResponseDto.Username, password);

            // Assert
            Assert.Equal(successResponseDto.UserId, result.UserId);
            Assert.Equal(successResponseDto.Username, result.UserDetails.Username);
            Assert.Equal(successResponseDto.Email, result.HiddenUserDetails.Email);
            Assert.Equal(successResponseDto.ProfileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(successResponseDto.ProfilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task GetUserProfileData_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("User does not exist!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getProfileData");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(
                It.IsAny<ServiceGetUserProfileDataRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.GetUserProfileData(userId));
            Assert.Equal("User does not exist!", exception.Message);
        }

        [Fact]
        public async Task GetUserProfileData_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            ServiceGetUserProfileDataResponseDto successResponseDto = TestDataFactory.CreateGetUserProfileDataResponse();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getProfileData");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(
                It.IsAny<ServiceGetUserProfileDataRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(successResponseDto);

            // Act
            UserProfileData result = await _usersService.GetUserProfileData(successResponseDto.UserId);

            // Assert
            Assert.Equal(successResponseDto.UserId, result.UserId);
            Assert.Equal(successResponseDto.Username, result.UserDetails.Username);
            Assert.Equal(successResponseDto.Email, result.HiddenUserDetails.Email);
            Assert.Equal(successResponseDto.ProfileUrl, result.UserDetails.ProfileUrl);
            Assert.Equal(successResponseDto.ProfilePictureUrl, result.UserDetails.ProfilePictureUrl);
        }

        [Fact]
        public async Task UpdateUserDetails_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";

            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("User does not exist!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("updateDetails");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(
                It.IsAny<ServiceUpdateUserDetailsRequestDto>(), targetUrl, HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.UpdateUserDetails(username, email));
            Assert.Equal("User does not exist!", exception.Message);
        }

        [Fact]
        public async Task UpdateUserDetails_WithValidParameters_ReturnUserDetails()
        {
            // Arrange
            ServiceUpdateUserDetailsResponseDto successResponseDto = TestDataFactory.CreateUpdateUserDetailsResponse();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("updateDetails");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(
                It.IsAny<ServiceUpdateUserDetailsRequestDto>(), targetUrl, HttpMethod.Put))
                .ReturnsAsync(successResponseDto);

            // Act
            EditableUserDetails result = await _usersService.UpdateUserDetails(successResponseDto.Email, successResponseDto.Username);

            // Assert
            Assert.Equal(successResponseDto.Username, result.Username);
            Assert.Equal(successResponseDto.Email, result.Email);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            IFormFile image = new Mock<IFormFile>().Object;

            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Error uploading profile picture!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("updateProfilePicture");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(
                It.IsAny<ServiceUpdateProfilePictureRequestDto>(), targetUrl, HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.UpdateProfilePicture(image));
            Assert.Equal("Error uploading profile picture!", exception.Message);
        }

        [Fact]
        public async Task UpdateProfilePicture_WithValidParameters_ReturnProfilePicture()
        {
            // Arrange
            Stream stream = new MemoryStream();
            IFormFile image = new FormFile(stream, 0, 0, "file", "file name");

            ServiceUpdateProfilePictureResponseDto successResponseDto = TestDataFactory.CreateUpdateProfilePictureResponse();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("updateProfilePicture");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(
                It.IsAny<ServiceUpdateProfilePictureRequestDto>(), targetUrl, HttpMethod.Put))
                .ReturnsAsync(successResponseDto);

            // Act
            string result = await _usersService.UpdateProfilePicture(image);

            // Assert
            Assert.Equal(successResponseDto.ProfilePictureUrl, result);
        }

        [Fact]
        public async Task GetAllSubscriptionsForUser_WithBadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Unable to retrieve subscriptions!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getSubscriptions");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(
                It.IsAny<ServiceGetAllSubscriptionsForUserRequestDto>(), targetUrl, HttpMethod.Get))
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

            List<UserDetails> usersDetails = TestDataFactory.CreateUserDetailsList();
            ServiceGetAllSubscriptionsForUserResponseDto successResponseDto = TestDataFactory.CreateGetAllSubscriptionsForUserResponse(usersDetails);
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getSubscriptions");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(
                It.IsAny<ServiceGetAllSubscriptionsForUserRequestDto>(), targetUrl, HttpMethod.Get))
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

            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Unable to retrieve subscribers!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getSubscribers");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(
                It.IsAny<ServiceGetAllSubscribingToUserRequestDto>(), targetUrl, HttpMethod.Get))
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

            List<UserDetails> usersDetails = TestDataFactory.CreateUserDetailsList();
            ServiceGetAllSubscribingToUserResponseDto successResponseDto = TestDataFactory.CreateGetAllSubscribingToUserResponse(usersDetails);
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("getSubscribers");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(
                It.IsAny<ServiceGetAllSubscribingToUserRequestDto>(), targetUrl, HttpMethod.Get))
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
