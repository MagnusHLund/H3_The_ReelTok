using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.Entities.Users;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.Videos
{
    public class VideoForFeedEntity : BaseVideoEntity
    {
        [Required]
        [JsonProperty("VideoDetails")]
        public VideoDetails VideoDetails { get; set; }

        [Required]
        [JsonProperty("VideoLikes")]
        public VideoLikes VideoLikes { get; set; }

        [Required]
        [JsonProperty("VideoCreator")]
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
