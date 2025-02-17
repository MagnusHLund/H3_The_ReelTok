using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Comments;

namespace reeltok.api.gateway.Services
{
    internal class CommentsService : BaseService, ICommentsService
    {
        private const string CommentsMicroServiceBaseUrl = "http://localhost:5005/api/comments";
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        internal CommentsService(IAuthService authService, IHttpService httpService)
        {
            _authService = authService;
            _httpService = httpService;
        }

        public async Task<CommentUsingDateTime> AddComment(Guid videoId, string commentText)
        {
            Guid userId = await _authService.GetUserIdByToken().ConfigureAwait(false);

            ServiceAddCommentRequestDto requestDto = new ServiceAddCommentRequestDto(userId, videoId, commentText);
            Uri targetUrl = new Uri($"{CommentsMicroServiceBaseUrl}/add");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceAddCommentResponseDto responseDto)
            {
                return CommentMapper.ConvertResponseDtoToCommentUsingDateTime<ServiceAddCommentResponseDto>(responseDto);
            }

            throw HandleExceptions(response);
        }

        public async Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount)
        {
            ServiceLoadCommentsRequestDto requestDto = new ServiceLoadCommentsRequestDto(videoId, amount);
            Uri targetUrl = new Uri($"{CommentsMicroServiceBaseUrl}/load");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceLoadCommentsResponseDto responseDto)
            {
                return responseDto.Comments.Select(comment => CommentMapper.ConvertToDateTime(comment)).ToList();
            }

            throw HandleExceptions(response);
        }
    }
}
