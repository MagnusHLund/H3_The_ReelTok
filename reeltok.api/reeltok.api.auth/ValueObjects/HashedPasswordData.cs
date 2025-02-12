using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.ValueObjects
{
    public class HashedPasswordData
    {
        [Required]
        public string Password { get; private set; }

        [Required]
        public string Salt { get; private set  ; }

        public HashedPasswordData(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
