using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class CommentsService : ICommentsService
    {
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        internal CommentsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public CommentUsingDateTime AddComment(string commentText)
        {
            return new CommentUsingDateTime(Guid.Empty, new CommentDetailsUsingDateTime(Guid.Empty, Guid.Empty, commentText, DateTime.Now));
        }

        public List<CommentUsingDateTime> LoadComments(Guid videoId, byte amount)
        {
            return new List<CommentUsingDateTime>() {
                new CommentUsingDateTime(Guid.Empty, new CommentDetailsUsingDateTime(Guid.Empty, Guid.Empty, "", DateTime.Now)) {}
            };
        }
    }
}