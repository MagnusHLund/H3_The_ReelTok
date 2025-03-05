using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.ValueObjects
{
    public class HiddenUserDetails
    {
        [Required]
        public string Email { get; private set; } = string.Empty;

        public HiddenUserDetails(string email)
        {
            Email = email;
        }

        private HiddenUserDetails() { }

    }
}