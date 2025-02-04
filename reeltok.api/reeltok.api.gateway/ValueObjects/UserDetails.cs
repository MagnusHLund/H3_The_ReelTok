namespace reeltok.api.gateway.ValueObjects
{
    public class UserDetails
    {
        public string Username { get; }
        public string ProfilePictureUrl { get; }
        public string ProfileUrl { get; }

        public UserDetails(string username, string profilePictureUrl, string profileUrl)
        {
            Username = username;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
        }
    }
}