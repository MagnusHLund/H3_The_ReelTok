using reeltok.api.users.ValueObjects;
using reeltok.api.users.DTOs.LikeVideo;

namespace reeltok.api.users.Mappers
{
    internal static class LikesMapper
    {
        internal static LikedDetails ConvertLikeVideoRequestDtoToLikedDetails(LikeVideoRequestDto requestDto)
        {
            return new LikedDetails(
                userId: requestDto.UserId,
                videoId: requestDto.VideoId
            );
        }
    }
}