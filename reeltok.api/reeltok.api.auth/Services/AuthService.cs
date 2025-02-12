using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Utils;
using Microsoft.IdentityModel.Tokens;

namespace reeltok.api.auth.Services
{
    public class AuthService : IAuthService
    {
        // TODO: Maybe implement some session cache for tokens?
         private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task DeleteUser(Guid userId)
        {
           await _authRepository.DeleteUser(userId);
        }

        public async Task<Guid> GetUserIdByToken(string refreshToken)
        {
            Guid userId = await _authRepository.GetUserIdByToken(refreshToken);

            return userId;
        }

        public async Task<Tokens> LoginUser(LoginCredentials loginCredentials)
        {
            UserAuthentication existingAuth = await _authRepository.GetUserAuthenticationByUserId(loginCredentials.UserId).ConfigureAwait(false);

            bool isPasswordValid = PasswordUtils.VerifyPassword(loginCredentials.PlainTextPassword, existingAuth.HashedPassword, existingAuth.Salt);

            if (!isPasswordValid)
            {
              throw new InvalidCredentialException();
            }

            Tokens tokens = GenerateTokens(existingAuth.UserId);
            return tokens;
        }

        public async Task LogoutUser(string refreshToken)
        {
           await _authRepository.LogoutUser(refreshToken);
        }

        // TODO: Rewrite this method
        public async Task<AccessToken> RefreshAccessToken(string refreshToken)
        {
            RefreshToken refreshTokenToCheck = await _authRepository.RefreshAccessToken(refreshToken);

            if (refreshTokenToCheck.ExpireDate < DateTime.UtcNow)
            {
                throw new SecurityTokenExpiredException();
            }

            AccessToken accessToken = GenerateTokenUtils.GenerateAccessToken(refreshTokenToCheck.UserId);

            return accessToken;
        }

        public async Task<Tokens> CreateUser(CreateDetails CreateDetails)
        {
            bool userExists = await _authRepository.DoesUserExist(CreateDetails.UserId).ConfigureAwait(false);

            if (userExists) {
                throw new InvalidOperationException("User already exists!");
            }

            if (!PasswordUtils.IsValid(CreateDetails.PlainTextPassword))
            {
                throw new ValidationException("Password does not follow the minimum requirements!");
            }

            HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword(CreateDetails.PlainTextPassword);

            UserAuthentication authInfo = new UserAuthentication(CreateDetails.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);
            await _authRepository.CreateUser(authInfo);

            Tokens tokens = GenerateTokens(authInfo.UserId);
            return tokens;
        }

        private Tokens GenerateTokens(Guid userId)
        {
            AccessToken accessToken = GenerateTokenUtils.GenerateAccessToken(userId);
            RefreshToken refreshToken = GenerateTokenUtils.GenerateRefreshToken(userId);

            return new Tokens(accessToken, refreshToken);
        }
    }
}
