using System.Xml.Serialization;
using reeltok.api.gateway.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class ServiceUploadVideoResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Video")]
        [Required]
        public Video Video { get; set; }

        public ServiceUploadVideoResponseDto(Video video, bool success = true) : base(success)
        {
            Video = video;
        }
    }
}
