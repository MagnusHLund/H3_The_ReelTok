using System.Text;
using System.Text.Json;
using System.Reflection;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace reeltok.api.videos.Services
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
            HttpMethod httpMethod
        ) where TResponse : BaseResponseDto
        {
            if (Equals(requestDto, default(TRequest)))
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            HttpRequestMessage request = httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete
                ? PrepareHttpRequestWithQueryParameters(requestDto, targetUrl)
                : PrepareHttpRequestBody(requestDto, targetUrl, httpMethod);

            using (request)
            {
                BaseResponseDto response = await RouteRequestAsync<TResponse>(request).ConfigureAwait(false);
                return response;
            }
        }

        public async Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request)
            where TResponse : BaseResponseDto
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? await DeserializeJsonToDto<TResponse>(response).ConfigureAwait(false)
                : await DeserializeJsonToDto<FailureResponseDto>(response).ConfigureAwait(false);
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

        private static HttpRequestMessage PrepareHttpRequestWithQueryParameters<TRequest>(TRequest requestDto, Uri targetUrl)
        {
            Dictionary<string, string> requestQueryParameters = ConvertRequestDtoToQueryParameters(requestDto);
            string targetUrlWithQueryParameters = QueryHelpers.AddQueryString(targetUrl.ToString(), requestQueryParameters);

            return new HttpRequestMessage(HttpMethod.Get, targetUrlWithQueryParameters);
        }

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private static async Task<BaseResponseDto> DeserializeJsonToDto<TResponse>(HttpResponseMessage response)
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