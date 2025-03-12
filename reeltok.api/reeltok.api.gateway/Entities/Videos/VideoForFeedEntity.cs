using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public class VideoForFeedEntity : BaseVideoEntity
    {
        public VideoDetails VideoDetails { get; set; }
        public VideoLikes VideoLikes { get; set; }
        public VideoForFeedEntity VideoCreator { get; set; }

        public VideoForFeedEntity(
            Guid videoId,
            VideoDetails videoDetails,
            VideoLikes videoLikes,
            VideoForFeedEntity videoCreator,
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
