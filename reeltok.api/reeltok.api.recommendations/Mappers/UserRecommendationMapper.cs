using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class UserRecommendationMapper
    {
        public static UserInterestDetails ToUserInterestDetailsFromDTO(CreateUserInterestDTO dto)
        {
            return new UserInterestDetails(dto.UserId);
        }
    }
}
