using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.DTOs
{
    public class ReadDTO
    {
        [Required]
        public Guid UserId { get; private set; } = Guid.Empty;

        [Required]
        public string Message { get; private set; } = string.Empty;

        [Required]
        public uint CreatedAt { get; private set; } = 0;

        public ReadDTO(Guid userId, string message, uint createdAt)
        {
            UserId = userId;
            Message = message;
            CreatedAt = createdAt;
        }
    }
}
