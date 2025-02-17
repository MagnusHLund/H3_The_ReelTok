using reeltok.api.videos.Entities;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.videos.Data
{
    public class VideosDbContext : DbContext
    {
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<VideoLikesEntity> VideosLikes { get; set; }
        // TODO: ensure proper keys in the database
        public VideosDbContext(DbContextOptions<VideosDbContext> options) : base(options) { }
    }
}
