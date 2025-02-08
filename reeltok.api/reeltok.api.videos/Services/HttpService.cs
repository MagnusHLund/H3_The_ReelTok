using System.Text;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Utils;

namespace reeltok.api.videos.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, Uri targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto
        {
            if (Equals(requestDto, null))
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            string requestContent = XmlUtils.SerializeDtoToXml(requestDto);

            HttpRequestMessage request = new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/xml")
            };

            BaseResponseDto response = await RouteRequestAsync<TResponse>(request);

            return response;
        }

        private async Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto
        {
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode
                ? await DeserializeXmlToDto<TResponse>(response)
                : await DeserializeXmlToDto<FailureResponseDto>(response);
        }

        private static async Task<BaseResponseDto> DeserializeXmlToDto<TResponse>(HttpResponseMessage response) where TResponse : BaseResponseDto
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return XmlUtils.DeserializeFromXml<TResponse>(responseContent);
        }
    }
}
