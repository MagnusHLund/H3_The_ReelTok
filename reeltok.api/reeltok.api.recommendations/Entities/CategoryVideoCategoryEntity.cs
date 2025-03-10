namespace reeltok.api.recommendations.Entities
{
    public class CategoryVideoCategoryEntity
    {
        public uint VideoCategoryId { get; set; }
        public VideoEntity VideoCategory { get; set; }
        public uint CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public CategoryVideoCategoryEntity(VideoEntity videoCategory, uint categoryId)
        {
            VideoCategory = videoCategory;
            CategoryId = categoryId;
        }

        private CategoryVideoCategoryEntity() { }
    }
}