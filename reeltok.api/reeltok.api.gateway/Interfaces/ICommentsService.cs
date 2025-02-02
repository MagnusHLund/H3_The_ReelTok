using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface ICommentsService
    {
        public CommentUsingDateTime AddComment(string commentText);
        public List<CommentUsingDateTime> LoadComments(Guid videoId, byte amount);
    }
}