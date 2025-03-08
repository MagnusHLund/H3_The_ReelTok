using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    [Index(nameof(UserId), nameof(Title))] // TODO: Move indexing into the DbContext instead, using fluent api
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
            uint uploadedAt
        ) : base(videoId, streamPath, uploadedAt)
        {
            UserId = userId;
            Title = title;
            Description = description;
            StreamPath = streamPath;
        }
    }
}
