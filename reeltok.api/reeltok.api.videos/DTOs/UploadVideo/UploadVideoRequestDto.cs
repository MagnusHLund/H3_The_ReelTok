using System.Xml.Serialization;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("UploadVideoRequestDto")]
    public class UploadVideoRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Video")]
        public VideoUpload Video { get; set; }

        public UploadVideoRequestDto(Guid userId, VideoUpload videoUpload)
        {
            UserId = userId;
            Video = videoUpload;
        }
    }
}
