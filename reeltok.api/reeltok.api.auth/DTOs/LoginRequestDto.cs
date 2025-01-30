
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal string PlainTextPassword { get; private set; }

        internal LoginRequestDto(Guid userId, string plainTextPassword) { }
    }
}