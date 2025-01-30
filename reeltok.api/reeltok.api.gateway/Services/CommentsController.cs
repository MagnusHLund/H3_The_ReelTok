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
        private readonly AuthService _authService;
        private readonly GatewayService _gatewayService;
        internal CommentsService(AuthService authService, GatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public List<Comment> LoadComments(Guid videoId, byte amount)
        {

        }
        public Comment AddComment(string commentText)
        {

        }
    }
}