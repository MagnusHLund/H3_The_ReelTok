using reeltok.api.auth.Data;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Interfaces.Entities;
using reeltok.api.auth.Interfaces.Repositories;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Repositories
{
    public class TokensRepository : ITokensRepository
    {
        private readonly AuthDbContext _context;

        public TokensRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task RevokeToken<TTokenEntity, TToken>(string tokenValue)
            where TTokenEntity : class, ITokenEntity<TToken>
            where TToken : IToken
        {
            TTokenEntity? tokenToRevoke = await _context.Set<TTokenEntity>()
                .FirstOrDefaultAsync(e => e.Token.TokenValue == tokenValue)
                .ConfigureAwait(false);

            if (tokenToRevoke == null)
            {
                return;
            }

            if (tokenToRevoke.RevokedAt != null)
            {
                throw new InvalidOperationException("Token already revoked!");
            }

            tokenToRevoke.RevokedAt = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<TTokenEntity> SaveToken<TTokenEntity, TToken>(TTokenEntity tokenEntity)
            where TTokenEntity : class, ITokenEntity<TToken>, new()
            where TToken : IToken
        {

            TTokenEntity savedToken = (await _context.AddAsync(tokenEntity)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return savedToken;
        }

        public async Task<Guid?> GetUserIdByRefreshToken(string refreshTokenValue)
        {
            RefreshTokenEntity? refreshToken = await _context.RefreshTokens
                .Where(r => r.Token.TokenValue == refreshTokenValue && r.RevokedAt == null)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            refreshToken = await MaybeExpireToken<RefreshTokenEntity, RefreshToken>(refreshToken)
                .ConfigureAwait(false);

            Guid? userId = refreshToken?.UserId;

            return userId;
        }

        public async Task<bool> IsAccessTokenRevokedAsync(string accessTokenValue)
        {
            AccessTokenEntity? accessToken = await _context.AccessTokens
                .Where(a => a.Token.TokenValue == accessTokenValue && a.RevokedAt == null)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            accessToken = await MaybeExpireToken<AccessTokenEntity, AccessToken>(accessToken)
                .ConfigureAwait(false);

            return accessToken == null;
        }

        private async Task<TTokenEntity?> MaybeExpireToken<TTokenEntity, TToken>(TTokenEntity tokenEntity)
            where TTokenEntity : class, ITokenEntity<TToken>
            where TToken : IToken
        {
            if (tokenEntity == null)
            {
                return null;
            }

            long currentUnixTime = DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow);

            if (tokenEntity.Token.ExpiresAt <= currentUnixTime)
            {
                tokenEntity.RevokedAt = currentUnixTime;
                _context.Update(tokenEntity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }

            return tokenEntity.RevokedAt.HasValue ? null : tokenEntity;
        }
    }
}
