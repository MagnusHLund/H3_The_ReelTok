using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Data
{
    public class RecommendationDbContext : DbContext
    {
        public RecommendationDbContext(
            DbContextOptions<RecommendationDbContext> options) : base(options) { }

        public DbSet<VideoEntity> VideoCategories { get; set; }
        public DbSet<CategoryVideoCategoryEntity> CategoryVideoCategories { get; set; }
        public DbSet<UserEntity> UserInterests { get; set; }
        public DbSet<CategoryUserInterestEntity> CategoryUserInterests { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<WatchedVideoEntity> WatchedVideo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<WatchedVideoEntity>().ToTable("WatchedVideos");
            modelBuilder.Entity<UserEntity>().ToTable("UserInterests");
            modelBuilder.Entity<VideoEntity>().ToTable("VideoCategories");

            modelBuilder.Entity<CategoryUserInterestEntity>().ToTable("CategoryUserInterests");
            modelBuilder.Entity<CategoryVideoCategoryEntity>().ToTable("CategoryVideoCategory");

            modelBuilder.Entity<CategoryVideoCategoryEntity>()
                .HasKey(cvc => new { cvc.CategoryId, cvc.VideoCategoryId });

            modelBuilder.Entity<CategoryVideoCategoryEntity>()
                .HasOne(cvc => cvc.Category)
                .WithMany(c => c.VideoCategoryCategoryEntities)
                .HasForeignKey(cvc => cvc.CategoryId);

            modelBuilder.Entity<CategoryVideoCategoryEntity>()
                .HasOne(cvc => cvc.VideoCategory)
                .WithMany()
                .HasForeignKey(cvc => cvc.VideoCategoryId);

            modelBuilder.Entity<CategoryUserInterestEntity>()
                .HasKey(cui => new { cui.UserInterestId, cui.CategoryId });

            modelBuilder.Entity<CategoryUserInterestEntity>()
                .HasOne(cui => cui.UserInterest)
                .WithMany()
                .HasForeignKey(cui => cui.UserInterestId);

            modelBuilder.Entity<CategoryUserInterestEntity>()
                .HasOne(cui => cui.Category)
                .WithMany(c => c.UserInterestCategoryEntities)
                .HasForeignKey(cui => cui.CategoryId);

            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(c => c.CategoryType)
                .IsUnique();

            modelBuilder.Entity<UserEntity>()
                .HasIndex(ui => ui.UserId);

            modelBuilder.Entity<VideoEntity>()
                .HasIndex(vc => vc.VideoId);

            modelBuilder.Entity<WatchedVideoEntity>()
                .HasIndex(wv => new { wv.UserId, wv.VideoId })
                .IsUnique();

            modelBuilder.Entity<CategoryEntity>()
                .Property(c => c.CategoryType)
                .HasConversion(
                    c => c.ToString(),
                    c => (CategoryType)Enum.Parse(typeof(CategoryType), c));

            CategoryEntity[] categories = Enum.GetValues(typeof(CategoryType))
                .Cast<CategoryType>()
                .Select((category, index) => new CategoryEntity(category) { CategoryId = (uint)(index + 1) })
                .ToArray();

            modelBuilder.Entity<CategoryEntity>().HasData(categories);
        }
    }
}
