using reeltok.api.auth.Entities;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Services;
using reeltok.api.auth.Interfaces.Repositories;

namespace reeltok.api.auth.Services
{
    public class TokenManagementService : ITokenManagementService
    {
        private readonly ITokensRepository _tokensRepository;

        public TokenManagementService(ITokensRepository tokensRepository)
        {
            _tokensRepository = tokensRepository;
        }

        public async Task RevokeTokens(string accessTokenValue, string refreshTokenValue)
        {
            await _tokensRepository.RevokeToken<AccessTokenEntity, AccessToken>(accessTokenValue)
                .ConfigureAwait(false);

            await _tokensRepository.RevokeToken<RefreshTokenEntity, RefreshToken>(refreshTokenValue)
                .ConfigureAwait(false);
        }

        public async Task<Guid?> GetUserIdByRefreshToken(string refreshTokenValue)
        {
            Guid? userId = await _tokensRepository.GetUserIdByRefreshToken(refreshTokenValue)
                .ConfigureAwait(false);

            return userId;
        }
    }
}