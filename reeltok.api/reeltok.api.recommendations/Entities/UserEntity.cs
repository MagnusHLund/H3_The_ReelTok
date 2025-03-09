using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class UserEntity
    {
        [Key]
        public uint UserInterestId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public UserEntity(Guid userId)
        {
            UserId = userId;
        }

        private UserEntity() { }
    }
}
