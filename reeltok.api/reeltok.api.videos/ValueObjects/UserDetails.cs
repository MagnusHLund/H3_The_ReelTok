using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.videos.ValueObjects
{
    public class UserDetails
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; }

        [Required]
        [JsonProperty("ProfileUrlPath")]
        public string ProfileUrlPath { get; }

        [JsonProperty("ProfilePictureUrlPath")]
        public string ProfilePictureUrlPath { get; }

        public UserDetails(string username, string profileUrlPath, string profilePictureUrlPath)
        {
            Username = username;
            ProfileUrlPath = profileUrlPath;
            ProfilePictureUrlPath = profilePictureUrlPath;
        }
    }
}
