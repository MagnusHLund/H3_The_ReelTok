using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entities
{
    public class UserAuthentication
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string Salt { get; set; }

        public UserAuthentication(Guid userId, string hashedPassword, string salt)
        {
            UserId = userId;
            HashedPassword = hashedPassword;
            Salt = salt;
        }
    }
}
