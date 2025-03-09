using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Utils;

namespace reeltok.api.recommendations.Factories
{
    internal class WatchedVideoFactory
    {
        internal static WatchedVideoEntity CreateWatchedVideoEntity(Guid userId, Guid videoId)
        {
            return new WatchedVideoEntity
            (
                userId: userId,
                videoId: videoId,
                lastWatchedAt: DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow),
                watchCount: 1
            );
        }
    }
}