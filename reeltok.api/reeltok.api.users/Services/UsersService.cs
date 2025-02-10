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
        public async Task<Users> CreateUserAsync(Users user)
        {
            Users returnUser;

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
        public async Task<Users?> GetUserByIdAsync(Guid userId)
        {
            Users? user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            return user;
        }
        public async Task<Users?> UpdateUserAsync(Users user, Guid userId)
        {
            Users? updatedUser = await _userRepository.UpdateUserAsync(user, userId).ConfigureAwait(false);

            if (updatedUser == null)
            {
                return null;
            }

            return updatedUser;
        }

        #endregion

        #region User Like Methods
        public async Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
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
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
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
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            if (user == null)
            {
                throw new ArgumentException("User does not exist."); // User not found
            }

            // Check if the user to be subscribed to exists
            var subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId).ConfigureAwait(false);

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
            var user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            var subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId).ConfigureAwait(false);
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