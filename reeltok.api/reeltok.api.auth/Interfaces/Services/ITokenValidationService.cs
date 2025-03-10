using System.Security.Claims;

namespace reeltok.api.auth.Interfaces.Services
{
    public interface ITokenValidationService
    {
        ClaimsPrincipal DecodeAccessToken(string accessTokenValue);
        Task<bool> IsValidAccessToken(string? accessTokenValue);
    }
}