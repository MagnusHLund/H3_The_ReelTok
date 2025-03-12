using reeltok.api.videos.Entities;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.videos.Data
{
    public class VideosDbContext : DbContext
    {
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<VideoTotalLikesEntity> VideosLikes { get; set; }

        public VideosDbContext(DbContextOptions<VideosDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoEntity>()
                .HasIndex(v => v.UserId); // Separate index on UserId

            modelBuilder.Entity<VideoEntity>()
                .HasIndex(v => v.Title); // Separate index on Title

            modelBuilder.Entity<VideoTotalLikesEntity>()
                .HasKey(vl => vl.VideoLikesId);
        }

    }
}
