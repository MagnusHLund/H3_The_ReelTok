
using System.Xml.Serialization;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class UploadVideoResponseDto : BaseResponseDto
    {
        public UploadVideoResponseDto(bool success = true) : base(success) { }
        public UploadVideoResponseDto() { }
    }
}
