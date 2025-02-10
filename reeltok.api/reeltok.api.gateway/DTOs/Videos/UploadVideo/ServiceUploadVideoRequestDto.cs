using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoRequestDto")]
    public class ServiceUploadVideoRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }

        [XmlElement(elementName: "Video")]
        [Required]
        public VideoUpload Video { get; set; }

        public ServiceUploadVideoRequestDto(Guid userId, VideoUpload video)
        {
            UserId = userId;
            Video = video;
        }
    }
}
