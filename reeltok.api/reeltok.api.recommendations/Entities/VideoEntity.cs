using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.Entities
{
    public class VideoEntity
    {
        [Key]
        public uint VideoCategoryId { get; set; }

        [Required]
        public Guid VideoId { get; set; }


        public VideoEntity(Guid videoId)
        {
            VideoId = videoId;
        }

        private VideoEntity() { }
    }
}
