using System.Text;
using System.Reflection;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace reeltok.api.gateway.Services
{
    internal class HttpService : BaseService, IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, Uri targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto
        {
            if (Equals(requestDto, default(TRequest)))
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            HttpRequestMessage request = httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete
                ? PrepareHttpRequestWithQueryParameters(requestDto, targetUrl)
                : PrepareHttpRequestBody(requestDto, targetUrl, httpMethod);

            BaseResponseDto response = await RouteRequestAsync<TResponse>(request).ConfigureAwait(false);

            return response;
        }

        public async Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto
        {

            HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            return response.IsSuccessStatusCode
                ? await DeserializeXmlToDto<TResponse>(response).ConfigureAwait(false)
                : await DeserializeXmlToDto<FailureResponseDto>(response).ConfigureAwait(false);
        }

        private static HttpRequestMessage PrepareHttpRequestBody<TRequest>(TRequest requestDto, Uri targetUrl, HttpMethod httpMethod)
        {
            string requestContent = XmlUtils.SerializeDtoToXml(requestDto);
            return new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = CreateStringContent(requestContent)
            };
        }

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/xml");
        }

        private HttpRequestMessage PrepareHttpRequestWithQueryParameters<TRequest>(TRequest requestDto, Uri targetUrl)
        {
            Dictionary<string, string> requestQueryParameters = ConvertRequestDtoToQueryParameters(requestDto);
            string targetUrlWithQueryParameters = QueryHelpers.AddQueryString(targetUrl.ToString(), requestQueryParameters);

            return new HttpRequestMessage(HttpMethod.Get, targetUrlWithQueryParameters);
        }

        private static async Task<BaseResponseDto> DeserializeXmlToDto<TResponse>(HttpResponseMessage response) where TResponse : BaseResponseDto
        {
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return XmlUtils.DeserializeFromXml<TResponse>(responseContent);
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
