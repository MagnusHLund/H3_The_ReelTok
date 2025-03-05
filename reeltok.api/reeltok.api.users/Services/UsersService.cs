using reeltok.api.users.Entities;
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

        public async Task<User> CreateUserAsync(User user)
        {
            User returnUser = await _userRepository.CreateUserAsync(user).ConfigureAwait(false);

            // TODO: Call recommendation api to add users recommendation

            return returnUser;
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            return user;
        }

        public async Task<User> UpdateUserAsync(User user, Guid userId)
        {
            if (user?.UserId != userId)
            {
                throw new InvalidOperationException($"User id mismatch. {user?.UserId} does not match {userId}");
            }

            User updatedUser = await _userRepository.UpdateUserAsync(user).ConfigureAwait(false);
            return updatedUser;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            bool IsUserDeleted = await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
            return IsUserDeleted;
        }
    }
}