using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using reeltok.api.videos.Entities;

namespace reeltok.api.videos.DTOs.UploadVideo
{
    [XmlRoot("UploadVideoResponseDto")]
    public class UploadVideoResponseDto : BaseResponseDto
    {
        [XmlElement("Video")]
        public Video Video;

        public UploadVideoResponseDto(Video video, bool success)
        {
            Video = video;
        }
    }
}
