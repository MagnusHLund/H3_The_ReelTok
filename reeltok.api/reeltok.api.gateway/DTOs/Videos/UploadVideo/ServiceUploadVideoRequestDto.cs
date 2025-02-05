using System.Xml.Serialization;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoRequestDto")]
    public class ServiceUploadVideoRequestDto
    {
        public Guid UserId { get; set; }
        public VideoUpload Video { get; set; }

        public ServiceUploadVideoRequestDto(Guid userId, VideoUpload video)
        {
            UserId = userId;
            Video = video;
        }
    }
}