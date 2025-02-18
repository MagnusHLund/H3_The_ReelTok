using System.Security.Claims;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Interfaces;
using reeltok.api.auth.ValueObjects;
using System.Security.Authentication;
using System.ComponentModel.DataAnnotations;

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

            if (!Guid.TryParse(stringUserId, out Guid userId))
            {
                throw new FormatException("Invalid UserId!");
            }

            return userId;
        }

        public async Task<Tokens> LoginUser(LoginCredentials loginCredentials)
        {
            UserCredentialsEntity existingUser = await _authRepository.GetUserCredentialsByUserId(loginCredentials.UserId).ConfigureAwait(false);

            bool isPasswordValid = PasswordUtils.VerifyPassword(loginCredentials.PlainTextPassword, existingUser.HashedPassword, existingUser.Salt);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialException("Invalid credentials!");
            }

            AccessToken accessToken = await _tokensService.GenerateAccessToken(existingUser.UserId).ConfigureAwait(false);
            RefreshToken refreshToken = await _tokensService.GenerateRefreshToken(existingUser.UserId).ConfigureAwait(false);

            return new Tokens(
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        public async Task LogoutUser(string accessTokenValue, string refreshTokenValue)
        {
            await _tokensService.RevokeTokens(accessTokenValue, refreshTokenValue).ConfigureAwait(false);
        }

        public async Task<Tokens> CreateUser(CreateDetails CreateDetails)
        {
            bool userExists = await _authRepository.DoesUserExist(CreateDetails.UserId).ConfigureAwait(false);

            if (userExists)
            {
                throw new InvalidOperationException("User already exists!");
            }

            if (!PasswordUtils.IsValid(CreateDetails.PlainTextPassword))
            {
                throw new ValidationException("Password does not follow the minimum requirements!");
            }

            HashedPasswordData hashedPasswordData = PasswordUtils.HashPassword(CreateDetails.PlainTextPassword);

            UserCredentialsEntity userCredentials = new UserCredentialsEntity(CreateDetails.UserId, hashedPasswordData.Password, hashedPasswordData.Salt);
            await _authRepository.CreateUser(userCredentials).ConfigureAwait(false);

            AccessToken accessToken = await _tokensService.GenerateAccessToken(userCredentials.UserId).ConfigureAwait(false);
            RefreshToken refreshToken = await _tokensService.GenerateRefreshToken(userCredentials.UserId).ConfigureAwait(false);

            return new Tokens(
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }
    }
}
