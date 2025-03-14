using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.ValueObjects
{
    public class LikedDetails
    {
        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public Guid VideoId { get; private set; }

        public LikedDetails(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        private LikedDetails() { }
    }
}