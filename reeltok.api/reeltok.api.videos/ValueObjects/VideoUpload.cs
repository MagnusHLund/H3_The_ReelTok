using System.Xml.Serialization;

namespace reeltok.api.videos.ValueObjects
{
    [XmlRoot("VideoDetails")]
    public class VideoUpload
    {
        [XmlElement("VideoDetails")]
        internal VideoDetails VideoDetails { get; }
        [XmlElement("VideoFile")]
        internal IFormFile VideoFile { get; }

        internal VideoUpload(VideoDetails videoDetails, IFormFile videoFile)
        {
            VideoDetails = videoDetails;
            VideoFile = videoFile;
        }
    }
}
