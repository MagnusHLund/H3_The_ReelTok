using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Interfaces
{
    public interface IGatewayService
    {
        public Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, string targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto;
        public Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto;
    }
}