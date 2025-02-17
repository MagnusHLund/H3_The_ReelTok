
using System.Xml.Serialization;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class UploadVideoResponseDto : BaseResponseDto
    {
        [XmlElement("Video")]
        public VideoEntity Video;

        public UploadVideoResponseDto(VideoEntity video, bool success)
        {
            Video = video;
        }
    }
}
