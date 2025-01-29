using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.DTOs
{
    internal class GetVideosForFeedRequestDto
    {
        internal Guid UserId { get; private set; }
        internal byte AmountOfVideos { get; private set; }

        internal GetVideosForFeedRequestDto(Guid userId, Byte amountOfVideos){
            UserId = userId;
            AmountOfVideos = amountOfVideos;
        }
    }
}