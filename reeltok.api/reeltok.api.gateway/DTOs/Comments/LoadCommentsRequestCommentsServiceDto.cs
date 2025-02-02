using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class LoadCommentsRequestCommentsServiceDto
    {
        public Guid VideoId { get; set; }

        public LoadCommentsRequestCommentsServiceDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}