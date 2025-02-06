using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Comments;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class CommentsService : BaseService, ICommentsService
    {
        private const string CommentsMicroServiceBaseUrl = "http://localhost:5005/comments";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        internal CommentsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public async Task<CommentUsingDateTime> AddComment(Guid videoId, string commentText)
        {
            Guid userId = await _authService.GetUserIdByToken();

            ServiceAddCommentRequestDto requestDto = new ServiceAddCommentRequestDto(userId, videoId, commentText);
            string targetUrl = $"{CommentsMicroServiceBaseUrl}/Add";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            if (response.Success && response is ServiceAddCommentResponseDto responseDto)
            {
                DateTime createdAt = DateTimeUtils.UnixTimeToDateTime(responseDto.CreatedAt);
                CommentDetailsUsingDateTime commentDetails = new CommentDetailsUsingDateTime(responseDto.UserId, videoId, responseDto.CommentText, createdAt);

                return new CommentUsingDateTime(responseDto.CommentId, commentDetails);
            }

            throw HandleExceptions(response);
        }

        public async Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount)
        {
            ServiceLoadCommentsRequestDto requestDto = new ServiceLoadCommentsRequestDto(videoId, amount);
            string targetUrl = $"{CommentsMicroServiceBaseUrl}/Load";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            if (response.Success && response is ServiceLoadCommentsResponseDto responseDto)
            {
                return responseDto.Comments.Select(comment => CommentMapper.ConvertToDateTime(comment)).ToList();
            }

            throw HandleExceptions(response);
        }
    }
}