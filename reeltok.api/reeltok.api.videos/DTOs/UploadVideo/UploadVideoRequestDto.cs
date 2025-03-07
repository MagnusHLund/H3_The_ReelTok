using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
{
    public class UploadVideoRequestDto
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
        public byte Category { get; set; }

        [Required]
        [JsonProperty("VideoFile")]
        public IFormFile VideoFile { get; set; }

        public UploadVideoRequestDto(Guid userId, string title, string description, byte category, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }
    }
}
