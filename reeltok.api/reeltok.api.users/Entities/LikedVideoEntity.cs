using reeltok.api.users.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.Entities
{
    public class LikedVideoEntity
    {
        [Key]
        public uint LikedVideoId { get; set; }

        [Required]
        public LikedDetails LikedVideoDetails { get; set; }
        public LikedVideoEntity(LikedDetails likedVideoDetails)
        {
            LikedVideoDetails = likedVideoDetails;
        }

        private LikedVideoEntity() { }
    }
}