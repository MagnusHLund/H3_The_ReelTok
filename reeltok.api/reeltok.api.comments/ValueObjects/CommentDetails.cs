using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.ValueObjects
{
    public class CommentDetails
    {
        [Required]
        public Guid UserId { get; private set; } = Guid.Empty;

        [Required]
        public Guid VideoId { get; private set; } = Guid.Empty;

        [Required]
        public string Message { get; private set; } = string.Empty;

        [Required]
        public uint CreatedAt { get; private set; } = 0;

        public CommentDetails(Guid userId, Guid videoId, string message, uint createdAt)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }

        private CommentDetails()
        {
        }
    }
}
