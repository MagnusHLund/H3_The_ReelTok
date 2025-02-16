using System.Text;
using System.Security.Claims;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Interfaces;
using System.Security.Cryptography;
using reeltok.api.auth.ValueObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace reeltok.api.auth.Services
{
    public class TokensService : ITokensService
    {
        private const string SecretKeyConfig = "JWTSettings:SecretKey";
        private const string AudienceConfig = "JWTSettings:Audience";
        private const string IssuerConfig = "JWTSettings:Issuer";

        private readonly AppSettingsUtils _appSettingsUtils;
        private readonly ITokensRepository _tokensRepository;

        public TokensService(AppSettingsUtils appSettingsUtils, ITokensRepository tokensRepository)
        {
            _appSettingsUtils = appSettingsUtils;
            _tokensRepository = tokensRepository;
        }

        public async Task<AccessToken> GenerateAccessToken(Guid userId)
        {
            string secretKey = _appSettingsUtils.GetConfigurationValue(SecretKeyConfig);
            string audience = _appSettingsUtils.GetConfigurationValue(AudienceConfig);
            string issuer = _appSettingsUtils.GetConfigurationValue(IssuerConfig);

            byte[] encodedSecretKey = Encoding.UTF8.GetBytes(secretKey);

            SymmetricSecurityKey key = new SymmetricSecurityKey(encodedSecretKey);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            uint createdAt = DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow);
            uint oneHourInSeconds = 3600;
            uint expiresAt = createdAt + oneHourInSeconds;

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTimeUtils.UnixTimeToDateTime(expiresAt),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            AccessToken accessToken = new AccessToken(
                tokenValue: tokenHandler.WriteToken(token),
                createdAt: createdAt,
                expiresAt: expiresAt
            );

            AccessTokenEntity accessTokenEntity = new AccessTokenEntity
            {
                UserId = userId,
                Token = accessToken
            };

            AccessTokenEntity accessTokenDatabaseResult = await _tokensRepository.SaveToken<AccessTokenEntity, AccessToken>(accessTokenEntity).ConfigureAwait(false);
            return accessTokenDatabaseResult.Token;
        }

        public ClaimsPrincipal DecodeAccessToken(string accessTokenValue)
        {
            string secretKey = _appSettingsUtils.GetConfigurationValue(SecretKeyConfig);
            byte[] encodedSecretKey = Encoding.UTF8.GetBytes(secretKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(encodedSecretKey),
                ValidateIssuer = false, // Specify true and add valid issuer if needed
                ValidateAudience = false, // Specify true and add valid audience if needed
                ValidateLifetime = true, // Validate token lifetime
                ClockSkew = TimeSpan.Zero // Reduce clock skew
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(accessTokenValue, validationParameters, out SecurityToken validatedToken);

            // TODO: Check database, to ensure the token is not invalidated!

            return principal;
        }

        public async Task<RefreshToken> GenerateRefreshToken(Guid userId)
        {
            byte[] randomBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            string tokenString = Convert.ToBase64String(randomBytes);

            uint createdAt = DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow);
            uint sevenDaysInSeconds = 604800;
            uint expiresAt = createdAt + sevenDaysInSeconds;

            RefreshToken refreshToken = new RefreshToken(
                tokenValue: tokenString,
                createdAt: createdAt,
                expiresAt: expiresAt
            );

            RefreshTokenEntity RefreshTokenEntity = new RefreshTokenEntity
            {
                UserId = userId,
                Token = refreshToken
            };

            RefreshTokenEntity refreshTokenDatabaseResult = await _tokensRepository.SaveToken<RefreshTokenEntity, RefreshToken>(RefreshTokenEntity).ConfigureAwait(false);
            return refreshTokenDatabaseResult.Token;
        }

        public async Task<Guid> GetUserIdByRefreshToken(string refreshTokenValue)
        {
            Guid userId = await _tokensRepository.GetUserIdByRefreshToken(refreshTokenValue).ConfigureAwait(false);
            return userId;
        }

        public async Task RevokeTokens(string accessTokenValue, string refreshTokenValue)
        {
            await _tokensRepository.RevokeToken<AccessTokenEntity, AccessToken>(accessTokenValue).ConfigureAwait(false);
            await _tokensRepository.RevokeToken<RefreshTokenEntity, RefreshToken>(refreshTokenValue).ConfigureAwait(false);
        }
    }
}
