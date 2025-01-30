

using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    internal class DeleteUserRequestDto
    {
        [Required]
        internal Guid UserId { get; private set; }

        internal DeleteUserRequestDto(Guid userId) { }
    }
}