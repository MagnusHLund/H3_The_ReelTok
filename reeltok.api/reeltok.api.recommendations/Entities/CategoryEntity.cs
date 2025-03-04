using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class CategoryEntity
    {
        [Key]
        public uint CategoryId { get; private set; }
        [Required]
        public CategoryDetails CategoryDetails { get; private set; }

        public List<UserInterestEntity> UserInterestEntities { get; private set; }
        public List<VideoCategoryEntity> VideoCategoryEntities { get; private set; }

        public CategoryEntity(CategoryDetails categoryDetails)
        {
            CategoryDetails = categoryDetails;
        }

        private CategoryEntity () {}
    }
}
