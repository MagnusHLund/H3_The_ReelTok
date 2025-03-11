using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Users.Login;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Users.CreateUser;
using reeltok.api.gateway.DTOs.Users.UpdateUserDetails;
using reeltok.api.gateway.DTOs.Users.GetUserProfileData;
using reeltok.api.gateway.DTOs.Users.UpdateProfilePicture;
using reeltok.api.gateway.DTOs.Users.GetAllSubscribingToUser;
using reeltok.api.gateway.DTOs.Users.GetAllSubscriptionsForUser;

namespace reeltok.api.gateway.Services
{
    internal class UsersService : BaseService, IUsersService
    {
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        internal UsersService(IAuthService authService, IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _authService = authService;
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<UserProfileData> LoginUser(string email, string password)
        {
            ServiceLoginRequestDto requestDto = new ServiceLoginRequestDto(email, password);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("login");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLoginRequestDto, ServiceLoginResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceLoginResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleNetworkResponseExceptions(response);
        }
        public async Task<UserProfileData> CreateUser(string email, string username, string password)
        {
            ServiceCreateUserRequestDto requestDto = new ServiceCreateUserRequestDto(email, username, password);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceCreateUserRequestDto, ServiceCreateUserResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceCreateUserResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleNetworkResponseExceptions(response);
        }
        public async Task<UserProfileData> GetUserProfileData(Guid userId)
        {
            ServiceGetUserProfileDataRequestDto requestDto = new ServiceGetUserProfileDataRequestDto(userId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserProfileDataRequestDto, ServiceGetUserProfileDataResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetUserProfileDataResponseDto responseDto)
            {
                return UserMapper.ConvertResponseDtoToUserProfileData(responseDto);
            }

            throw HandleNetworkResponseExceptions(response);
        }
        public async Task<EditableUserDetails> UpdateUserDetails(string username, string email)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);

            ServiceUpdateUserDetailsRequestDto requestDto = new ServiceUpdateUserDetailsRequestDto(userId, username, email);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUpdateUserDetailsRequestDto, ServiceUpdateUserDetailsResponseDto>(requestDto, targetUrl, HttpMethod.Put).ConfigureAwait(false);

            if (response.Success && response is ServiceUpdateUserDetailsResponseDto responseDto)
            {
                return new EditableUserDetails(responseDto.Username, responseDto.Email);
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<string> UpdateProfilePicture(IFormFile image)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);

            ServiceUpdateProfilePictureRequestDto requestDto = new ServiceUpdateProfilePictureRequestDto(userId, image);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("users/profile-picture");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceUpdateProfilePictureRequestDto, ServiceUpdateProfilePictureResponseDto>(requestDto, targetUrl, HttpMethod.Put).ConfigureAwait(false);

            if (response.Success && response is ServiceUpdateProfilePictureResponseDto responseDto)
            {
                return responseDto.ProfilePictureUrl;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<UserDetails>> GetAllSubscriptionsForUser(Guid userId)
        {
            ServiceGetAllSubscriptionsForUserRequestDto requestDto = new ServiceGetAllSubscriptionsForUserRequestDto(userId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("subscriptions/subscriptions");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetAllSubscriptionsForUserRequestDto, ServiceGetAllSubscriptionsForUserResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetAllSubscriptionsForUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleNetworkResponseExceptions(response);
        }
        public async Task<List<UserDetails>> GetAllSubscribingToUser(Guid userId)
        {
            ServiceGetAllSubscribingToUserRequestDto requestDto = new ServiceGetAllSubscribingToUserRequestDto(userId);
            Uri targetUrl = _endpointFactory.GetUsersApiUrl("subscriptions/subscribers");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetAllSubscribingToUserRequestDto, ServiceGetAllSubscribingToUserResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceGetAllSubscribingToUserResponseDto responseDto)
            {
                return responseDto.Users;
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
