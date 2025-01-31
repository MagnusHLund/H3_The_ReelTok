using System.ComponentModel.DataAnnotations;


namespace reeltok.api.users.DTOs
{
    internal class DeleteUserRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal DeleteUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}