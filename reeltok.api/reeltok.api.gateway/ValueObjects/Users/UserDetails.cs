using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class UserDetails
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; }

        [JsonProperty("ProfilePictureUrlPath")]
        public string? ProfilePictureUrl { get; set; }

        [JsonProperty("ProfileUrlPath")]
        public string? ProfileUrl { get; set; }

        public UserDetails(string username, string? profilePictureUrl, string? profileUrl)
        {
            Username = username;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
        }
    }
}
