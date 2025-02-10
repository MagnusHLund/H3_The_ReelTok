using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entites
{
    public class Auth
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string Salt { get; set; }

        public Auth(Guid userId, string hashedPassword, string salt)
        {
            UserId = userId;
            HashedPassword = hashedPassword;
            Salt = salt;
        }
    }
}
