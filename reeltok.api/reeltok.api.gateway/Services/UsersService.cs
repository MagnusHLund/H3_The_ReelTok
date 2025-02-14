using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class UsersService : BaseService, IUsersService
    {
        private const string UsersMicroServiceBaseUrl = "http://localhost:5001/api/users";
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;

        internal UsersService(IAuthService authService, IHttpService httpService)
        {
            _authService = authService;
            _httpService = httpService;
        }

        public async Task<UserProfileData> LoginUser(string email, string password)
        {
            ServiceLoginRequestDto requestDto = new ServiceLoginRequestDto(email, password);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/Login");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceLoginResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleExceptions(response);
        }
        public async Task<UserProfileData> CreateUser(string email, string username, string password)
        {
            ServiceCreateUserRequestDto requestDto = new ServiceCreateUserRequestDto(email, username, password);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/Create");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceCreateUserResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleExceptions(response);
        }
        public async Task<UserProfileData> GetUserProfileData(Guid userId)
        {
            ServiceGetUserProfileDataRequestDto requestDto = new ServiceGetUserProfileDataRequestDto(userId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/GetProfileData");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserProfileDataResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleExceptions(response);
        }
        public async Task<EditableUserDetails> UpdateUserDetails(string username, string email)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceUpdateUserDetailsRequestDto requestDto = new ServiceUpdateUserDetailsRequestDto(userId, username, email);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/UpdateDetails");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(requestDto, targetUrl, HttpMethod.Put).ConfigureAwait(false);

            if (response.Success && response is ServiceUpdateUserDetailsResponseDto responseDto)
            {
                return new EditableUserDetails(responseDto.Username, responseDto.Email);
            }

            throw HandleExceptions(response);
        }

        public async Task<string> UpdateProfilePicture(IFormFile image)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceUpdateProfilePictureRequestDto requestDto = new ServiceUpdateProfilePictureRequestDto(userId, image);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/UpdateProfilePicture");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(requestDto, targetUrl, HttpMethod.Put).ConfigureAwait(false);

            if (response.Success && response is ServiceUpdateProfilePictureResponseDto responseDto)
            {
                return responseDto.ProfilePictureUrl;
            }

            throw HandleExceptions(response);
        }

        public async Task<List<UserDetails>> GetAllSubscriptionsForUser(Guid userId)
        {
            ServiceGetAllSubscriptionsForUserRequestDto requestDto = new ServiceGetAllSubscriptionsForUserRequestDto(userId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/GetSubscriptions");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetAllSubscriptionsForUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleExceptions(response);
        }
        public async Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId)
        {
            ServiceGetAllSubscribingToUserRequestDto requestDto = new ServiceGetAllSubscribingToUserRequestDto(userId);
            Uri targetUrl = new Uri($"{UsersMicroServiceBaseUrl}/GetSubscribers");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetAllSubscribingToUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleExceptions(response);
        }
    }
}
