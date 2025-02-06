using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.DeleteVideo
{
    [XmlRoot("DeleteVideoResponseDto")]
    internal class DeleteVideoResponseDto : BaseResponseDto
    {
        internal DeleteVideoResponseDto(bool success = true) : base(success) { }
    }
}