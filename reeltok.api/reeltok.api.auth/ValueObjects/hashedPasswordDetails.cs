using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
    public class HashedPasswordDetails
    {
        [Required]
        public string Password { get; private set; }

        [Required]
        public string Salt { get; private set; }

        public HashedPasswordDetails(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
