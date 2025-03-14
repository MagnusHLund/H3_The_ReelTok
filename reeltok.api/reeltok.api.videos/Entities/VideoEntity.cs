using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    public class VideoEntity : BaseVideoEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(256)]
        public string Description { get; set; }

        public VideoEntity(
            Guid videoId,
            Guid userId,
            string title,
            string description,
            string streamPath,
            long uploadedAt
        ) : base(videoId, streamPath, uploadedAt)
        {
            UserId = userId;
            Title = title;
            Description = description;
            StreamPath = streamPath;
        }
    }
}
