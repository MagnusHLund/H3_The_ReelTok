using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
{
    // Form data Request
    public class UploadVideoRequestDto
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public IFormFile VideoFile { get; set; }

        public UploadVideoRequestDto(string userId, string title, string description, string category, IFormFile videoFile)
        {
            UserId = userId;
            Title = title;
            Description = description;
            Category = category;
            VideoFile = videoFile;
        }

        public UploadVideoRequestDto() { }
    }
}
