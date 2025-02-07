namespace reeltok.api.gateway.ValueObjects
{
    public class VideoUpload
    {
        public VideoDetails VideoDetails { get; }
        public IFormFile VideoFile { get; }

        public VideoUpload(VideoDetails videoDetails, IFormFile videoFile)
        {
            VideoDetails = videoDetails;
            VideoFile = videoFile;
        }
    }
}