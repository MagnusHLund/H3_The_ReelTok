using reeltok.api.users.Entities;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.users.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<LikedVideoEntity> LikedVideos { get; set; }
        public DbSet<SubscriptionEntity> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("Users");

            modelBuilder.Entity<UserEntity>()
                .OwnsOne(u => u.UserDetails, ud =>
                {
                    ud.Property(u => u.Username).HasColumnName("Username");
                    ud.Property(u => u.ProfileUrlPath).HasColumnName("ProfileUrlPath");
                    ud.Property(u => u.ProfilePictureUrlPath).HasColumnName("ProfilePictureUrlPath");

                    ud.HasIndex(u => u.ProfileUrlPath).IsUnique();
                    ud.HasIndex(u => u.ProfilePictureUrlPath).IsUnique();
                });

            modelBuilder.Entity<UserEntity>()
                .OwnsOne(u => u.HiddenUserDetails, hd =>
                {
                    hd.Property(h => h.Email).HasColumnName("Email");

                    hd.HasIndex(h => h.Email).IsUnique();
                });

            // LikedVideoEntity configuration
            modelBuilder.Entity<LikedVideoEntity>()
                .HasIndex(lv => new { lv.UserId, lv.VideoId })
                .IsUnique();

            modelBuilder.Entity<LikedVideoEntity>()
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(lv => lv.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // SubscriptionEntity configuration
            modelBuilder.Entity<SubscriptionEntity>()
                .HasIndex(s => new { s.UserId, s.SubscribingToUserId })
                .IsUnique();

            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasOne<UserEntity>()
                .WithMany()
                .HasForeignKey(s => s.SubscribingToUserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
