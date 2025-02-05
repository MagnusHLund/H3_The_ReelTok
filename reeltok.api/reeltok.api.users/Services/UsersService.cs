using System.Web;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.Utils;

namespace reeltok.api.users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddToLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            bool isValidVideo = await HttpUtils.ValidateVideoAsync(likedVideoId);
            if (!isValidVideo)
            {
                throw new ArgumentException("Invalid video.");
            }

            await _userRepository.AddToLikedVideoAsync(userId, likedVideoId);
        }

        public async Task CreateAsync(UserProfileData user, Guid userId)
        {
            // if (user == null)
            //     throw new ArgumentException("User profile data cannot be null");

            // if (user.Details == null)
            //     throw new ArgumentException("User details cannot be null");

            // if (string.IsNullOrWhiteSpace(user.Details.UserName) ||
            //     string.IsNullOrWhiteSpace(user.Details.ProfileUrl) ||
            //     string.IsNullOrWhiteSpace(user.Details.ProfilePictureUrl) ||
            //     string.IsNullOrWhiteSpace(user.Details.HiddenDetails.Email))
            // {
            //     throw new ArgumentException("User details contain invalid or missing values");
            // }

            // Try to create the user
            try
            {
                await _userRepository.CreateUserAsync(user.Details);
            }
            catch (Exception ex)
            {
                // Handle the error, you can log it or throw a custom exception if needed
                throw new InvalidOperationException("User creation failed.", ex);
            }

            // Optionally, check if the user now exists in the repository to confirm successful creation
            var createdUser = await _userRepository.GetUserByIdAsync(userId);

            if (createdUser == null)
            {
                throw new InvalidOperationException("User was not created successfully.");
            }
        }


        public async Task<UserProfileData?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task RemoveFromLikedVideosAsync(Guid userId, Guid likedVideoId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            bool isValidVideo = await HttpUtils.ValidateVideoAsync(likedVideoId);
            if (!isValidVideo)
            {
                throw new ArgumentException("Invalid video.");
            }

            await _userRepository.RemoveFromLikedVideoAsync(userId, likedVideoId);
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