using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Data
{
    public class VideoDbContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
