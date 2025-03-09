using System.ComponentModel.DataAnnotations;

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
