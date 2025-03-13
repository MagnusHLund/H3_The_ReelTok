using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoUpload
    {
        [Required]
        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [Required]
        [JsonProperty("Title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("Category")]
        public string Category { get; set; }

        [Required]
        [JsonProperty("VideoFile")]
        public IFormFile VideoFile { get; }

        public VideoUpload(string title, string description, string category, IFormFile videoFile)
        {
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }

        public VideoUpload(string userId, string title, string description, string category, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }
    }
}
