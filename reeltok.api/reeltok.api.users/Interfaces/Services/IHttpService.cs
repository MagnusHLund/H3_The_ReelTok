using reeltok.api.users.DTOs;

namespace reeltok.api.users.Interfaces.Services
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