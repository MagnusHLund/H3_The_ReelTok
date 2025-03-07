using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class UserInterestResponseDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public string UserInterest { get; set; }

        public UserInterestResponseDto(Guid userId, string userInterest, bool success = true) : base(success)
        {
            UserId = userId;
            UserInterest = userInterest;
        }
    }
}
