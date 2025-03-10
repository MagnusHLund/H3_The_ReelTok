using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Interfaces.Services
{
    public interface ITokenGenerationService
    {
        Task<AccessToken> GenerateAccessToken(Guid userId);
        Task<RefreshToken> GenerateRefreshToken(Guid userId);
    }
}