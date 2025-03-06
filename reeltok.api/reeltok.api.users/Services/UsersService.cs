using reeltok.api.users.utils;
using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;
using reeltok.api.users.DTOs.CreateUser;
using reeltok.api.users.DTOs;

namespace reeltok.api.users.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IHttpService _httpService;

        public UsersService(IUsersRepository userRepository, IHttpService httpService)
        {
            _userRepository = userRepository;
            _httpService = httpService;
        }

        public async Task<UserEntity> CreateUserAsync(string username, string email, string password, byte interests)
        {
            UserEntity userToCreate = UsersFactory.CreateUserEntity(username, email);
            UserEntity createdUser = await _userRepository.CreateUserAsync(userToCreate).ConfigureAwait(false);

            try
            {
                await CreateUserInAuthApiAsync(createdUser.UserId, password).ConfigureAwait(false);
                await CreateUserInRecommendationsApiAsync(createdUser.UserId, interests).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await DeleteUserAsync(createdUser.UserId).ConfigureAwait(false);
                throw new InvalidOperationException($"User creation failed in other API!, {ex}");
            }

            return createdUser;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            UserEntity user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            return user;
        }

        public async Task<UserEntity> UpdateUserAsync(Guid userId, string? username, string? email)
        {
            UserEntity user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(username))
            {
                if (ValidationUtils.IsValidUsername(username))
                {
                    throw new InvalidOperationException("Invalid username");
                }

                user = UsersFactory.UpdateUserEntityUsername(user, username);
            }

            if (!string.IsNullOrEmpty(email))
            {
                // TODO: validate email
                if (!ValidationUtils.IsValidEmail(email))
                {
                    throw new InvalidOperationException("Invalid email");
                }

                user = UsersFactory.UpdateUserEntityEmail(user, email);
            }

            try
            {
                user = await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
            }
            catch
            {
                throw new InvalidOperationException("User update failed!");
            }

            return user;
        }

        public async Task<List<UserEntity>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            List<UserEntity> users = await _userRepository.GetUsersByUserIdsAsync(userIds).ConfigureAwait(false);
            return users;
        }

        private async Task CreateUserInAuthApiAsync(Guid userId, string password)
        {
            // TODO: Less hardcoded uri
            AuthServiceCreateUserRequestDto requestDto = new AuthServiceCreateUserRequestDto(userId, password);
            Uri targetUrl = new Uri("https://localhost:5003/api/auth");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<AuthServiceCreateUserRequestDto, AuthServiceCreateUserResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is AuthServiceCreateUserResponseDto responseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        private async Task CreateUserInRecommendationsApiAsync(Guid userId, byte userInterests)
        {
            // TODO: Less hardcoded uri
            RecommendationsServiceCreateUserRequestDto requestDto = new RecommendationsServiceCreateUserRequestDto(userId, userInterests);
            Uri targetUrl = new Uri("https://localhost:5004/api/recommendations");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<RecommendationsServiceCreateUserRequestDto, RecommendationsServiceCreateUserResponseDto>(
                requestDto, targetUrl, HttpMethod.Post)
                .ConfigureAwait(false);

            if (response.Success && response is RecommendationsServiceCreateUserResponseDto responseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        private async Task<bool> DeleteUserAsync(Guid userId)
        {
            bool IsUserDeleted = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
            return IsUserDeleted;
        }
    }
}