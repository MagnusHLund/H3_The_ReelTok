using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Data;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Interfaces;

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
            UserAuthentication userToDelete = await _context.Auths.Where(e => e.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            _context.Remove<UserAuthentication>(userToDelete);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Guid> GetUserIdByToken(string refreshToken)
        {
            RefreshToken userRefreshToken = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find the refresh token in the database!");

            return userRefreshToken.UserId;
        }

        public async Task LogoutUser(string refreshToken)
        {
            // TODO: Shouldn't it also invalidate JWTs? This is only refresh tokens...

            RefreshToken refreshTokenToInvalidate = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the refresh token in the database!");

            _context.Remove<RefreshToken>(refreshTokenToInvalidate);
            await _context.SaveChangesAsync();
        }

        public async Task<UserAuthentication> GetUserAuthenticationByUserId(Guid userId)
        {
            UserAuthentication userCredentials = await _context.Auths.Where(a => a.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find user: {userId} in the database!");

            return userCredentials;
        }

        public async Task<bool> DoesUserExist(Guid userId) {
            bool userExists = await _context.Auths.AnyAsync(a => a.UserId == userId).ConfigureAwait(false);
            return userExists;
        }

        public async Task<RefreshToken> RefreshAccessToken(string refreshToken)
        {
            // We're using this to check the expiry date so we can ensure that our token is still valid
            RefreshToken refreshTokenToCheck = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the refresh token in the database!");

            return refreshTokenToCheck;
        }

        public async Task CreateUser(UserAuthentication authInfo)
        {
            await _context.AddAsync<UserAuthentication>(authInfo);
            await _context.SaveChangesAsync();
        }
    }
}
