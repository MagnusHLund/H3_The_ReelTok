using Newtonsoft.Json;
using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class ServiceUploadVideoRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("Category")]
        public CategoryType Category { get; set; }

        [Required]
        [JsonProperty("VideoFile")]
        public IFormFile VideoFile { get; set; }

        public ServiceUploadVideoRequestDto(
            Guid userId,
            string title,
            string description,
            CategoryType category,
            IFormFile videoFile
        )
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }
    }
}
