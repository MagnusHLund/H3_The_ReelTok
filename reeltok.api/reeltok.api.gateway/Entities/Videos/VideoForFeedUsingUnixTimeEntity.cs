using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.Entities.Videos
{
    public class VideoForFeedUsingUnixTimeEntity : BaseVideoUsingUnixTimeEntity
    {
        [Required]
        [JsonProperty("VideoDetails")]
        public VideoDetails VideoDetails { get; set; }

        [Required]
        [JsonProperty("VideoLikes")]
        public VideoLikes VideoLikes { get; set; }

        [Required]
        [JsonProperty("User")]
        public UserEntity VideoCreator { get; set; }

        public VideoForFeedUsingUnixTimeEntity(
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
