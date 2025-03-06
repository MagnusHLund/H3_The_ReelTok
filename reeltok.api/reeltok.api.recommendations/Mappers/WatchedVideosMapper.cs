using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Mappers
{
    public static class WatchedVideosMapper
    {
        public static WatchedVideoDetails ToEntity(CreateWatchedVideoDto createWatchedVideoDto)
        {
            return new WatchedVideoDetails
            (
                createWatchedVideoDto.UserId,
                createWatchedVideoDto.VideoId,
                createWatchedVideoDto.TimeWatched
            );
        }
    }
}
