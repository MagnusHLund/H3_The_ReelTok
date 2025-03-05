using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class UserDetails
    {
        [Required]
        public string Username { get; private set; } = string.Empty;

        [Required] // TODO: Do we even really need ProfileUrl? we have their UserId. The profile page can be their userId.
        public string ProfileUrl { get; private set; } = string.Empty;
        public string? ProfilePictureUrl { get; private set; }

        public UserDetails(string username, string profileUrl, string? profilePictureUrl)
        {
            Username = username;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
        }

        private UserDetails() { }
    }
}