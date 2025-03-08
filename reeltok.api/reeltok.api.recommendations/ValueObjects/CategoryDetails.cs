using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.ValueObjects
{
    public class CategoryDetails
    {
        [Required]
        public CategoryType CategoryName { get; private set; }

        public CategoryDetails(CategoryType categoryName)
        {
            CategoryName = categoryName;
        }
        private CategoryDetails() { }
    }
}
