using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        internal CommentsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public List<Comment> LoadComments(Guid videoId, byte amount)
        {
            return new List<Comment>() {
                new Comment() {}
            };
        }
        public Comment AddComment(string commentText)
        {
            return new Comment();
        }
    }
}