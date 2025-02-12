using System.Text;
using System.Security.Claims;
using reeltok.api.auth.Utils;
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

        internal TokensService(AppSettingsUtils appSettingsUtils)
        {
            _appSettingsUtils = appSettingsUtils;
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            byte[] randomBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            string tokenString = Convert.ToBase64String(randomBytes);
            DateTime createDate = DateTime.UtcNow;
            DateTime expireDate = createDate.AddDays(7);

            return new RefreshToken(
                userId: userId,
                tokenValue: tokenString,
                createDate: createDate,
                expireDate: expireDate
            );
        }

        public AccessToken GenerateAccessToken(Guid userId)
        {
            string secretKey = _appSettingsUtils.GetConfigurationValue(SecretKeyConfig);
            string audience = _appSettingsUtils.GetConfigurationValue(AudienceConfig);
            string issuer = _appSettingsUtils.GetConfigurationValue(IssuerConfig);

            byte[] encodedSecretKey = Encoding.UTF8.GetBytes(secretKey);

            SymmetricSecurityKey key = new SymmetricSecurityKey(encodedSecretKey);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime createDate = DateTime.UtcNow;
            DateTime expireDate = createDate.AddHours(1);

            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expireDate,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessToken(
                token: tokenHandler.WriteToken(token),
                createDate: createDate,
                expireDate: expireDate
            );
        }

        public ClaimsPrincipal DecodeRefreshToken(string refreshTokenValue)
        {
            string secretKey = _appSettingsUtils.GetConfigurationValue(SecretKeyConfig);
            byte[] key = Encoding.UTF8.GetBytes(secretKey); // Ensure you use your actual secret key

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // Set to true if you need to validate the issuer
                ValidateAudience = false, // Set to true if you need to validate the audience
                ValidateLifetime = true, // Validate token expiration
                ClockSkew = TimeSpan.Zero // Reduce clock skew
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(refreshTokenValue, validationParameters, out SecurityToken validatedToken);
            return principal;
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
            return principal;
        }
    }
}
