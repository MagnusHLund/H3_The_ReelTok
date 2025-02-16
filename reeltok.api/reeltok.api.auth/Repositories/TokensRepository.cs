using reeltok.api.auth.Data;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using reeltok.api.auth.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TTokenEntity> RevokeToken<TTokenEntity, TToken>(string tokenValue)
            where TTokenEntity : class, ITokenEntity<TToken>
            where TToken : IToken
        {
            TTokenEntity tokenToRevoke = await _context.Set<TTokenEntity>().FirstOrDefaultAsync(e => e.Token.TokenValue == tokenValue).ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the token in the database!");

            if (tokenToRevoke.RevokedAt != null)
            {
                throw new InvalidOperationException("Token already revoked!");
            }

            tokenToRevoke.RevokedAt = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return tokenToRevoke;
        }

        public async Task<TTokenEntity> SaveToken<TTokenEntity, TToken>(TTokenEntity tokenEntity)
            where TTokenEntity : class, ITokenEntity<TToken>, new()
            where TToken : IToken
        {

            TTokenEntity tokenDatabaseResult = (await _context.AddAsync(tokenEntity).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return tokenDatabaseResult;
        }

        // TODO: When reworking exception middleware, make this remove cookie tokens on exception!
        public async Task<Guid> GetUserIdByRefreshToken(string refreshTokenValue)
        {
            RefreshTokenEntity refreshTokenDatabaseResult = await _context.RefreshTokens.Where(r => r.Token.TokenValue == refreshTokenValue).FirstOrDefaultAsync().ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Unable to find the token in the database!");

            if (refreshTokenDatabaseResult.Token.ExpiresAt < DateTimeUtils.DateTimeToUnixTime(DateTime.Now))
            {
                refreshTokenDatabaseResult = await RevokeToken<RefreshTokenEntity, RefreshToken>(refreshTokenDatabaseResult.Token.TokenValue).ConfigureAwait(false);
            }

            if (refreshTokenDatabaseResult.RevokedAt != null)
            {
                throw new InvalidOperationException("User is revoked!");
            }

            Guid userId = refreshTokenDatabaseResult.UserId;

            return userId;
        }
    }
}
