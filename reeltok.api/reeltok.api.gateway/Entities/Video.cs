using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public class Video
    {
        public Guid VideoId { get; set; }
        public VideoDetails VideoDetails { get; set; }
        public uint Likes { get; set; }
        public bool HasLiked { get; set; }
        public string StreamUrl { get; set; }
        public DateTime UploadDate { get; set; }
        public UserDetails CreatorDetails { get; set; }

        public Video(Guid videoId, VideoDetails videoDetails, uint likes, bool hasLiked, string streamUrl, DateTime uploadDate, UserDetails creatorDetails)
        {
            VideoId = videoId;
            VideoDetails = videoDetails;
            Likes = likes;
            HasLiked = hasLiked;
            StreamUrl = streamUrl;
            UploadDate = uploadDate;
            CreatorDetails = creatorDetails;
        }
    }
}