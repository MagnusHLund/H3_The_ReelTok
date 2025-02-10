using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class LikedVideo
    {
        [Key]
        public uint LikedVideoId { get; set; } = 0;

        [Required]
        public LikedDetails LikedVideoDetails { get; set; }
        public LikedVideo(uint likedVideoId, LikedDetails likedVideoDetails)
        {
            LikedVideoId = likedVideoId;
            LikedVideoDetails = likedVideoDetails;
        }

        private LikedVideo() { }
    }
}