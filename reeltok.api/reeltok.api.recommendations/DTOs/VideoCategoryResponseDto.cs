using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class VideoCategoryResponseDto : BaseResponseDto
    {
        public Guid VideoId { get; set; }
        public string Category { get; set; }
        public VideoCategoryResponseDto(Guid videoId, string category, bool success = true) : base(success)
        {
            VideoId = videoId;
            Category = category;
        }
    }
}
