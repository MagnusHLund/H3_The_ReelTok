using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class UserDetails
    {
        [Required]
        public string UserName { get; private set; } = string.Empty;

        [Required]
        public string ProfileUrl { get; private set; } = string.Empty;

        [Required]
        public string ProfilePictureUrl { get; private set; } = string.Empty;

        [Required]
        public HiddenUserDetails HiddenDetails { get; private set; }
        public UserDetails(string userName, string profileUrl, string profilePictureUrl, HiddenUserDetails hiddenDetails)
        {
            UserName = userName;
            ProfileUrl = profileUrl;
            ProfilePictureUrl = profilePictureUrl;
            HiddenDetails = hiddenDetails;
        }

        private UserDetails()
        {

        }
    }
}