using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.AddVideoCategory
{
    public class AddVideoCategoryResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Category")]
        public CategoryType Category { get; set; }

        public AddVideoCategoryResponseDto(CategoryType category, bool success = true) : base(success)
        {
            Category = category;
        }
    }
}
