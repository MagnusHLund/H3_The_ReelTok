using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.Utils;

namespace reeltok.api.users.Services
{
    public class UsersService : IUsersService
    {
        #region Private Fields
        private readonly IUsersRepository _userRepository;

        #endregion

        #region Constructors
        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region User CRUD Methods
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

        #endregion

        #region User Like Methods
        public async Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            bool isValidVideo = await HttpUtils.ValidateVideoAsync(likedVideoId).ConfigureAwait(false);
            if (!isValidVideo)
            {
                throw new ArgumentException("Invalid video.");
            }

            await _userRepository.AddToLikedVideoAsync(userId, likedVideoId).ConfigureAwait(false);
        }
        public async Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            bool isValidVideo = await HttpUtils.ValidateVideoAsync(likedVideoId).ConfigureAwait(false);
            if (!isValidVideo)
            {
                throw new ArgumentException("Invalid video.");
            }

            await _userRepository.RemoveFromLikedVideoAsync(userId, likedVideoId).ConfigureAwait(false);
        }

        #endregion

        #region User Subscription Methods
        public async Task SubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            // Check if the first user exists
            User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                throw new ArgumentException("User does not exist."); // User not found
            }

            // Check if the user to be subscribed to exists
            User? subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId).ConfigureAwait(false);

            if (subscribeUser == null)
            {
                throw new ArgumentException("User you want to subscribe to does not exist."); // Target user not found
            }

            // If both users exist, call the repository to persist the subscription
            await _userRepository.AddUserToSubscriptionAsync(userId, subscribeUserId).ConfigureAwait(false);
        }
        public async Task UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            // Check if both users exist
            User? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            User? subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId).ConfigureAwait(false);
            if (subscribeUser == null)
            {
                throw new ArgumentException("User to unsubscribe from does not exist.");
            }

            // Call the repository to remove the subscription
            await _userRepository.RemoveUserFromSubscriptionAsync(userId, subscribeUserId).ConfigureAwait(false);
        }

        #endregion

        #region User Image Methods
        public Task DeleteUserImageAsync(Guid userId, string saveDirectory)
        {
            throw new NotImplementedException();
        }
        public Task SaveUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUserImageAsync(Guid userId, IFormFile imageFile, string saveDirectory)
        {
            throw new NotImplementedException();
        }
        public Task<string> GetUserImageAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        

        #endregion
    }
}