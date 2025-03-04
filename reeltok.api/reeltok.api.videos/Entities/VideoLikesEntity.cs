using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoLikesEntity
    {
        public Guid VideoId { get; set; }
        public VideoLikes VideoLikes { get; set; }

        public VideoLikesEntity(Guid videoId, VideoLikes videoLikes)
        {
            VideoId = videoId;
            VideoLikes = videoLikes;
        }

        public VideoLikesEntity() { }
    }
}
