using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedResponseDto")]
    public class ServiceGetVideosForFeedResponseDto : BaseResponseDto
    {
        public List<Video> Videos { get; set; }

        public ServiceGetVideosForFeedResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }

        public ServiceGetVideosForFeedResponseDto() { }
    }
}