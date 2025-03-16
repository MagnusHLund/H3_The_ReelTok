using reeltok.api.videos.DTOs;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Utils;

namespace reeltok.api.videos.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod,
            bool isMultipartFormData = false
        ) where TResponse : BaseResponseDto
        {
            if (Equals(requestDto, default(TRequest)))
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            HttpRequestMessage request = HttpRequestFactory.CreateHttpRequest(requestDto, targetUrl, httpMethod, isMultipartFormData);

            return await SendRequestAsync<TResponse>(request).ConfigureAwait(false);
        }

        private async Task<BaseResponseDto> SendRequestAsync<TResponse>(HttpRequestMessage request)
            where TResponse : BaseResponseDto
        {
            using (request)
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
                HttpResponseUtils.HandleResponseCookies(response, _httpContextAccessor);

                return await HttpResponseUtils.DeserializeResponseAsync<TResponse>(response).ConfigureAwait(false);
            }
        }
    }
}