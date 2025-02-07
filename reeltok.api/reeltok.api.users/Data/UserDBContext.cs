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

            modelBuilder.Entity<LikedVideo>().OwnsOne(lv => lv.LikedVideoDetails);

            modelBuilder.Entity<Subscription>().OwnsOne(s => s.SubDetails);
        }
    }
}