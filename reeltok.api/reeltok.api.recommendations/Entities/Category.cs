using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class Category
    {
        [Required]
        public uint CategoryId { get; set; }
        [Required]
        public CategoryDetails CategoryDetails { get; set; }

        public Category(uint categoryId, CategoryDetails categoryDetails)
        {
            CategoryId = categoryId;
            CategoryDetails = categoryDetails;
        }

    }
}