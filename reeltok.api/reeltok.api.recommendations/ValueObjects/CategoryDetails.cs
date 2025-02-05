using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.ValueObjects
{
    public class CategoryDetails
    {
        [Required]
        public string CategoryName { get; set; }

        public CategoryDetails(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}