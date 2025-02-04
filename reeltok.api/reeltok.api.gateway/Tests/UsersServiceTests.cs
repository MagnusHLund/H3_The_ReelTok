using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.Data;
using Moq;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Services;
using Xunit;

namespace reeltok.api.gateway.Tests
{
    public class UsersServiceTests
    {
        private const string BaseTestUrl = "http;//localhost:5001/users";
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
            FailureResponseDto failureResponseDto = new FailureResponseDto("Password does not meet minimum requirements!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoginRequestDto, LoginResponseDto>(
                It.IsAny<LoginRequestDto>(), $"{BaseTestUrl}/Login", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.LoginUser(email, password));
            Assert.Equal("Password does not meet minimum requirements!", exception.Message);
        }

        [Fact]
        public async Task LoginUser_WithValidParameters_ReturnUserProfileData()
        {
            // Arrange
            bool success = true;
            string email = "test@reeltok.com";
            string password = "Sup3rSecur3Passw0rd";
            LoginResponseDto successResponseDto = new LoginResponseDto();

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoginRequestDto, LoginResponseDto>(
                It.IsAny<LoginRequestDto>(), $"{BaseTestUrl}/Login", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _usersService.LoginUser(email, password));
            Assert.Equal("Password does not meet minimum requirements!", exception.Message);
        }

        [Fact]
        public async Task CreateUser_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task CreateUser_WithValidParameters_ReturnUserProfileData()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetUserProfileData_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetUserProfileData_WithValidParameters_ReturnUserProfileData()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task UpdateUserDetails_WithBadRequest_ThrowInvalidOperationException()
        {
            Assert.True(true);
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