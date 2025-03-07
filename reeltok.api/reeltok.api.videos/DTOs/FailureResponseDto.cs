using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.videos.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Message")]
        public string Message { get; set; }

        [Required]
        [JsonProperty("Success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message, bool success = false) : base(success)
        {
            Message = message;
            Success = false;
        }
    }
}
