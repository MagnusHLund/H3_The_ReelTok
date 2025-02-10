using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class GatewayUploadVideoResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Video")]
        [Required]
        public Video Video { get; set; }

        public GatewayUploadVideoResponseDto(Video video)
        {
            Video = video;
        }
    }
}