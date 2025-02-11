using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    public class DeleteUserRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        public DeleteUserRequestDto(Guid userId)
        {
            UserId = userId;
        }

        public DeleteUserRequestDto() { }
    }
}
