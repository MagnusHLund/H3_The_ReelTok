using Newtonsoft.Json;

namespace reeltok.api.auth.DTOs.GetUserIdByToken
{
    public class GetUserIdByTokenResponseDto : BaseResponseDto
    {
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }

        public GetUserIdByTokenResponseDto(Guid userId, bool success = true) : base(success)
        {
            UserId = userId;
        }

        public GetUserIdByTokenResponseDto() { }
    }
}
