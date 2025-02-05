using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class GatewayUploadVideoResponseDto : BaseResponseDto
    {
        public Video Video { get; set; }

        public GatewayUploadVideoResponseDto(Video video)
        {
            Video = video;
        }
    }
}