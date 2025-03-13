using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.ValueObjects
{
    public class HiddenUserDetails
    {
        [Required]
        [JsonProperty("Email")]
        public string Email { get; private set; } = string.Empty;

        public HiddenUserDetails(string email)
        {
            Email = email;
        }

        private HiddenUserDetails() { }
    }
}