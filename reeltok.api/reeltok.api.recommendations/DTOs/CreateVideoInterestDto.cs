using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class CreateVideoInterestDto : BaseResponseDto
    {
        public Guid VideoId { get; set; }
        public int CategoryId { get; set; }

        public CreateVideoInterestDto(Guid videoId, int categoryId, bool success = true) : base(success)
        {
            VideoId = videoId;
            CategoryId = categoryId;
        }

    }
}
