using reeltok.api.auth.Interfaces.Entities;

namespace reeltok.api.auth.Interfaces.Repositories
{
    public interface ITokensRepository
    {
        Task RevokeToken<TTokenEntity, TToken>(string tokenValue)
            where TTokenEntity : class, ITokenEntity<TToken>
            where TToken : IToken;

        Task<TTokenEntity> SaveToken<TTokenEntity, TToken>(TTokenEntity tokenEntity)
            where TTokenEntity : class, ITokenEntity<TToken>, new()
            where TToken : IToken;

        Task<Guid?> GetUserIdByRefreshToken(string refreshTokenValue);
        Task<bool> IsAccessTokenRevokedAsync(string accessTokenValue);
    }
}
