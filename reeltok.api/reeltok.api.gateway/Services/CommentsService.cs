using System.Threading.Tasks;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Comments;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Mapper;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Services
{
    internal class CommentsService : ICommentsService
    {
        private const string CommentMicroServiceBaseUrl = "http://localhost:5005/comments";
        private readonly IAuthService _authService;
        private readonly IGatewayService _gatewayService;
        internal CommentsService(IAuthService authService, IGatewayService gatewayService)
        {
            _authService = authService;
            _gatewayService = gatewayService;
        }

        public async Task<CommentUsingDateTime> AddComment(Guid videoId, string commentText)
        {
            if (videoId.Equals(Guid.Empty))
            {
                throw new InvalidOperationException("Video does not exist!");
            }

            if (commentText.Equals(string.Empty))
            {
                throw new InvalidOperationException("Comment cannot be empty!");
            }

            Guid userId = await _authService.GetUserIdByToken();

            AddCommentRequestCommentsServiceDto requestDto = new AddCommentRequestCommentsServiceDto(userId, videoId, commentText);
            string targetUri = $"{CommentMicroServiceBaseUrl}/Add";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<AddCommentRequestCommentsServiceDto, AddCommentResponseCommentsServiceDto>(requestDto, targetUri, HttpMethod.Post);

            if (response.Success && response is AddCommentResponseCommentsServiceDto responseDto)
            {
                DateTime createdAt = DateTimeUtils.UnixTimeToDateTime(responseDto.CreatedAt);
                CommentDetailsUsingDateTime commentDetails = new CommentDetailsUsingDateTime(responseDto.UserId, videoId, responseDto.CommentText, createdAt);

                return new CommentUsingDateTime(responseDto.CommentId, commentDetails);
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }

        public async Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount)
        {
            if (videoId.Equals(Guid.Empty))
            {
                throw new InvalidOperationException("Video does not exist!");
            }

            if (amount < 0)
            {
                throw new InvalidOperationException("Invalid comment amount");
            }

            LoadCommentsRequestCommentsServiceDto request = new LoadCommentsRequestCommentsServiceDto(videoId);
            string targetUri = $"{CommentMicroServiceBaseUrl}/Load";

            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<LoadCommentsRequestCommentsServiceDto, LoadCommentsResponseCommentsServiceDto>(request, targetUri, HttpMethod.Get);

            if (response.Success && response is LoadCommentsResponseCommentsServiceDto responseDto)
            {
                return (List<CommentUsingDateTime>)responseDto.Comments
                    .Select(comment => CommentMapper.ConvertToDateTime(comment));
            }

            if (response is FailureResponseDto failureResponse)
            {
                throw new InvalidOperationException(failureResponse.Message);
            }

            throw new InvalidOperationException("An unknown error has occurred!");
        }
    }
}