using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using System.Security.Authentication;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Interfaces.Repositories;

namespace reeltok.api.auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenGenerationService _tokenGenerationService;
        private readonly ITokenManagementService _tokenManagementService;

        public AuthenticationService(
            IAuthRepository authRepository,
            ITokenGenerationService tokenGenerationService,
            ITokenManagementService tokenManagementService
        )
        {
            _authRepository = authRepository;
            _tokenGenerationService = tokenGenerationService;
            _tokenManagementService = tokenManagementService;
        }

        public async Task<Tokens> LoginUserAsync(Credentials loginCredentials)
        {
            UserCredentialsEntity existingUser = await _authRepository.GetUserCredentialsByUserId(loginCredentials.UserId).ConfigureAwait(false);

            bool isPasswordValid = PasswordUtils
                .VerifyPassword(loginCredentials.PlainTextPassword, existingUser.HashedPasswordDetails);

            if (!isPasswordValid)
            {
                throw new InvalidCredentialException("Invalid credentials!");
            }

            AccessToken accessToken = await _tokenGenerationService.GenerateAccessToken(existingUser.UserId)
                .ConfigureAwait(false);

            RefreshToken refreshToken = await _tokenGenerationService.GenerateRefreshToken(existingUser.UserId)
                .ConfigureAwait(false);

            return new Tokens(
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        public async Task LogoutUserAsync(string accessTokenValue, string refreshTokenValue)
        {
            await _tokenManagementService.RevokeTokens(accessTokenValue, refreshTokenValue).ConfigureAwait(false);
        }
    }
}
