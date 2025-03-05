using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class UserInterestResponseDTO
    {
        public Guid UserId { get; set; }
        public string UserInterest { get; set; }

        public UserInterestResponseDTO(Guid userId, string userInterest)
        {
            UserId = userId;
            UserInterest = userInterest;
        }
    }
}
