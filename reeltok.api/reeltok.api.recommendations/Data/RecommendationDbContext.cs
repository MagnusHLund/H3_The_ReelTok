namespace reeltok.api.recommendations.Data
{
    using Microsoft.EntityFrameworkCore;
    using reeltok.api.recommendations.Entities;
    using reeltok.api.recommendations.ValueObjects;

    public class RecommendationDbContext : DbContext
    {
        public RecommendationDbContext(DbContextOptions<RecommendationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<RecommendationEntity> Recommendations { get; set; }
        public DbSet<WatchedVideoEntity> WatchedVideoEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<WatchedVideoEntity>().ToTable("VideoWatched");
            modelBuilder.Entity<RecommendationEntity>().ToTable("Recommendations");

            modelBuilder.Entity<CategoryEntity>().OwnsOne(ce => ce.CategoryDetails, CategoryDetails =>
            {
                CategoryDetails.Property(cd => cd.CategoryName).HasColumnName("CategoryName");
            });

            modelBuilder.Entity<WatchedVideoEntity>().OwnsOne(vw => vw.WatchedVideoDetails, WatchedVideoDetails =>
            {
                WatchedVideoDetails.Property(vwd => vwd.UserId).HasColumnName("UserId");
                WatchedVideoDetails.Property(vwd => vwd.VideoId).HasColumnName("VideoId");
                WatchedVideoDetails.Property(vwd => vwd.TimeWatched).HasColumnName("TimeWatched");
            });

            modelBuilder.Entity<RecommendationEntity>().OwnsOne(re => re.RecommendationDetails, RecommendationDetails =>
            {
                RecommendationDetails.Property(rd => rd.UserId).HasColumnName("UserId");
            });

            // **Explicitly Define Many-to-Many Relationship with Custom Table and Column Names**
            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.RecommendationEntities)
                .WithMany(r => r.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryRecommendations", // Custom Join Table Name
                    j => j.HasOne<RecommendationEntity>()
                        .WithMany()
                        .HasForeignKey("RecommendationId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<CategoryEntity>()
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("RecommendationId", "CategoryId"); // Composite Primary Key
                        j.ToTable("CategoryRecommendations"); // Set Table Name
                    });

            modelBuilder.Entity<CategoryEntity>()
                .HasMany(c => c.WatchedVideoEntities)
                .WithMany(v => v.CategoryEntities)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryWatchedVideos", // Custom Join Table Name
                    j => j.HasOne<WatchedVideoEntity>()
                        .WithMany()
                        .HasForeignKey("WatchedVideoId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<CategoryEntity>()
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("CategoryId", "WatchedVideoId"); // Composite Primary Key
                        j.ToTable("CategoryWatchedVideos"); // Set Table Name
                    });
        }
    }
}
