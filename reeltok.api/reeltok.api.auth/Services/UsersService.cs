using reeltok.api.auth.Utils;
using System.Security.Claims;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Factories;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Services;
using System.ComponentModel.DataAnnotations;
using reeltok.api.auth.Interfaces.Repositories;

namespace reeltok.api.auth.Services
{
    public class UsersService : IUsersService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenGenerationService _tokenGenerationService;
        private readonly ITokenValidationService _tokenValidationService;

        public UsersService(
            IAuthRepository authRepository,
            ITokenGenerationService tokenGenerationService,
            ITokenValidationService tokenValidationService
        )
        {
            _authRepository = authRepository;
            _tokenGenerationService = tokenGenerationService;
            _tokenValidationService = tokenValidationService;
        }

        public Guid GetUserIdByAccessTokenAsync(string accessTokenValue)
        {
            ClaimsPrincipal decodedAccessToken = _tokenValidationService.DecodeAccessToken(accessTokenValue);
            string? stringUserId = decodedAccessToken?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(stringUserId, out Guid userId))
            {
                throw new FormatException("Invalid UserId!");
            }

            return userId;
        }


        public async Task<Tokens> SignUpAsync(Credentials userCredentials)
        {
            bool userExists = await _authRepository.DoesUserExist(userCredentials.UserId).ConfigureAwait(false);

            if (userExists)
            {
                throw new InvalidOperationException("User already exists!");
            }

            if (!PasswordUtils.IsValid(userCredentials.PlainTextPassword))
            {
                throw new ValidationException("Password does not follow the minimum requirements!");
            }

            HashedPasswordDetails hashedPasswordDetails = PasswordUtils.HashPassword(userCredentials.PlainTextPassword);

            UserCredentialsEntity userCredentialsEntity = CredentialFactory
                .CreateUserCredentialsEntity(userCredentials.UserId, hashedPasswordDetails);

            await _authRepository.CreateUser(userCredentialsEntity).ConfigureAwait(false);

            AccessToken accessToken = await _tokenGenerationService.GenerateAccessToken(userCredentialsEntity.UserId)
                .ConfigureAwait(false);

            RefreshToken refreshToken = await _tokenGenerationService.GenerateRefreshToken(userCredentialsEntity.UserId)
                .ConfigureAwait(false);

            return new Tokens(
                accessToken: accessToken,
                refreshToken: refreshToken
            );
        }

        public async Task DeleteUser(Guid userId)
        {
            await _authRepository.DeleteUser(userId).ConfigureAwait(false);
        }
    }
}