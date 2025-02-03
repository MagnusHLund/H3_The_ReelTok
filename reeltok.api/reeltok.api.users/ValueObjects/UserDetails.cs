using System.ComponentModel.DataAnnotations;
using reeltok.api.users.Entities.ValueObjects;

namespace reeltok.api.users.ValueObjects
{
    public class UserDetails
    {
        [Required]
        public string UserName { get; } = string.Empty;

        [Required]
        public string ProfileUrl { get; } = string.Empty;

        [Required]
        public string ProfilePictureUrl { get; } = string.Empty;

        [Required]
        public HiddenUserDetails HiddenDetails { get; }
        public UserDetails(string userName, string profileUrl, string profilePictureUrl, HiddenUserDetails hiddenDetails)
        {
            UserName = userName;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
            HiddenDetails = hiddenDetails;
        }
    }
}