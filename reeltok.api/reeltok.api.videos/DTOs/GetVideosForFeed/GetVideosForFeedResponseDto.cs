using System.Xml.Serialization;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedResponseDto")]
    public class GetVideosForFeedResponseDto : BaseResponseDto
    {
        [XmlElement("Videos")]
        public List<Video> Videos { get; set; }

        public GetVideosForFeedResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}
