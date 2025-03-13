using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoUpload
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid? UserId { get; set; }

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
        public IFormFile VideoFile { get; }

        public VideoUpload(string title, string description, CategoryType category, IFormFile videoFile)
        {
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }

        public VideoUpload(Guid userId, string title, string description, CategoryType category, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }
    }
}
