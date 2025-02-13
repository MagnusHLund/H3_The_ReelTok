using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class LikedVideo
    {
        [Key]
        public uint LikedVideoId { get; set; }

        [Required]
        public LikedDetails LikedVideoDetails { get; set; }
        public LikedVideo(LikedDetails likedVideoDetails)
        {
            LikedVideoDetails = likedVideoDetails;
        }

        private LikedVideo() { }
    }
}