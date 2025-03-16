using reeltok.api.comments.DTOs;
using reeltok.api.comments.Utils;
using reeltok.api.comments.Factories;
using reeltok.api.comments.Interfaces.Services;

namespace reeltok.api.comments.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

                return await HttpResponseUtils.DeserializeResponseAsync<TResponse>(response).ConfigureAwait(false);
            }
        }
    }
}