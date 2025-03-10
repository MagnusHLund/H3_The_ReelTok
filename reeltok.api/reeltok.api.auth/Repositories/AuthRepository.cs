using reeltok.api.auth.Data;
using reeltok.api.auth.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Interfaces.Repositories;

namespace reeltok.api.auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<UserCredentialsEntity> CreateUser(UserCredentialsEntity userCredentials)
        {
            UserCredentialsEntity userCredentialsDatabaseResult = (await _context.AddAsync(userCredentials)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return userCredentialsDatabaseResult;
        }

        public async Task DeleteUser(Guid userId)
        {
            UserCredentialsEntity userToDelete = await _context.UserCredentials.Where(e => e.UserId == userId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            _context.Remove<UserCredentialsEntity>(userToDelete);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<UserCredentialsEntity> GetUserCredentialsByUserId(Guid userId)
        {
            UserCredentialsEntity userCredentials = await _context.UserCredentials.Where(a => a.UserId == userId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            return userCredentials;
        }

        public async Task<bool> DoesUserExist(Guid userId)
        {
            bool userExists = await _context.UserCredentials.AnyAsync(a => a.UserId == userId)
                .ConfigureAwait(false);

            return userExists;
        }
    }
}
