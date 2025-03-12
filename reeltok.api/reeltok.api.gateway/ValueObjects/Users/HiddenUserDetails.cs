using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class HiddenUserDetails
    {
        [Required]
        [JsonProperty("Email")]
        public string Email { get; }

        public HiddenUserDetails(string email)
        {
            Email = email;
        }
    }
}
