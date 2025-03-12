using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IHttpService
    {
        Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, Uri targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto;
    }
}
