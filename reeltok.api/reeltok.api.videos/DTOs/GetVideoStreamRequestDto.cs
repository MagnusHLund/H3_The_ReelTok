using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.DTOs
{
    internal class GetVideoStreamRequestDto
    {
        internal Guid? VideoId { get; private set; }

        internal GetVideoStreamRequestDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }

}