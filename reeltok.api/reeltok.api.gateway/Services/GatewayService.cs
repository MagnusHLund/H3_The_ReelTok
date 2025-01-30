using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reeltok.api.gateway.Interfaces;
using System.Xml.Serialization;
using System.Text;
using System.Net;
using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public GatewayService(IMapper mapper, HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, string targetUri) where TResponse : BaseResponseDto, new()
        {
            var requestContent = SerializeToXml(requestDto);

            var request = new HttpRequestMessage(HttpMethod.Post, targetUri)
            {
                Content = new StringContent(requestContent, Encoding.UTF8, "application/xml")
            };

            var response = await RouteRequestAsync<TResponse>(request);
            return response;
        }

        // TODO: Rework this method
        public async Task<HttpResponseMessage> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto, new()
        {
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDto = DeserializeFromXml<TResponse>(responseContent);

                var responseXml = SerializeToXml(responseDto);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseXml, Encoding.UTF8, "application/xml")
                };
            }

            return response;
        }

        private static string SerializeToXml<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        private static T DeserializeFromXml<T>(string xml)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}