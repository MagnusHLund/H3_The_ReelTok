using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    public class VideoLikesEntity
    {
        [Key]
        public uint VideoLikesId { get; set; }
        [Required]
        public Guid VideoId { get; set; }
        [Required]
        public uint TotalLikes { get; set; } = 0;

        public VideoLikesEntity(uint videoLikesId, Guid videoId, uint totalLikes)
        {
            VideoLikesId = videoLikesId;
            VideoId = videoId;
            TotalLikes = totalLikes;
        }
    }
}
