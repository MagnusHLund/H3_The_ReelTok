using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class UserDetails
    {
        [Required]
        [JsonProperty("Username")]
        public string Username { get; }

        [JsonProperty("ProfilePictureUrl")]
        public string? ProfilePictureUrl { get; }

        [JsonProperty("ProfileUrl")]
        public string? ProfileUrl { get; }

        public UserDetails(string username, string? profilePictureUrl, string? profileUrl)
        {
            Username = username;
            ProfilePictureUrl = profilePictureUrl;
            ProfileUrl = profileUrl;
        }
    }
}
