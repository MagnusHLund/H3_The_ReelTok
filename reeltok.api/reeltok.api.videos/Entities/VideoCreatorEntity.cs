namespace reeltok.api.videos.Entities
{
    public class VideoCreatorEntity : UserEntity
    {
        public Guid VideoId { get; set; }

        public VideoCreatorEntity(
            Guid videoId,
            Guid userId,
            string username,
            string profileUrlPath,
            string profilePictureUrlPath
            ) : base(userId, username, profileUrlPath, profilePictureUrlPath)
        {
            VideoId = videoId;
            UserId = userId;
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}
