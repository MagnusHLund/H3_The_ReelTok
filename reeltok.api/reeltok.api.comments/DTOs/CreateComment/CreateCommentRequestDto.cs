using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.DTOs.CreateComment
{
    public class CreateCommentRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("Message")]
        public string CommentText { get; set; }

        public CreateCommentRequestDto(Guid videoId, Guid userId, string commentText)
        {
            VideoId = videoId;
            UserId = userId;
            CommentText = commentText;
        }
    }
}