using reeltok.api.users.DTOs.LikeVideoRequests;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Mappers
{
    public static class LikeVideoMapper
    {
        public static LikedDetails ToLikeVideoFromCreateDTO(this LikeVideoRequestDto dto)
        {
            return new LikedDetails(
                dto.UserId,
                dto.VideoId
            );
        }
    }
}