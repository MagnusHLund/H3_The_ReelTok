using reeltok.api.comments.Entities;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.comments.Data
{
    public class CommentDbContext : DbContext
    {

        public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().ToTable("Comments");

            modelBuilder.Entity<Comment>().OwnsOne(cd => cd.CommentDetails, commentDetails =>
            {
                commentDetails.Property(c => c.UserId).HasColumnName("UserId");
                commentDetails.Property(c => c.VideoId).HasColumnName("VideoId");
                commentDetails.Property(c => c.Message).HasColumnName("Message");
                commentDetails.Property(c => c.CreatedAt).HasColumnName("CreatedAt");
            });

        }

    }
}
