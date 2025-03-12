using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class ServiceAddCommentRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        [Required]
        [Range(1, 1024)]
        public string Message { get; set; }

        public ServiceAddCommentRequestDto(Guid userId, Guid videoId, string message)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
        }
    }
}
