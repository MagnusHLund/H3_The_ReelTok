using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Enums;

namespace reeltok.api.videos.Entities
{
    [Index(nameof(UserId), nameof(Tag))]
    public class Video
    {
        // TODO: Update properties to follow the same rules as the DTOs in gateway allow (For example minimum length of the title)

        [Key]
        public Guid VideoId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public RecommendedCategories Tag { get; set; }
        [Required]
        [MaxLength(50)]
        public string StreamUrl { get; set; }
        [Required]
        public uint Likes { get; set; }
        [Required]
        public uint UploadedAt { get; set; }
    }
}
