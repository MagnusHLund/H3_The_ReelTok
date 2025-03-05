namespace reeltok.api.recommendations.Data
{
    using Microsoft.EntityFrameworkCore;
    using reeltok.api.recommendations.Entities;
    public class RecommendationDbContext : DbContext
    {
        public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<UserInterestEntity> UserInterests { get; set; }
        public DbSet<WatchedVideoEntity> WatchedVideoEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<WatchedVideoEntity>().ToTable("VideoWatched");
            modelBuilder.Entity<UserInterestEntity>().ToTable("UserInterests");
            modelBuilder.Entity<VideoCategoryEntity>().ToTable("VideoCategories");

            modelBuilder.Entity<CategoryEntity>().OwnsOne(ce => ce.CategoryDetails, CategoryDetails =>
            {
                CategoryDetails.Property(cd => cd.CategoryName).HasColumnName("CategoryName").HasConversion<string>();
            });

            modelBuilder.Entity<WatchedVideoEntity>().OwnsOne(vw => vw.WatchedVideoDetails, WatchedVideoDetails =>
            {
                WatchedVideoDetails.Property(vwd => vwd.UserId).HasColumnName("UserId");
                WatchedVideoDetails.Property(vwd => vwd.VideoId).HasColumnName("VideoId");
                WatchedVideoDetails.Property(vwd => vwd.TimeWatched).HasColumnName("TimeWatched");
            });

            modelBuilder.Entity<UserInterestEntity>().OwnsOne(ur => ur.UserInterestDetails, UserInterestDetails =>
            {
                UserInterestDetails.Property(ud => ud.UserId).HasColumnName("UserId");
            });

            modelBuilder.Entity<VideoCategoryEntity>().OwnsOne(vc => vc.VideoCategoryDetails, VideoCategoryDetails =>
            {
                VideoCategoryDetails.Property(vcd => vcd.VideoId).HasColumnName("VideoId");
            });

            // **Explicitly Define Many-to-Many Relationship with Custom Table and Column Names**
            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.UserInterestEntities)
                .WithMany(r => r.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryUserInterests", // Custom Join Table Name
                    j => j.HasOne<UserInterestEntity>()
                        .WithMany()
                        .HasForeignKey("UserInterestId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<CategoryEntity>()
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("UserInterestId", "CategoryId"); // Composite Primary Key
                        j.ToTable("CategoryUserInterests"); // Set Table Name
                    });

            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.VideoCategoryEntities)
                .WithMany(v => v.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryVideoCategories", // Custom Join Table Name
                    j => j.HasOne<VideoCategoryEntity>()
                        .WithMany()
                        .HasForeignKey("VideoCategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<CategoryEntity>()
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("CategoryId", "VideoCategoryId"); // Composite Primary Key
                        j.ToTable("CategoryVideoCategories"); // Set Table Name
                    });
        }
    }
}
