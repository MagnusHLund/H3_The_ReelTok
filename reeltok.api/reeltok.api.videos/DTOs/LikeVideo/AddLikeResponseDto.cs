using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeResponseDto")]
    public class AddLikeResponseDto : BaseResponseDto
    {
        public AddLikeResponseDto(bool success) : base(success) { }
    }
}
