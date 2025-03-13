using Newtonsoft.Json;
using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class GatewayUploadVideoRequestDto
    {
        [Required]
        [StringLength(50)]
        [JsonProperty("Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        [JsonProperty("Category")]
        public CategoryType Category { get; set; }

        [Required]
        [JsonProperty("Video")]
        public IFormFile Video { get; set; }

        public GatewayUploadVideoRequestDto(string title, string description, CategoryType category, IFormFile video)
        {
            Title = title;
            Description = description;
            Category = category;
            Video = video;
        }

        // Parameterless constructor required for multipart/form-data requests
        public GatewayUploadVideoRequestDto() { }
    }
}
