using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces.Services;

namespace reeltok.api.gateway.Services
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
            ForwardCookies(request);

            return await SendRequestAsync<TResponse>(request).ConfigureAwait(false);
        }

        private void ForwardCookies(HttpRequestMessage request)
        {
            IRequestCookieCollection? cookies = _httpContextAccessor.HttpContext?.Request.Cookies;
            if (cookies != null)
            {
                foreach (var cookie in cookies)
                {
                    request.Headers.Add("Cookie", $"{cookie.Key}={cookie.Value}");
                }
            }
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