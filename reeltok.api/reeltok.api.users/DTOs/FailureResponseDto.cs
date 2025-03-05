using Newtonsoft.Json;

namespace reeltok.api.users.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }

        public FailureResponseDto() { }
    }
}