using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;

namespace reeltok.api.users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(UserProfileData user)
        {
            if (user == null)
                throw new ArgumentException("User profile data cannot be null");

            if (user.Details == null)
                throw new ArgumentException("User details cannot be null");

            if (string.IsNullOrWhiteSpace(user.Details.UserName) ||
                string.IsNullOrWhiteSpace(user.Details.ProfileUrl) ||
                string.IsNullOrWhiteSpace(user.Details.ProfilePictureUrl) ||
                string.IsNullOrWhiteSpace(user.Details.HiddenDetails.Email))
            {
                throw new ArgumentException("User details contain invalid or missing values");
            }

            // If all validations pass, call the repository
            await _userRepository.CreateUserAsync(user.Details);
        }


        public async Task<UserProfileData?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found");  // Or a custom exception like NotFoundException
            }

            return user;
        }

        public Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            throw new NotImplementedException();
        }

        public async Task SubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            // Check if the first user exists
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User does not exist."); // User not found
            }

            // Check if the user to be subscribed to exists
            var subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId);

            if (subscribeUser == null)
            {
                throw new ArgumentException("User you want to subscribe to does not exist."); // Target user not found
            }

            // If both users exist, call the repository to persist the subscription
            await _userRepository.AddUserToSubscriptionAsync(userId, subscribeUserId);
        }

        public async Task UnsubscribeAsync(Guid userId, Guid subscribeUserId)
        {
            // Check if both users exist
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            var subscribeUser = await _userRepository.GetUserByIdAsync(subscribeUserId);
            if (subscribeUser == null)
            {
                throw new ArgumentException("User to unsubscribe from does not exist.");
            }

            // Call the repository to remove the subscription
            await _userRepository.RemoveUserFromSubscriptionAsync(userId, subscribeUserId);
        }


        public Task UpdateUserAsync(UserProfileData user)
        {
            throw new NotImplementedException();
        }
    }
}