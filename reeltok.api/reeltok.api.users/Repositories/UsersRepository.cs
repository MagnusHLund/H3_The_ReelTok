using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        #region Fields
        private readonly UserDBContext _context;

        #endregion

        #region Constructor
        public UsersRepository(UserDBContext context)
        {
            _context = context;
        }

        #endregion

        #region User Methods
        public async Task<UserProfileData> CreateUserAsync(UserProfileData user)
        {
            UserProfileData DbUser = (await _context.Users.AddAsync(user)).Entity;
            await _context.SaveChangesAsync();
            return DbUser;
        }
        public Task<UserProfileData?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<string> GetUserImageAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateUserAsync(UserProfileData user, Guid userId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region User Like Methods
        public Task<List<Guid>> GetAllLikedVideoIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public Task AddToLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }
        public Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region User Subscription Methods
        public Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }
        public Task RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guid>> GetAllSubscribersIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Guid>> GetAllSubscriptionIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}