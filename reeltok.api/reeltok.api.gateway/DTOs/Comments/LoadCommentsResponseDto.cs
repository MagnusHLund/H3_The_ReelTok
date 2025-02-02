using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    public class LoadCommentsResponseDto : BaseResponseDto
    {

        public List<CommentUsingDateTime> Comments { get; set; }

        public LoadCommentsResponseDto(List<CommentUsingDateTime> comments, bool success) : base(success)
        {
            Comments = comments;
        }
    }
}