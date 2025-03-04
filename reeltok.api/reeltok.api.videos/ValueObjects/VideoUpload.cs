using System.Xml.Serialization;

namespace reeltok.api.videos.ValueObjects
{
    [XmlRoot("VideoUpload")]
    public class VideoUpload
    {
        [XmlElement("VideoDetails")]
        public VideoDetails VideoDetails { get; }
        [XmlElement("VideoFile")]
        public IFormFile VideoFile { get; }

        public VideoUpload(VideoDetails videoDetails, IFormFile videoFile)
        {
            VideoDetails = videoDetails;
            VideoFile = videoFile;
        }
    }
}
