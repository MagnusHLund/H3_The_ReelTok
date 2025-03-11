using reeltok.api.gateway.Enums;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    public class GatewayUploadVideoRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        public CategoryType Tag { get; set; }

        [Required]
        public IFormFile Video { get; set; }

        public GatewayUploadVideoRequestDto(string title, string description, CategoryType tag, IFormFile video)
        {
            Title = title;
            Description = description;
            Tag = tag;
            Video = video;
        }
    }
}
