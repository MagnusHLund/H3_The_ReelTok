using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.Entities
{
    public class CategoryEntity
    {
        [Key]
        public uint CategoryId { get; set; }
        
        [Required]
        public CategoryType Category { get; set; }

        public ICollection<CategoryUserInterestEntity>? UserInterestCategoryEntities { get; set; }
        public ICollection<CategoryVideoCategoryEntity>? VideoCategoryCategoryEntities { get; set; }

        public CategoryEntity(uint categoryId, CategoryType category)
        {
            CategoryId = categoryId;
            Category = category;
        }

        public CategoryEntity(CategoryType category)
        {
            Category = category;
        }

        private CategoryEntity() { }
    }
}
