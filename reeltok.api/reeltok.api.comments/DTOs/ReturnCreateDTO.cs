using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.comments.DTOs
{
    public class ReturnCreateDTO
    {

        [Required]
        public int CommentId { get; set; }
        [Required]
        public Guid UserId { get; set; } = Guid.Empty;

        [Required]
        public Guid VideoId { get; set; } = Guid.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        [Required]
        public uint CreatedAt { get; set; } = 0;

        public ReturnCreateDTO(int commentId, Guid userId, Guid videoId, string message, uint createdAt)
        {
            CommentId = commentId;
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }

        public ReturnCreateDTO()
        {

        }
    }
}
