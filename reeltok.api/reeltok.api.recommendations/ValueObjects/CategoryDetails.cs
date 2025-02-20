using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.ValueObjects
{
    public class CategoryDetails
    {
        [Required]
        public RecommendedCategories CategoryName { get; private set; }

        public CategoryDetails(RecommendedCategories categoryName)
        {
            CategoryName = categoryName;
        }
        private CategoryDetails () {}
    }
}
