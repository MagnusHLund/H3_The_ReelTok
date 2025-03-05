using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class UserRecommendationMapper
    {
        public static UserInterestDetails ToUserInterestDetailsFromDTO(CreateUserInterestDTO dto)
        {
            return new UserInterestDetails(dto.UserId);
        }

        public static UserInterestResponseDTO ToUserInterestDTOFromEntity
            (UserInterestEntity userInterest, CategoryEntity category)
        {
            return new UserInterestResponseDTO
            (
                userInterest.UserInterestDetails.UserId,
                category.CategoryDetails.CategoryName.ToString()
            );
        }
    }
}
