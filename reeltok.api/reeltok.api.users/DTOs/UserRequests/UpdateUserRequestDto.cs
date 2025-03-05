using System.ComponentModel.DataAnnotations;

namespace reeltok.api.users.DTOs.UserRequests
{
    public class UpdateUserRequestDto
    {
        [Required]
        public Guid UserId { get; }
        public string? Username { get; }
        public string? Email { get; }

        public UpdateUserRequestDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}