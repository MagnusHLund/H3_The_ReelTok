using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            throw new NotImplementedException();
        }

        public async Task<Guid> GetUserIdByToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Tokens> LoginUser(LoginCredentials loginCredentials)
        {
            throw new NotImplementedException();
        }

        public async Task LogoutUser(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Auth?> GetAuthByUserId(Guid userId)
        {
            var auth =  await _context.FindAsync<Auth>(userId);
            return auth;
        }

        public async Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(Auth authInfo)
        {
            await _context.AddAsync<Auth>(authInfo);
        }
    }
}
