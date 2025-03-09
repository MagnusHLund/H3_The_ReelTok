namespace reeltok.api.recommendations.Entities
{
    public class CategoryVideoCategoryEntity
    {
        public uint CategoryId { get; set; }
        public CategoryEntity Category { get; set; }
        public uint VideoCategoryId { get; set; }
        public VideoEntity VideoCategory { get; set; }

        public CategoryVideoCategoryEntity(VideoEntity videoCategory, CategoryEntity category)
        {
            VideoCategory = videoCategory;
            Category = category;
        }

        private CategoryVideoCategoryEntity() { }
    }
}