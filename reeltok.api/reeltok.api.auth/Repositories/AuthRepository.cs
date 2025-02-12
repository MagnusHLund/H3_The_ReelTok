using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Data;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task DeleteUser(Guid userId)
        {
            UserCredentialsEntity userToDelete = await _context.UserCredentials.Where(e => e.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            _context.Remove<UserCredentialsEntity>(userToDelete);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task LogoutUser(string refreshToken)
        {
            // TODO: Shouldn't it also invalidate JWTs? This is only refresh tokens... Maybe change method to "InvalidateTokens"?

            RefreshToken refreshTokenToInvalidate = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the refresh token in the database!");

            _context.Remove<RefreshToken>(refreshTokenToInvalidate);
            await _context.SaveChangesAsync();
        }

        public async Task<UserCredentialsEntity> GetUserAuthenticationByUserId(Guid userId)
        {
            UserCredentialsEntity userCredentials = await _context.UserCredentials.Where(a => a.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            return userCredentials;
        }

        public async Task<bool> DoesUserExist(Guid userId) {
            bool userExists = await _context.UserCredentials.AnyAsync(a => a.UserId == userId).ConfigureAwait(false);
            return userExists;
        }

        public async Task<RefreshToken> RefreshAccessToken(string refreshToken)
        {
            // We're using this to check the expiry date so we can ensure that our token is still valid
            RefreshToken refreshTokenToCheck = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the refresh token in the database!");

            return refreshTokenToCheck;
        }

        public async Task<UserCredentialsEntity> CreateUser(UserCredentialsEntity userCredentials)
        {
            UserCredentialsEntity userCredentialsDatabaseResult = await _context.AddAsync(userCredentials).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            return userCredentialsDatabaseResult;
        }
    }
}
