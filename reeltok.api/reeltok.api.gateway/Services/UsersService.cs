using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class UsersService : BaseService, IUsersService
    {
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/users";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;

        internal UsersService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public async Task<UserProfileData> LoginUser(string email, string password)
        {
            ServiceLoginRequestDto requestDto = new ServiceLoginRequestDto(email, password);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/Login";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is ServiceLoginResponseDto responseDto)
            {
                Guid userId = responseDto.UserId;
                UserDetails userDetails = new UserDetails(responseDto.Username, responseDto.ProfilePictureUrl, responseDto.ProfileUrl);
                HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(responseDto.Email);
                return new UserProfileData(userId, userDetails, hiddenUserDetails);
            }

            throw HandleExceptions(response);
        }
        public async Task<UserProfileData> CreateUser(string email, string username, string password)
        {
            ServiceCreateUserRequestDto requestDto = new ServiceCreateUserRequestDto(email, username, password);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/Create";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is ServiceCreateUserResponseDto responseDto)
            {
                Guid userId = responseDto.UserId;
                UserDetails userDetails = new UserDetails(responseDto.Username, responseDto.ProfilePictureUrl, responseDto.ProfileUrl);
                HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(responseDto.Email);
                return new UserProfileData(userId, userDetails, hiddenUserDetails);
            }

            throw HandleExceptions(response);
        }
        public async Task<UserProfileData> GetUserProfileData(Guid userId)
        {
            return new UserProfileData(Guid.Empty, new UserDetails("", "", ""), new HiddenUserDetails(""));
        }
        public async Task<EditableUserDetails> UpdateUserDetails(EditableUserDetails userDetails)
        {
            return new EditableUserDetails("", "");
        }
        public async Task<string> UpdateProfilePicture(IFormFile image)
        {
            return "Image url";
        }
        public async Task<List<UserDetails>> GetAllSubscriptionsForUser(Guid userId)
        {
            return new List<UserDetails>();
        }
        public async Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId)
        {
            return new List<UserDetails>();
        }
    }
}