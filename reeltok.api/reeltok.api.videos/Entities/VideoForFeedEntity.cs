using Newtonsoft.Json;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoForFeedEntity : BaseVideoEntity
    {
        [JsonProperty("VideoDetails")]
        public VideoDetails VideoDetails { get; set; }

        [JsonProperty("VideoLikes")]
        public VideoLikes VideoLikes { get; set; }

        [JsonProperty("User")]
        public UserEntity VideoCreator { get; set; }

        public VideoForFeedEntity(
            Guid videoId,
            VideoDetails videoDetails,
            VideoLikes videoLikes,
            UserEntity videoCreator,
            string streamPath,
            long uploadedAt
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
