using reeltok.api.comments.DTOs;

namespace reeltok.api.comments.Interfaces.Services
{
    public interface IHttpService
    {
        Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod,
            bool isMultipartFormData = false
        ) where TResponse : BaseResponseDto;
    }
}