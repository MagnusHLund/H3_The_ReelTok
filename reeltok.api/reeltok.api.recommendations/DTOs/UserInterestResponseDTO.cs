using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class UserInterestResponseDto
    {
        public Guid UserId { get; set; }
        public string UserInterest { get; set; }

        public UserInterestResponseDto(Guid userId, string userInterest)
        {
            UserId = userId;
            UserInterest = userInterest;
        }
    }
}
