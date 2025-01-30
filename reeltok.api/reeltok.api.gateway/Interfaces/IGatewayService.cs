using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Interfaces
{
    public interface IGatewayService
    {
        public Task<HttpResponseMessage> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, string targetUri) where TResponse : BaseResponseDto, new();
        public Task<HttpResponseMessage> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto, new();
    }
}