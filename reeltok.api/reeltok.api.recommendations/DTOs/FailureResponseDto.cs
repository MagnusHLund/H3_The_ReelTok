using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Message")]
        public string Message { get; set; }
        
        [JsonProperty("Success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
