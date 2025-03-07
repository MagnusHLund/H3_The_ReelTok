using reeltok.api.videos.DTOs;

namespace reeltok.api.videos.Interfaces
{
    public interface IHttpService
    {
        Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto, Uri targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto;
    }
}
