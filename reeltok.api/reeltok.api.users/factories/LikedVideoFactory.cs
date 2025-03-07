using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.factories
{
    internal static class LikedVideoFactory
    {
        internal static LikedVideoEntity CreateLikedVideoEntity(LikedDetails likedDetails)
        {
            return new LikedVideoEntity(
                userId: likedDetails.UserId,
                videoId: likedDetails.VideoId
            );
        }
    }
}