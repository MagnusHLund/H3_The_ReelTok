using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Entities;

namespace reeltok.api.users.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {

        }

        public DbSet<UserProfileData> Users { get; set; }
        public DbSet<LikedVideo> LikedVideos { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfileData>().ToTable("Users");

            modelBuilder.Entity<UserProfileData>()
            .OwnsOne(up => up.UserDetails, ud =>
            {
                ud.OwnsOne(u => u.HiddenDetails);  // Ensure HiddenDetails is also owned
            });

            modelBuilder.Entity<LikedVideo>().OwnsOne(lv => lv.LikedVideoDetails, lv =>
            {
                lv.WithOwner();

                lv.HasOne<UserProfileData>()
                    .WithMany()
                    .HasForeignKey(lv => lv.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // modelBuilder.Entity<Subscription>().OwnsOne(s => s.SubDetails);

            modelBuilder.Entity<Subscription>().OwnsOne(s => s.SubDetails, sd =>
            {
                sd.WithOwner();

                // Configure foreign keys without cascade delete
                sd.HasOne<UserProfileData>()
                    .WithMany()
                    .HasForeignKey(s => s.SubscriberUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                sd.HasOne<UserProfileData>()
                    .WithMany()
                    .HasForeignKey(s => s.SubscribingToUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}