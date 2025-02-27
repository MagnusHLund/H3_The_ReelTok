using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reeltok.api.users.ValueObjects
{
    public class HiddenUserDetails
    {
        [Required]
        [Column("Email", TypeName = "nvarchar(100)")]
        public string Email { get; private set; } = string.Empty;

        public HiddenUserDetails(string email)
        {
            Email = email;
        }

        private HiddenUserDetails() { }

    }
}