namespace reeltok.api.videos.Entities
{
    public class HasUserLikedVideoEntity
    {
        public Guid VideoId { get; set; }
        public bool HasUserLikedVideo { get; set; }

        public HasUserLikedVideoEntity(Guid videoId, bool hasUserLikedVideo)
        {
            VideoId = videoId;
            HasUserLikedVideo = hasUserLikedVideo;
        }
    }
}