using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.users.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("message")]
        public string Message { get; set; }

        [Required]
        [JsonProperty("success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }
    }
}