using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.ValueObjects
{
    public class UserDetails
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; private set; } = string.Empty;

        [JsonProperty("ProfileUrlPath")]
        public string? ProfileUrlPath { get; private set; }

        [JsonProperty("ProfilePictureUrlPath")]
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