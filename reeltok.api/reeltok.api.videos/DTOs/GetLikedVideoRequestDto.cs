using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.DTOs
{
    internal class GetLikedVideoRequestDto
    {
        internal Guid UserId { get; private set; }
        internal byte AmountOfVideos { get; private set; }

        internal GetLikedVideoRequestDto(Guid userId, byte amountOfVideos){
            UserId = userId;
            AmountOfVideos = amountOfVideos;
        }
    }
}