using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface ICommentsService
    {
        public Comment AddComment(string commentText);
        public List<Comment> LoadComments(Guid videoId, byte amount);
    }
}