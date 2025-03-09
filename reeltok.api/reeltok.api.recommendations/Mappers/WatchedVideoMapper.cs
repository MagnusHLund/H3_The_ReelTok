using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Mappers
{
    internal static class WatchedVideoMapper
    {
        internal static List<Guid> ConvertWatchedVideoEntityListToVideoIdList(List<WatchedVideoEntity> watchedVideoEntities)
        {
            List<Guid> existingVideoIds = watchedVideoEntities.Select(wv => wv.VideoId).ToList();

            return existingVideoIds;
        }
    }
}
