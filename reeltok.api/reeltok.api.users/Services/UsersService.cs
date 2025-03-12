using reeltok.api.users.utils;
using reeltok.api.users.Entities;
using reeltok.api.users.factories;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IExternalApiService _externalApiService;
        private readonly IStorageService _storageService;
        private readonly ISubscriptionsRepository _subscriptions;

        public UsersService(
            IUsersRepository userRepository,
            IExternalApiService externalApiService,
            IStorageService storageService,
            ISubscriptionsRepository subscriptionsRepository
        )
        {
            _userRepository = userRepository;
            _externalApiService = externalApiService;
            _storageService = storageService;
            _subscriptions = subscriptionsRepository;
        }

        public async Task<UserEntity> CreateUserAsync(string username, string email, string password, byte interests)
        {
            if (!ValidationUtils.IsValidEmail(email) || !ValidationUtils.IsValidUsername(username))
            {
                throw new ArgumentException("User attempted to sign up, with invalid username or email!");
            }

            UserEntity userToCreate = UsersFactory.CreateUserEntity(username, email);
            UserEntity createdUser = await _userRepository.CreateUserAsync(userToCreate).ConfigureAwait(false);

            try
            {
                await _externalApiService.CreateUserInAuthApiAsync(createdUser.UserId, password)
                    .ConfigureAwait(false);

                await _externalApiService.CreateUserInRecommendationsApiAsync(createdUser.UserId, interests)
                    .ConfigureAwait(false);
            }
            catch
            {
                await DeleteUserAsync(createdUser.UserId).ConfigureAwait(false);
                throw;
            }

            return createdUser;
        }

        public async Task<UserWithSubscriptionCounts> GetUserByIdAsync(Guid userId)
        {
            ExternalUserEntity user = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

            int totalSubscriber = await _subscriptions.GetSubscribersCountAsync(userId).ConfigureAwait(false);
            int totalSubscription = await _subscriptions.GetSubscriptionsCountAsync(userId).ConfigureAwait(false);

            return new UserWithSubscriptionCounts(user, totalSubscriber, totalSubscription);
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

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            UserEntity user = await _userRepository.GetUserByEmailAsync(email).ConfigureAwait(false);
            return user;
        }

        public async Task<UserEntity> UpdateUserProfilePictureAsync(IFormFile imageFile, Guid userId)
        {
            bool isValidImage = await ImageUtils.IsValidImage(imageFile).ConfigureAwait(false);

            if (!isValidImage)
            {
                throw new ArgumentException($"The provided file is not a valid image. Uploaded by userId {userId}");
            }

            string profilePictureUrl = await _storageService.UploadProfilePictureToFileServerAsync(imageFile, userId)
                .ConfigureAwait(false);

            UserEntity userToUpdate = await _userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);
            userToUpdate = UsersFactory.UpdateUserEntityProfilePictureUrlPath(userToUpdate, profilePictureUrl);

            UserEntity updatedUser = await _userRepository.UpdateUserAsync(userToUpdate).ConfigureAwait(false);
            return updatedUser;
        }

        private async Task DeleteUserAsync(Guid userId)
        {
            await _userRepository.DeleteUserAsync(userId).ConfigureAwait(false);
        }
    }
}
