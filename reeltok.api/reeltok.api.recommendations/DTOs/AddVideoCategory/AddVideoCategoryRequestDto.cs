using Newtonsoft.Json;
using reeltok.api.recommendations.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs.AddVideoCategory
{
    public class AddVideoCategoryRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("Category")]
        public CategoryType Category { get; set; }

        public AddVideoCategoryRequestDto(Guid videoId, CategoryType category)
        {
            VideoId = videoId;
            Category = category;
        }
    }
}