using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public Task DeleteUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetUserIdByToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Tokens> LoginUser(LoginCredentials loginCredentials)
        {
            throw new NotImplementedException();
        }

        public Task LogOutUser(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RegisterUser(Guid userId, string plainTextPassword)
        {
            throw new NotImplementedException();
        }
    }
}
