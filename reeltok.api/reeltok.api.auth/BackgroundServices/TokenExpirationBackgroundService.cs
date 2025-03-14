using reeltok.api.auth.Data;
using reeltok.api.auth.Utils;
using reeltok.api.auth.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.ValueObjects;
using reeltok.api.auth.Interfaces.Entities;

namespace reeltok.api.auth.BackgroundServices
{
    public class TokenExpirationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _checkInterval;

        public TokenExpirationBackgroundService(IServiceProvider serviceProvider, TimeSpan checkInterval)
        {
            _serviceProvider = serviceProvider;
            _checkInterval = checkInterval;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndRevokeExpiredTokens(stoppingToken).ConfigureAwait(false);
                await Task.Delay(_checkInterval, stoppingToken).ConfigureAwait(false);
            }
        }

        private async Task CheckAndRevokeExpiredTokens(CancellationToken stoppingToken)
        {
            await RevokeUnrevokedExpiredTokens<AccessTokenEntity, AccessToken>(stoppingToken)
                .ConfigureAwait(false);

            await RevokeUnrevokedExpiredTokens<RefreshTokenEntity, RefreshToken>(stoppingToken)
                .ConfigureAwait(false);
        }

        private async Task RevokeUnrevokedExpiredTokens<TTokenEntity, TToken>(CancellationToken stoppingToken)
            where TTokenEntity : class, ITokenEntity<TToken>
            where TToken : IToken
        {

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                AuthDbContext dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

                long currentTime = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

                List<TTokenEntity> expiredTokens = await dbContext.Set<TTokenEntity>()
                    .Where(t => t.Token.ExpiresAt < currentTime && !t.RevokedAt.HasValue)
                    .ToListAsync(stoppingToken)
                    .ConfigureAwait(false);

                foreach (TTokenEntity token in expiredTokens)
                {
                    token.RevokedAt = currentTime;
                }

                await dbContext.SaveChangesAsync(stoppingToken)
                    .ConfigureAwait(false);
            }
        }
    }
}