using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class WatchedVideosMapper
    {
        public static WatchedVideoDetails ToEntity(CreateWatchedVideoDto createWatchedVideoDto)
        {
            uint unixTimestamp = Convert.ToUInt32(Math.Floor(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));

            return new WatchedVideoDetails
            (
                createWatchedVideoDto.UserId,
                createWatchedVideoDto.VideoId,
                createWatchedVideoDto.TimeWatched,
                unixTimestamp
            );
        }
    }
}
