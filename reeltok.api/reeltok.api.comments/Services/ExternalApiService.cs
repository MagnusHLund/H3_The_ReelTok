using reeltok.api.comments.DTOs;
using reeltok.api.comments.Exceptions;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.Interfaces.Factories;
using reeltok.api.comments.DTOs.DoesVideoIdExist;

namespace reeltok.api.comments.Services
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly IHttpService _httpService;
        private readonly IEndpointFactory _endpointFactory;

        public ExternalApiService(IHttpService httpService, IEndpointFactory endpointFactory)
        {
            _httpService = httpService;
            _endpointFactory = endpointFactory;
        }

        public async Task EnsureVideoIdExistAsync(Guid videoId)
        {
            VideosServiceDoesVideoIdExistRequestDto requestDto = new VideosServiceDoesVideoIdExistRequestDto(videoId);
            Uri targetUrl = _endpointFactory.GetVideosApiUrl("videos");

            BaseResponseDto response = await _httpService.ProcessRequestAsync
                <VideosServiceDoesVideoIdExistRequestDto, VideosServiceDoesVideoIdExistResponseDto>(
                requestDto, targetUrl, HttpMethod.Get)
                .ConfigureAwait(false);

            if (response.Success && response is VideosServiceDoesVideoIdExistResponseDto)
            {
                return;
            }

            throw HandleNetworkResponseExceptions(response);
        }

        private static Exception HandleNetworkResponseExceptions(BaseResponseDto response)
        {
            if (response is FailureResponseDto failureResponse)
            {
                return new FailureNetworkResponseException(failureResponse.Message);
            }

            return new InvalidOperationException("An unknown error has occurred!");
        }
    }
}