namespace reeltok.api.recommendations.Entities
{
    public class CategoryUserInterestEntity
    {
        public uint UserInterestId { get; set; }
        public UserEntity UserInterest { get; set; }
        public uint CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public CategoryUserInterestEntity(UserEntity userInterest, uint categoryId)
        {
            UserInterest = userInterest;
            CategoryId = categoryId;
        }

        private CategoryUserInterestEntity() { }
    }
}