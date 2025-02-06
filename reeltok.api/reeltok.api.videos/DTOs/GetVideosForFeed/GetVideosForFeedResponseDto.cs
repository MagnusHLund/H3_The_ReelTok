using System.Xml.Serialization;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedResponseDto")]
    internal class GetVideosForFeedResponseDto : BaseResponseDto
    {
        [XmlElement("Videos")]
        internal List<Video> Videos { get; set; }

        internal GetVideosForFeedResponseDto(List<Video> videos, bool success = true) : base(success)
        {
            Videos = videos;
        }
    }
}