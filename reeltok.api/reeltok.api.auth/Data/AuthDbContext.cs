using reeltok.api.auth.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.auth.ValueObjects;

namespace reeltok.api.auth.Data
{
    public class AuthDbContext : DbContext
    {
        public DbSet<UserCredentialsEntity> UserCredentials { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
        public DbSet<AccessTokenEntity> AccessTokens { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly ignore the value objects
            modelBuilder.Ignore<AccessToken>();
            modelBuilder.Ignore<RefreshToken>();

            // Configure value objects within entities
            modelBuilder.Entity<RefreshTokenEntity>().OwnsOne(rt => rt.Token, rt =>
            {
                rt.Property(p => p.TokenValue).HasColumnName("RefreshTokenValue").IsRequired();
                rt.Property(p => p.CreatedAt).HasColumnName("RefreshTokenCreatedAt").IsRequired();
                rt.Property(p => p.ExpiresAt).HasColumnName("RefreshTokenExpiresAt").IsRequired();
            });

            modelBuilder.Entity<AccessTokenEntity>().OwnsOne(at => at.Token, at =>
            {
                at.Property(p => p.TokenValue).HasColumnName("AccessTokenValue").IsRequired();
                at.Property(p => p.CreatedAt).HasColumnName("AccessTokenCreatedAt").IsRequired();
                at.Property(p => p.ExpiresAt).HasColumnName("AccessTokenExpiresAt").IsRequired();
            });

            modelBuilder.Entity<AccessTokenEntity>().OwnsOne(at => at.Token, at =>
            {
                at.Property(p => p.TokenValue).HasColumnName("AccessTokenValue").IsRequired();
                at.Property(p => p.CreatedAt).HasColumnName("AccessTokenCreatedAt").IsRequired();
                at.Property(p => p.ExpiresAt).HasColumnName("AccessTokenExpiresAt").IsRequired();

                at.HasIndex(p => p.TokenValue).IsUnique().HasDatabaseName("UX_AccessToken_TokenValue");
                at.HasIndex(p => p.ExpiresAt).HasDatabaseName("IX_AccessToken_ExpiresAt");
            });

            modelBuilder.Entity<UserCredentialsEntity>()
                .HasMany<RefreshTokenEntity>()
                .WithOne()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCredentialsEntity>()
                .HasMany<AccessTokenEntity>()
                .WithOne()
                .HasForeignKey(at => at.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
