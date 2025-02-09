using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Entites;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Data;

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
            var userToDelete = await _context.Auths.Where(e => e.UserId == userId).FirstOrDefaultAsync();

            _context.Remove<Auth>(userToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> GetUserIdByToken(string refreshToken)
        {
            var userRefreshToken = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();
            
            return userRefreshToken.UserId;
        }

        public async Task LogoutUser(string refreshToken)
        {
            var refreshTokenToInvalidate = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();

            _context.Remove<RefreshToken>(refreshTokenToInvalidate);
            await _context.SaveChangesAsync(); 
        }

        public async Task<Auth?> GetAuthByUserId(Guid userId)
        {
            var auth = await _context.FindAsync<Auth>(userId);
            return auth;
        }

        public async Task<RefreshToken> RefreshAccessToken(string refreshToken)
        {
            // We're using this to check the expiry date so we can assure that our token is still valid
            var refreshTokenToCheck = await _context.RefreshTokens.Where(e => e.Token == refreshToken).FirstOrDefaultAsync();
            
            return refreshTokenToCheck;
        }

        public async Task RegisterUser(Auth authInfo)
        {
            await _context.AddAsync<Auth>(authInfo);
        }
    }
}
