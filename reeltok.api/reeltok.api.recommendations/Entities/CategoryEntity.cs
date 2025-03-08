using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class CategoryEntity
    {
        [Key]
        public uint CategoryId { get; private set; }
        [Required]
        public CategoryDetails CategoryDetails { get; private set; }

        public List<UserInterestEntity>? UserInterestEntities { get; set; }
        public List<VideoCategoryEntity>? VideoCategoryEntities { get; set; }

        public CategoryEntity(CategoryDetails categoryDetails)
        {
            CategoryDetails = categoryDetails;
        }

        private CategoryEntity() { }
    }
}
