using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoCreatorDetailsEntity
    {
        public Guid VideoId { get; set; }
        public UserDetails UserDetails { get; set; }

        public VideoCreatorDetailsEntity(Guid videoId, UserDetails userDetails)
        {
            VideoId = videoId;
            UserDetails = userDetails;
        }
    }
}
