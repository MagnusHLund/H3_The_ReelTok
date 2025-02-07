using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Entites;
using reeltok.api.auth.Utils;

namespace reeltok.api.auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
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

        public async Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterUser(RegisterDetails registerDetails)
        {
            var existingAuth = await _authRepository.GetAuthByUserId(registerDetails.UserId);
            if (existingAuth != null)
            {
              throw new ArgumentException("User already exists.");
            }

            if (PasswordUtils.IsValid(registerDetails.PlainTextPassword) == false)
            {
              throw new ValidationException(); 
            }

            var (hashedPassword, salt) = PasswordUtils.HashPassword(registerDetails.PlainTextPassword);

            Auth authInfo = new Auth(registerDetails.UserId, hashedPassword, salt);
            
            await _authRepository.RegisterUser(authInfo);
        }
    }
}
