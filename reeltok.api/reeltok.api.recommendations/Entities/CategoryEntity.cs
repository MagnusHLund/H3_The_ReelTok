using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Entities
{
    public class CategoryEntity
    {
        [Key]
        public uint CategoryId { get; set; }
        [Required]
        public CategoryType CategoryType { get; set; }

        public ICollection<CategoryUserInterestEntity>? UserInterestCategoryEntities { get; set; }
        public ICollection<CategoryVideoCategoryEntity>? VideoCategoryCategoryEntities { get; set; }

        public CategoryEntity(uint categoryId, CategoryType category)
        {
            CategoryId = categoryId;
            CategoryType = category;
        }

        private CategoryEntity() { }
    }
}
