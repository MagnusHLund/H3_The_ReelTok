using System.Security.Claims;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface ITokensService
    {
        Task<AccessToken> GenerateAccessToken(Guid userId);
        ClaimsPrincipal DecodeAccessToken(string accessTokenValue);
        Task<RefreshToken> GenerateRefreshToken(Guid userId);
        Task<Guid> GetUserIdByRefreshToken(string refreshTokenValue);
        Task RevokeTokens(string accessTokenValue, string refreshTokenValue);
    }
}
