using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    internal class AddLikeResponseDto : BaseResponseDto
    {
        internal AddLikeResponseDto(bool success) : base(success) { }
    }
}