using reeltok.api.comments.Entities;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.comments.Data
{
    public class CommentsDbContext : DbContext
    {
        public DbSet<CommentEntity> Comments { get; set; }

        public CommentsDbContext(DbContextOptions<CommentsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentEntity>().ToTable("Comments");

            modelBuilder.Entity<CommentEntity>().OwnsOne(cd => cd.CommentDetails, commentDetails =>
            {
                commentDetails.Property(cd => cd.UserId).HasColumnName("UserId");
                commentDetails.Property(cd => cd.VideoId).HasColumnName("VideoId");
                commentDetails.Property(cd => cd.Message).HasColumnName("Message");
                commentDetails.Property(cd => cd.CreatedAt).HasColumnName("CreatedAt");

                commentDetails.HasIndex(cd => cd.UserId);
                commentDetails.HasIndex(cd => cd.VideoId);
                commentDetails.HasIndex(cd => cd.CreatedAt);

                commentDetails.HasIndex(cd => new { cd.UserId, cd.VideoId });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
