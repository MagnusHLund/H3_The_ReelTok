using System.Xml.Serialization;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("UploadVideoRequestDto")]
    internal class UploadVideoRequestDto
    {
        [XmlElement("UserId")]
        internal Guid UserId { get; set; }
        [XmlElement("Video")]
        internal VideoUpload Video { get; set; }

        internal UploadVideoRequestDto(Guid userId, VideoUpload videoUpload)
        {
            UserId = userId;
            Video = videoUpload;
        }
    }
}