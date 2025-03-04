namespace reeltok.api.videos.Entities
{
    public class VideoCreatorEntity
    {
        public Guid VideoId { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; }
        public string ProfileUrlPath { get; }
        public string ProfilePictureUrlPath { get; }

        public VideoCreatorEntity(Guid videoId, Guid userId, string username, string profileUrlPath, string profilePictureUrlPath)
        {
            VideoId = videoId;
            UserId = userId;
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}
