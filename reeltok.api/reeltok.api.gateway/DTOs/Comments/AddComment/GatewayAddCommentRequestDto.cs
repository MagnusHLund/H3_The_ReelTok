using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class GatewayAddCommentRequestDto
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [StringLength(1024, MinimumLength = 1)]
        [JsonProperty("Message")]
        public string Message { get; set; }

        public GatewayAddCommentRequestDto(Guid videoId, string message)
        {
            VideoId = videoId;
            Message = message;
        }
    }
}
