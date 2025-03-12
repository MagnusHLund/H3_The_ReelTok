namespace reeltok.api.gateway.Entities
{
    public class BaseVideoEntity
    {
        public Guid VideoId { get; set; }
        public string StreamPath { get; set; }
        public uint UploadedAt { get; set; }

        protected BaseVideoEntity(Guid videoId, string streamPath, uint uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}
