using reeltok.api.auth.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.Entities
{
    public class UserCredentialsEntity
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public HashedPasswordDetails HashedPasswordDetails { get; set; }

        public UserCredentialsEntity(Guid userId, HashedPasswordDetails hashedPasswordDetails)
        {
            UserId = userId;
            HashedPasswordDetails = hashedPasswordDetails;
        }

        private UserCredentialsEntity() { }
    }
}
