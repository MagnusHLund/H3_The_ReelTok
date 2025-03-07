using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.CreateUser
{
    public class RecommendationsServiceCreateUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public byte Interests { get; set; }

        public RecommendationsServiceCreateUserRequestDto(Guid userId, byte interests)
        {
            UserId = userId;
            Interests = interests;
        }
    }
}