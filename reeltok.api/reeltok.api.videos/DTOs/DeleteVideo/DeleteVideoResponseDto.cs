using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.DeleteVideo
{
    [XmlRoot("DeleteVideoResponseDto")]
    public class DeleteVideoResponseDto : BaseResponseDto
    {
        public DeleteVideoResponseDto(bool success = true) : base(success) { }
    }
}
