using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.Entities;

namespace reeltok.api.auth.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base (options) { }

        public DbSet<UserCredentialsEntity> UserCredentials { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }
    }
}
