using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Interfaces
{
    public interface IHttpService
    {
        Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, string targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto;
        Task<BaseResponseDto> RouteRequestAsync<TResponse>(HttpRequestMessage request) where TResponse : BaseResponseDto;
    }
}
