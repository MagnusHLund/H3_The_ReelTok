using reeltok.api.videos.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    [Index(nameof(UserId), nameof(Tag))]
    public class VideoEntity
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
        public string StreamPath { get; set; }
        [Required]
        public uint UploadedAt { get; set; }

        public VideoEntity(Guid videoId, Guid userId, string title, string description, RecommendedCategories tag, string streamPath, uint uploadedAt)
        {
            VideoId = videoId;
            UserId = userId;
            Title = title;
            Description = description;
            Tag = tag;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}
