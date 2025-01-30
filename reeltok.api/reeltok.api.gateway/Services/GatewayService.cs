using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reeltok.api.gateway.Interfaces;

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

        public HttpResponseMessage ProcessRequest(HttpRequest request) { }
        public HttpResponseMessage RouteRequest(HttpResponseMessage response) { }
    }
}