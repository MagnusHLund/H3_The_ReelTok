using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.RemoveLike
{
    [XmlRoot("RemoveLikeResponseDto")]
    public class RemoveLikeResponseDto : BaseResponseDto
    {
        public RemoveLikeResponseDto(bool success = true) : base(success) { }
    }
}
