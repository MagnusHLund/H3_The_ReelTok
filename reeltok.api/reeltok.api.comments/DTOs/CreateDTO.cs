using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.comments.DTOs
{
    public class CreateDTO
    {
        [Required]
        public Guid UserId { get; private set; } = Guid.Empty;

        [Required]
        public Guid VideoId { get; private set; } = Guid.Empty;

        [Required]
        public string Message { get; private set; } = string.Empty;

        [Required]
        public uint CreatedAt { get; private set; } = 0;

        public CreateDTO(Guid userId, Guid videoId, string message, uint createdAt)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }
    }
}
