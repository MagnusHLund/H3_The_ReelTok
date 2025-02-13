using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            User returnUser;

            try
            {
                returnUser = await _userRepository.CreateUserAsync(user).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("User creation failed.", ex);
            }

            return returnUser;
        }
        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        public async Task<User?> UpdateUserAsync(User user, Guid userId)
        {
            User? updatedUser = await _userRepository.UpdateUserAsync(user, userId).ConfigureAwait(false);

            if (updatedUser == null)
            {
                return null;
            }

            return updatedUser;
        }
        public Task<bool> DeleteUserAsync(Guid userId)
        {
            bool IsUserDeleted;

            try
            {
                IsUserDeleted = _userRepository.DeleteUserAsync(userId).Result;
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("User deletion failed.", ex);
            }

            return Task.FromResult(IsUserDeleted);
        }
    }
}