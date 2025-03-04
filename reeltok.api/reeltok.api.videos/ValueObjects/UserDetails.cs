namespace reeltok.api.videos.ValueObjects
{
    public class UserDetails
    {
        public string Username { get; }
        public string ProfileUrlPath { get; }
        public string ProfilePictureUrlPath { get; }

        public UserDetails(string username, string profileUrlPath, string profilePictureUrlPath)
        {
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}
