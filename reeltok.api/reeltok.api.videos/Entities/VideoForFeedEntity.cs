using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoForFeedEntity
    {
        public Guid VideoId { get; set; }
        public VideoDetails VideoDetails { get; set; }
        public VideoLikes VideoLikes { get; set; }
        public UserDetails VideoCreatorUserDetails { get; set; }
        public Uri StreamUrl { get; set; }

        public VideoForFeedEntity(
            Guid videoId,
            VideoDetails videoDetails,
            VideoLikes videoLikes,
            UserDetails videoCreatorUserDetails,
            Uri streamUrl)
        {
            VideoId = videoId;
            VideoDetails = videoDetails;
            VideoLikes = videoLikes;
            VideoCreatorUserDetails = videoCreatorUserDetails;
            StreamUrl = streamUrl;
        }

        public VideoForFeedEntity() { }
    }
}
