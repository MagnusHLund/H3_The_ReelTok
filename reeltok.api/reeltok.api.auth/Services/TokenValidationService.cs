using System.Text;
using System.Security.Claims;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Interfaces.Repositories;

namespace reeltok.api.auth.Services
{
    public class TokenValidationService : ITokenValidationService
    {
        private const string SecretKeyConfig = "JWTSettings:SecretKey";

        private readonly AppSettingsUtils _appSettingsUtils;
        private readonly ITokensRepository _tokensRepository;

        public TokenValidationService(AppSettingsUtils appSettingsUtils, ITokensRepository tokensRepository)
        {
            _appSettingsUtils = appSettingsUtils;
            _tokensRepository = tokensRepository;
        }

        public async Task<bool> IsValidAccessToken(string? accessTokenValue)
        {
            if (string.IsNullOrEmpty(accessTokenValue))
            {
                return false;
            }

            var decodedToken = DecodeAccessToken(accessTokenValue);
            var userId = decodedToken?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            bool isRevoked = await IsAccessTokenRevokedAsync(accessTokenValue).ConfigureAwait(false);
            return !isRevoked;
        }

        public ClaimsPrincipal DecodeAccessToken(string accessTokenValue)
        {
            string secretKey = _appSettingsUtils.GetConfigurationValue(SecretKeyConfig);
            byte[] encodedSecretKey = Encoding.UTF8.GetBytes(secretKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(encodedSecretKey),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler
                .ValidateToken(accessTokenValue, validationParameters, out SecurityToken validatedToken);

            return principal;
        }

        private async Task<bool> IsAccessTokenRevokedAsync(string accessTokenValue)
        {
            bool isRevoked = await _tokensRepository.IsAccessTokenRevokedAsync(accessTokenValue)
                .ConfigureAwait(false);

            return isRevoked;
        }
    }

}