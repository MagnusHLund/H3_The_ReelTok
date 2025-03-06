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
                .OwnsOne(u => u.UserDetails);

            modelBuilder.Entity<UserEntity>()
                .OwnsOne(u => u.UserDetails, ud =>
                {
                    ud.Property(u => u.Username).HasColumnName("Username");
                    ud.Property(u => u.ProfileUrl).HasColumnName("ProfileUrl");
                    ud.Property(u => u.ProfilePictureUrl).HasColumnName("ProfilePictureUrl");
                });

            modelBuilder.Entity<UserEntity>()
                .OwnsOne(u => u.HiddenUserDetails, hd =>
                {
                    hd.Property(h => h.Email).HasColumnName("Email");
                });

            modelBuilder.Entity<LikedVideoEntity>().OwnsOne(lv => lv.LikedVideoDetails, lv =>
            {
                lv.Property(l => l.UserId).HasColumnName("UserId");
                lv.Property(l => l.VideoId).HasColumnName("VideoId");

                lv.WithOwner();

                lv.HasOne<UserEntity>()
                    .WithMany()
                    .HasForeignKey(lv => lv.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SubscriptionEntity>().OwnsOne(s => s.SubDetails, sd =>
            {
                sd.Property(s => s.UserId).HasColumnName("SubscriberUserId");
                sd.Property(s => s.SubscribingToUserId).HasColumnName("SubscribingToUserId");

                sd.WithOwner();

                // Configure foreign keys without cascade delete
                sd.HasOne<UserEntity>()
                    .WithMany()
                    .HasForeignKey(s => s.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                sd.HasOne<UserEntity>()
                    .WithMany()
                    .HasForeignKey(s => s.SubscribingToUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
