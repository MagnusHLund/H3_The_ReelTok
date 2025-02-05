using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedResponseDto")]
    public class GatewayGetVideosForFeedResponseDto : BaseResponseDto
    {
        public List<Video> Videos { get; set; }

        public GatewayGetVideosForFeedResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}