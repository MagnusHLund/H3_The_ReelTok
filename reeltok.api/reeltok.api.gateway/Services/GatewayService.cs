using System.Text;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    internal class GatewayService : BaseService, IGatewayService
    {
        private readonly HttpClient _httpClient;

        public GatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, string targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto
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

            if (Equals(response, null))
            {
                // TODO: Write test for this
                string exceptionMessage = $"Received null response, when calling endpoint: {targetUrl}";
                throw new HttpRequestException(exceptionMessage);
            }

            return response;
        }

        public async Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto
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
