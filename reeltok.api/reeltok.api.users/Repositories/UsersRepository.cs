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
        public async Task<User> CreateUserAsync(User user)
        {
            User DbUser = (await _context.Users.AddAsync(user).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return DbUser;
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id).ConfigureAwait(false);
        }
        public Task<string> GetUserImageAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public async Task<User?> UpdateUserAsync(User user, Guid userId)
        {
            User? existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId).ConfigureAwait(false);

            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            User updateUser = _context.Users.Update(user).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return updateUser;
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            User? userModel = await _context.Users.FindAsync(userId).ConfigureAwait(false);

            if (userModel == null)
            {
                return false;
            }

            _context.Users.Remove(userModel);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
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