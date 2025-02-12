using System.Security.Claims;
using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces
{
    public interface ITokensService
    {
        RefreshToken GenerateRefreshToken(Guid userId);
        AccessToken GenerateAccessToken(Guid userId);
        ClaimsPrincipal DecodeRefreshToken(string refreshTokenValue);
        ClaimsPrincipal DecodeAccessToken(string accessTokenValue);

    }
}
