using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Entites;

namespace reeltok.api.auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

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

        public Task LogoutUser(string refreshToken)
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
