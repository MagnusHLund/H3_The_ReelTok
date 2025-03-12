using System.Text;
using System.Text.Json;
using System.Reflection;
using System.Net.Http.Headers;
using reeltok.api.gateway.DTOs;
using Microsoft.AspNetCore.WebUtilities;
using reeltok.api.gateway.Interfaces.Services;

namespace reeltok.api.gateway.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // TODO: Refactor this class

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

            HttpRequestMessage request;

            if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
            {
                request = PrepareHttpRequestWithQueryParameters(requestDto, targetUrl);
            }
            else if (isMultipartFormData)
            {
                request = PrepareMultipartFormDataRequest(requestDto, targetUrl, httpMethod);
            }
            else
            {
                request = PrepareHttpRequestBody(requestDto, targetUrl, httpMethod);
            }

            using (request)
            {
                BaseResponseDto response = await RouteRequestAsync<TResponse>(request).ConfigureAwait(false);
                return response;
            }
        }

        private async Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request)
            where TResponse : BaseResponseDto
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? await DeserializeJsonToDtoAsync<TResponse>(response).ConfigureAwait(false)
                : await DeserializeJsonToDtoAsync<FailureResponseDto>(response).ConfigureAwait(false);
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

        private static async Task<BaseResponseDto> DeserializeJsonToDtoAsync<TResponse>(HttpResponseMessage response)
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
