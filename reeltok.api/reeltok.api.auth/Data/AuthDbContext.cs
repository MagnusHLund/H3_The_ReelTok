using reeltok.api.auth.Entities;
using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<RefreshTokenEntity>().Property(p => p.TokenId)
                .HasColumnName("RefreshTokenId");

            modelBuilder.Entity<AccessTokenEntity>().Property(p => p.TokenId)
                .HasColumnName("AccessTokenId");

            // Configure value objects within entities
            modelBuilder.Entity<RefreshTokenEntity>().OwnsOne(rt => rt.Token, rt =>
            {
                rt.Property(p => p.TokenValue).HasColumnName("TokenValue");
                rt.Property(p => p.CreatedAt).HasColumnName("CreatedAt");
                rt.Property(p => p.ExpiresAt).HasColumnName("ExpiresAt");

                rt.HasIndex(p => p.TokenValue)
                    .IsUnique();

                rt.HasIndex(p => p.ExpiresAt);
            });

            modelBuilder.Entity<AccessTokenEntity>().OwnsOne(at => at.Token, at =>
            {
                at.Property(p => p.TokenValue).HasColumnName("TokenValue");
                at.Property(p => p.CreatedAt).HasColumnName("CreatedAt");
                at.Property(p => p.ExpiresAt).HasColumnName("ExpiresAt");

                at.HasIndex(p => p.TokenValue)
                    .IsUnique();

                at.HasIndex(p => p.ExpiresAt);
            });

            modelBuilder.Entity<RefreshTokenEntity>().HasIndex(rt => rt.RevokedAt);

            modelBuilder.Entity<AccessTokenEntity>().HasIndex(at => at.RevokedAt);

            modelBuilder.Entity<UserCredentialsEntity>().OwnsOne(uc => uc.HashedPasswordDetails, uc =>
            {
                uc.Property(p => p.Password).HasColumnName("HashedPassword");
                uc.Property(p => p.Salt).HasColumnName("Salt");
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
