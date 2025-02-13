using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UserDBContext _context;
        public UsersRepository(UserDBContext context)
        {
            _context = context;
        }

        public Task AddToLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public Task AddUserToSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync(UserDetails user)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileData?> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromLikedVideoAsync(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserFromSubscriptionAsync(Guid userId, Guid subscriptionUserId)
        {
            throw new NotImplementedException();
        }
    }
}