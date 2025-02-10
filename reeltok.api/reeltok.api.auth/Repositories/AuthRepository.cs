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
            Auth? userToDelete = await _context.Auths.Where(e => e.UserId == userId).FirstOrDefaultAsync();

            _context.Remove<Auth>(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetUserIdByToken(string refreshToken)
        {
            RefreshToken? userRefreshToken = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();

            return userRefreshToken.UserId;
        }

        public async Task LogoutUser(string refreshToken)
        {
            RefreshToken? refreshTokenToInvalidate = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();

            _context.Remove<RefreshToken>(refreshTokenToInvalidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Auth?> GetAuthByUserId(Guid userId)
        {
            Auth? auth = await _context.Auths.Where(a => a.UserId == userId).FirstOrDefaultAsync().ConfigureAwait(false);
            return auth;
        }

        public async Task<RefreshToken> RefreshAccessToken(string refreshToken)
        {
            // We're using this to check the expiry date so we can assure that our token is still valid
            RefreshToken? refreshTokenToCheck = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();

            return refreshTokenToCheck;
        }

        public async Task CreateUser(Auth authInfo)
        {
            await _context.AddAsync<Auth>(authInfo);
        }
    }
}
