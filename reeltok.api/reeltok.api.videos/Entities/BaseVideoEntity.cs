using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.Entities
{
    public class BaseVideoEntity
    {
        [Key]
        public Guid VideoId { get; set; }
        public string StreamPath { get; set; }
        public uint UploadedAt { get; set; }

        public BaseVideoEntity(Guid videoId, string streamPath, uint uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}