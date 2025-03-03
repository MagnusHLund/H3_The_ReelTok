namespace reeltok.api.videos.Entities
{
    public class ProfileVideoEntity
    {
        public Guid VideoId { get; set; }
        public string StreamPath { get; set; }
        public uint UploadedAt { get; set; }

        public ProfileVideoEntity(Guid videoId, string streamPath, uint uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}