namespace reeltok.api.auth.Interfaces.Services
{
    public interface ITokenManagementService
    {
        Task RevokeTokens(string accessTokenValue, string refreshTokenValue);
        Task<Guid?> GetUserIdByRefreshToken(string refreshTokenValue);
    }
}