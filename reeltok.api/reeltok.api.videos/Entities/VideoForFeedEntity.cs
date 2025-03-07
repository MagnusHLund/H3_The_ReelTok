using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoForFeedEntity : BaseVideoEntity
    {
        public VideoDetails VideoDetails { get; set; }
        public VideoLikes VideoLikes { get; set; }
        public UserEntity VideoCreator { get; set; }

        public VideoForFeedEntity(
            Guid videoId,
            VideoDetails videoDetails,
            VideoLikes videoLikes,
            UserEntity videoCreator,
            string streamPath,
            uint uploadedAt
            ) : base(videoId, streamPath, uploadedAt)
        {
            VideoId = videoId;
            VideoDetails = videoDetails;
            VideoLikes = videoLikes;
            VideoCreator = videoCreator;
            StreamPath = streamPath;
        }
    }
}
