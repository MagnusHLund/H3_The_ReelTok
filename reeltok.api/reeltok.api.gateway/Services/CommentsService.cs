using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Mappers;
using reeltok.api.gateway.Interfaces.Services;
using reeltok.api.gateway.Interfaces.Factories;
using reeltok.api.gateway.DTOs.Comments.AddComment;
using reeltok.api.gateway.DTOs.Comments.LoadComments;
using reeltok.api.gateway.Entities.comments;

namespace reeltok.api.gateway.Services
{
    public class CommentsService : BaseService, ICommentsService
    {
        private readonly IAuthService _authService;
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        public CommentsService(IAuthService authService, IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _authService = authService;
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task<CommentUsingDateTime> AddComment(Guid videoId, string message)
        {
            Guid userId = await _authService.GetUserIdByAccessToken().ConfigureAwait(false);

            ServiceAddCommentRequestDto requestDto = new ServiceAddCommentRequestDto(userId, videoId, message);
            Uri targetUrl = _endpointFactory.GetCommentsApiUrl("comments");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(requestDto, targetUrl, HttpMethod.Post).ConfigureAwait(false);

            if (response.Success && response is ServiceAddCommentResponseDto responseDto)
            {
                CommentUsingUnixTime comment = responseDto.Comment;
                return CommentMapper.ConvertToDateTime(comment);
            }

            throw HandleNetworkResponseExceptions(response);
        }

        public async Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount)
        {
            ServiceLoadCommentsRequestDto requestDto = new ServiceLoadCommentsRequestDto(videoId, amount);
            Uri targetUrl = _endpointFactory.GetCommentsApiUrl("comments");

            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(requestDto, targetUrl, HttpMethod.Get).ConfigureAwait(false);

            if (response.Success && response is ServiceLoadCommentsResponseDto responseDto)
            {
                List<CommentUsingUnixTime> comments = responseDto.Comments;
                return comments.Select(comment => CommentMapper.ConvertToDateTime(comment)).ToList();
            }

            throw HandleNetworkResponseExceptions(response);
        }
    }
}
