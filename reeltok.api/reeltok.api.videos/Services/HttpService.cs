using System.Text;
using System.Text.Json;
using System.Reflection;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;

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

            HttpRequestMessage request = CreateHttpRequest(requestDto, targetUrl, httpMethod, isMultipartFormData);
            ForwardCookies(request);

            return await SendRequestAsync<TResponse>(request).ConfigureAwait(false);
        }

        private static HttpRequestMessage CreateHttpRequest<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod,
            bool isMultipartFormData
        )
        {
            if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
            {
                return PrepareHttpRequestWithQueryParameters(requestDto, targetUrl);
            }
            else if (isMultipartFormData)
            {
                return PrepareMultipartFormDataRequest(requestDto, targetUrl, httpMethod);
            }
            else
            {
                return PrepareHttpRequestBody(requestDto, targetUrl, httpMethod);
            }
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
                return await DeserializeResponseAsync<TResponse>(response).ConfigureAwait(false);
            }
        }

        private static HttpRequestMessage PrepareHttpRequestBody<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod
        )
        {
            string requestContent = JsonSerializer.Serialize(requestDto);
            return new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = CreateStringContent(requestContent)
            };
        }

        private static HttpRequestMessage PrepareMultipartFormDataRequest<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod
        )
        {
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            foreach (PropertyInfo property in typeof(TRequest).GetProperties())
            {
                object? value = property.GetValue(requestDto);
                if (value != null)
                {
                    StringContent stringContent = new StringContent(value.ToString() ?? string.Empty);
                    stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = property.Name
                    };
                    formDataContent.Add(stringContent);
                }
            }
            return new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = formDataContent
            };
        }

        private static HttpRequestMessage PrepareHttpRequestWithQueryParameters<TRequest>(
            TRequest requestDto,
            Uri targetUrl
        )
        {
            Dictionary<string, string> requestQueryParameters = ConvertRequestDtoToQueryParameters(requestDto);
            string targetUrlWithQueryParameters = QueryHelpers.AddQueryString(targetUrl.ToString(), requestQueryParameters);

            return new HttpRequestMessage(HttpMethod.Get, targetUrlWithQueryParameters);
        }

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private static async Task<BaseResponseDto> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
            where TResponse : BaseResponseDto
        {
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<TResponse>(responseContent)
                ?? throw new InvalidOperationException("Failed to deserialize response content.");
        }

        private static Dictionary<string, string> ConvertRequestDtoToQueryParameters<TRequest>(TRequest request)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo properties in typeof(TRequest).GetProperties())
            {
                object? value = properties.GetValue(request);
                if (value != null)
                {
                    dictionary.Add(properties.Name, value.ToString());
                }
            }
            return dictionary;
        }
    }
}