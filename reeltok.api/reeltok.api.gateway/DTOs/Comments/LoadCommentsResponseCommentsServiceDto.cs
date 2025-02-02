using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class LoadCommentsResponseCommentsServiceDto : BaseResponseDto
    {
        public List<CommentUsingUnixTime> Comments { get; set; }

        public LoadCommentsResponseCommentsServiceDto(List<CommentUsingUnixTime> comments, bool success) : base(success)
        {
            Comments = comments;
        }
    }
}