using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public abstract class BaseComment
    {
        public Guid CommentId { get; set; }

        protected BaseComment(Guid commentId)
        {
            CommentId = commentId;
        }
    }
}
