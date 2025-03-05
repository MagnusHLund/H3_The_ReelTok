using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // TODO: Many places can probably just use the ExternalUserEntity.
        public async Task<UserEntity> CreateUserAsync(string username, string email, string password)
        {
            UserEntity userToCreate = UsersFactory.CreateUserEntity(username, email);
            UserEntity returnUser = await _userRepository.CreateUserAsync(userToCreate).ConfigureAwait(false);

            bool isUserCreatedInAuthApi = await CreateUserInAuthApiAsync(returnUser.UserId, password).ConfigureAwait(false);

            if (!isUserCreatedInAuthApi)
            {
                await DeleteUserAsync(returnUser.UserId).ConfigureAwait(false);
                throw new InvalidOperationException("User creation failed in Auth API!");
            }

            return returnUser;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid userId)
        {
            UserEntity user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            return user;
        }

        public async Task<UserEntity> UpdateUserAsync(Guid userId, string? username, string? email)
        {
            if (!string.IsNullOrEmpty(username))
            {
                // TODO: Update user in users database
            }

            if (!string.IsNullOrEmpty(email))
            {
                // TODO: Update user in auth database
            }

            UserEntity updatedUser = await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
            return updatedUser;
        }

        public async Task<List<UserEntity>> GetUsersByIdsAsync(List<Guid> userIds)
        {
            List<UserEntity> users = await _userRepository.GetUsersByUserIdsAsync(userIds).ConfigureAwait(false);
            return users;
        }

        private async Task<bool> CreateUserInAuthApiAsync(Guid userId, string password)
        {
            // TODO: Call recommendation api to add users recommendation.
            return true;
        }

        private async Task<bool> DeleteUserAsync(Guid userId)
        {
            bool IsUserDeleted = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
            return IsUserDeleted;
        }
    }
}