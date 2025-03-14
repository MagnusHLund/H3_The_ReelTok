using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    public class VideoTotalLikesEntity
    {
        [Key]
        public uint VideoLikesId { get; set; }
        [Required]
        public Guid VideoId { get; set; }
        [Required]
        public uint TotalLikes { get; set; }

        public VideoEntity Video { get; set; }

        public VideoTotalLikesEntity(uint videoLikesId, Guid videoId, uint totalLikes)
        {
            VideoLikesId = videoLikesId;
            VideoId = videoId;
            TotalLikes = totalLikes;
        }

        public VideoTotalLikesEntity(Guid videoId, uint totalLikes)
        {
            VideoId = videoId;
            TotalLikes = totalLikes;
        }
    }
}
