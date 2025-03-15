using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class ServiceAddCommentRequestDto
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("Message")]
        public string Message { get; set; }

        public ServiceAddCommentRequestDto(Guid userId, Guid videoId, string message)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
        }
    }
}
