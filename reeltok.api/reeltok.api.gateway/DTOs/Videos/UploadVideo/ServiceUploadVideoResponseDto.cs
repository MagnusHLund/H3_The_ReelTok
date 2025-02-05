using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class ServiceUploadVideoResponseDto : BaseResponseDto
    {
        public Video Video { get; set; }

        public ServiceUploadVideoResponseDto(Video video)
        {
            Video = video;
        }
    }
}