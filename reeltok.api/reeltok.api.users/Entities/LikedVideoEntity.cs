using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reeltok.api.users.Entities
{
    public class LikedVideoEntity
    {
        [Key]
        public uint LikedVideoId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; } = Guid.Empty;

        [Required]
        public Guid VideoId { get; set; } = Guid.Empty;

        public LikedVideoEntity(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        private LikedVideoEntity() { }
    }
}