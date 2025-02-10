using Microsoft.EntityFrameworkCore;
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
        public async Task<Users> CreateUserAsync(Users user)
        {
            Users DbUser = (await _context.Users.AddAsync(user).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return DbUser;
        }
        public async Task<Users?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id).ConfigureAwait(false);
        }
        public Task<string> GetUserImageAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public async Task<Users?> UpdateUserAsync(Users user, Guid userId)
        {
            Users? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId).ConfigureAwait(false);

            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            Users updateUser = _context.Users.Update(user).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return updateUser;
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