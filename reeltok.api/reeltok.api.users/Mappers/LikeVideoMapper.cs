using reeltok.api.users.ValueObjects;
using reeltok.api.users.DTOs.LikeVideoRequests;

namespace reeltok.api.users.Mappers
{
    public static class LikeVideoMapper
    {
        public static LikedDetails ToLikeVideoFromCreateDTO(LikeVideoRequestDto requestDto)
        {
            return new LikedDetails(
                userId: requestDto.UserId,
                videoId: requestDto.VideoId
            );
        }
    }
}