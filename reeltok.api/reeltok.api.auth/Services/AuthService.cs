using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using reeltok.api.auth.Exceptions;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Entites;
using reeltok.api.auth.Utils;

namespace reeltok.api.auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
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
            var existingAuth = await _authRepository.GetAuthByUserId(loginCredentials.UserId);
           
            if (existingAuth == null)
            {
              throw new UserDoesNotExistException(); 
            }
            
            bool isPasswordValid = PasswordUtils.VerifyPassword(loginCredentials.PlainTextPassword, existingAuth.HashedPassword, existingAuth.Salt);

            if (!isPasswordValid)
            {
              throw new InvalidCredentialException(); 
            }
            
            string secretKey = _configuration["JWTSettings:SecretKey"];
            string issuer = _configuration["JWTSettings:Issuer"];
            string audience = _configuration["JWTSettings:Audience"];

            AccessToken accessToken = GenerateTokenUtility.GenerateAccessToken(existingAuth.UserId, secretKey, issuer, audience);
            RefreshToken refreshToken = GenerateTokenUtility.GenerateRefreshToken(existingAuth.UserId);

            return new Tokens(accessToken, refreshToken);

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
              throw new UserAlreadyExistsException("User already exists.");
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
