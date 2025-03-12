namespace reeltok.api.gateway.Entities
{
    public class VideoCreatorEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string ProfileUrlPath { get; set; }
        public string ProfilePictureUrlPath { get; set; }

        public VideoCreatorEntity(Guid userId, string username, string profileUrlPath, string profilePictureUrlPath)
        {
            UserId = userId;
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}
