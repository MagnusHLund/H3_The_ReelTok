using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.auth.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Message")]
        public string Message { get; set; }

        [Required]
        [JsonProperty("Success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
