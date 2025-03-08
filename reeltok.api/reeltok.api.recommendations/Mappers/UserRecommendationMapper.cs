using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class UserRecommendationMapper
    {
        public static UserInterestDetails ToUserInterestDetailsFromDTO(AddVideoRecommendationResponseDto dto)
        {
            return new UserInterestDetails(dto.UserId);
        }

    }
}
