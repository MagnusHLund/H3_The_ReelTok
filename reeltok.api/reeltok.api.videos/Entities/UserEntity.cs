namespace reeltok.api.videos.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string ProfileUrlPath { get; set; }
        public string ProfilePictureUrlPath { get; set; }

        public UserEntity(Guid userId, string username, string profileUrlPath, string profilePictureUrlPath)
        {
            UserId = userId;
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}