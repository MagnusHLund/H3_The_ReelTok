using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mappers;
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
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
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
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleExceptions(response);
        }
        public async Task<UserProfileData> GetUserProfileData(Guid userId)
        {
            ServiceGetUserProfileDataRequestDto requestDto = new ServiceGetUserProfileDataRequestDto(userId);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/GetProfileData";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceGetUserProfileDataResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleExceptions(response);
        }
        public async Task<EditableUserDetails> UpdateUserDetails(string username, string email)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceUpdateUserDetailsRequestDto requestDto = new ServiceUpdateUserDetailsRequestDto(userId, username, email);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/UpdateDetails";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(requestDto, targetUrl, HttpMethod.Put);

            if (response.Success && response is ServiceUpdateUserDetailsResponseDto responseDto)
            {
                return new EditableUserDetails(responseDto.Username, responseDto.Email);
            }

            throw HandleExceptions(response);
        }

        public async Task<string> UpdateProfilePicture(IFormFile image)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceUpdateProfilePictureRequestDto requestDto = new ServiceUpdateProfilePictureRequestDto(userId, image);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/UpdateProfilePicture";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(requestDto, targetUrl, HttpMethod.Put);

            if (response.Success && response is ServiceUpdateProfilePictureResponseDto responseDto)
            {
                return responseDto.ProfilePictureUrl;
            }

            throw HandleExceptions(response);
        }

        public async Task<List<UserDetails>> GetAllSubscriptionsForUser(Guid userId)
        {
            ServiceGetAllSubscriptionsForUserRequestDto requestDto = new ServiceGetAllSubscriptionsForUserRequestDto(userId);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/GetSubscriptions";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceGetAllSubscriptionsForUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleExceptions(response);
        }
        public async Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId)
        {
            ServiceGetAllSubscribingToUserRequestDto requestDto = new ServiceGetAllSubscribingToUserRequestDto(userId);
            string targetUrl = $"{UsersMicroServiceBaseUrl}/GetSubscribers";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceGetAllSubscribingToUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleExceptions(response);
        }
    }
}