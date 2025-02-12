using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using System.Security.Authentication;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Utils;
using System.Security.Claims;

namespace reeltok.api.auth.Services
{
    public class AuthService : IAuthService
    {
        // TODO: Maybe implement some session cache for tokens?
         private readonly IAuthRepository _authRepository;
         private readonly ITokensService _tokensService;

        public AuthService(IAuthRepository authRepository, ITokensService tokensService)
        {
            _authRepository = authRepository;
            _tokensService = tokensService;
        }

        public async Task DeleteUser(Guid userId)
        {
           await _authRepository.DeleteUser(userId).ConfigureAwait(false);
        }

        public Guid GetUserIdByToken(string accessTokenValue)
        {
            ClaimsPrincipal decodedAccessToken = _tokensService.DecodeAccessToken(accessTokenValue);
            string? stringUserId = decodedAccessToken?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!Guid.TryParse(stringUserId, out Guid userId))
            {
                throw new FormatException("Invalid UserId!");
            }

            return userId;
        }

        public async Task<Tokens> LoginUser(LoginCredentials loginCredentials)
        {
            UserCredentialsEntity existingAuth = await _authRepository.GetUserAuthenticationByUserId(loginCredentials.UserId).ConfigureAwait(false);

            bool isPasswordValid = PasswordUtils.VerifyPassword(loginCredentials.PlainTextPassword, existingAuth.HashedPassword, existingAuth.Salt);

            if (!isPasswordValid)
            {
              throw new InvalidCredentialException("Invalid credentials!");
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

            if (refreshTokenToCheck.ExpireDate < DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow))
            {
                throw new SecurityTokenExpiredException();
            }

            AccessToken accessToken = _tokensService.GenerateAccessToken(refreshTokenToCheck.UserId);

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

            UserCredentialsEntity userCredentials = new UserCredentialsEntity(CreateDetails.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);
            await _authRepository.CreateUser(userCredentials).ConfigureAwait(false);

            Tokens tokens = GenerateTokens(userCredentials.UserId);
            return tokens;
        }

        private Tokens GenerateTokens(Guid userId) // TODO: Remove this method
        {
            AccessToken accessToken = _tokensService.GenerateAccessToken(userId);
            RefreshToken refreshToken = _tokensService.GenerateRefreshToken(userId);

            return new Tokens(accessToken, refreshToken);
        }
    }
}
