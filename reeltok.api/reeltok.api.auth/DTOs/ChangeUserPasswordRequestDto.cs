

using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    internal class ChangeUserPasswordRequestDto
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal string NewPlainTextPassword { get; private set; }
        
        internal ChangeUserPasswordRequestDto(Guid userId, string newPlainTextPassword){}

    }
}