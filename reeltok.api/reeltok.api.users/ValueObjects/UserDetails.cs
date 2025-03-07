using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class UserDetails
    {
        [Required]
        public string Username { get; private set; } = string.Empty;
        public string? ProfileUrlPath { get; private set; }
        public string? ProfilePictureUrlPath { get; private set; }

        public UserDetails(string username, string? profileUrlPath, string? profilePictureUrlPath)
        {
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }

        private UserDetails() { }
    }
}