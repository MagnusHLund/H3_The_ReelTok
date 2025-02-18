using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entities
{
    public class UserCredentialsEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string Salt { get; set; }

        public UserCredentialsEntity(Guid userId, string hashedPassword, string salt)
        {
            UserId = userId;
            HashedPassword = hashedPassword;
            Salt = salt;
        }
    }
}
