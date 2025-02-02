using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class LoadCommentsRequestDto
    {
        public Guid VideoId { get; set; }

        public LoadCommentsRequestDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}