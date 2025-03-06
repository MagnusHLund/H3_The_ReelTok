using reeltok.api.users.utils;
using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IExternalApiService _externalApiService;

        public UsersService(IUsersRepository userRepository, IExternalApiService externalApiService)
        {
            _userRepository = userRepository;
            _externalApiService = externalApiService;
        }

        public async Task<UserEntity> CreateUserAsync(string username, string email, string password, byte interests)
        {
            UserEntity userToCreate = UsersFactory.CreateUserEntity(username, email);
            UserEntity createdUser = await _userRepository.CreateUserAsync(userToCreate).ConfigureAwait(false);

            try
            {
                await _externalApiService.CreateUserInAuthApiAsync(createdUser.UserId, password).ConfigureAwait(false);
                await _externalApiService.CreateUserInRecommendationsApiAsync(createdUser.UserId, interests).ConfigureAwait(false);
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
                if (!ValidationUtils.IsValidUsername(username))
                {
                    throw new InvalidOperationException("Invalid username");
                }

                user = UsersFactory.UpdateUserEntityUsername(user, username);
            }

            if (!string.IsNullOrEmpty(email))
            {
                if (!ValidationUtils.IsValidEmail(email))
                {
                    throw new InvalidOperationException("Invalid email");
                }

                user = UsersFactory.UpdateUserEntityEmail(user, email);
            }

            user = await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
            return user;
        }

        public async Task<List<UserEntity>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            List<UserEntity> users = await _userRepository.GetUsersByUserIdsAsync(userIds).ConfigureAwait(false);
            return users;
        }

        private async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
        }
    }
}