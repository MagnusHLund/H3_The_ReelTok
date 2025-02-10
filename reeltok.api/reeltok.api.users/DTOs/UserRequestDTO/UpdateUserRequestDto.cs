using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    public class UpdateUserRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public string UserName { get; }

        [Required]
        public string Email { get; }

        public UpdateUserRequestDto(Guid userId, string userName, string email)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
        }
    }
}