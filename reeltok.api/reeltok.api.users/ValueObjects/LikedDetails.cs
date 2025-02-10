using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class LikedDetails
    {

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; private set; } = Guid.Empty;
        // public UserProfileData? User { get; private set; } = null;

        [Required]
        public Guid VideoId { get; private set; } = Guid.Empty;
        public LikedDetails(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        private LikedDetails() { }
    }
}