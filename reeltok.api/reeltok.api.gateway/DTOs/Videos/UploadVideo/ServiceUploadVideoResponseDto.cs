using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class ServiceUploadVideoResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Video")]
        [Required]
        public Video Video { get; set; }

        public ServiceUploadVideoResponseDto(Video video, bool success) : base(success)
        {
            Video = video;
        }
    }
}