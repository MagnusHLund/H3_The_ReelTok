using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    [XmlRoot("UserLikedVideoResponseDto")]
    public class ServiceUserLikedVideoResponseDto : BaseResponseDto
    {
        [XmlElement("UserHasLikedVideo")]
        public bool UserHasLikedVideo { get; set; }
        public ServiceUserLikedVideoResponseDto(bool userHasLikedVideo, bool success = true) : base(success)
        {
            UserHasLikedVideo = userHasLikedVideo;
        }

        public ServiceUserLikedVideoResponseDto() {}
    }
}
