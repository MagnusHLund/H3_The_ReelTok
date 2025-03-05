using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class VideoRecommendationMapper
    {
        public static VideoCategoryDetails ToVideoCategoryDetailsFromDTO(CreateVideoInterestDto dto)
        {
            return new VideoCategoryDetails(dto.VideoId);
        }

        public static VideoCategoryResponseDto ToVideoCategoryDTOFromEntity
            (VideoCategoryEntity videoCategory, CategoryEntity category)
        {
            return new VideoCategoryResponseDto
            (
                videoCategory.VideoCategoryDetails.VideoId,
                category.CategoryDetails.CategoryName.ToString()
            );
        }
    }
}
