using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class CreateWatchedVideoDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public ushort TimeWatched { get; set; }

        public CreateWatchedVideoDto(Guid userId, Guid videoId, ushort timeWatched, bool success = true) : base(success)
        {
            UserId = userId;
            VideoId = videoId;
            TimeWatched = timeWatched;
        }
    }
}
