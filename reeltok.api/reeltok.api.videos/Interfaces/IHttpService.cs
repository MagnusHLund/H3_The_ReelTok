using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.DTOs;

namespace reeltok.api.videos.Interfaces
{
    public interface IHttpService
    {
        Task<BaseResponseDto> ProcessRequestAsync<TRequest, TResponse>(TRequest requestDto ,string targetUrl, HttpMethod httpMethod) where TResponse : BaseResponseDto;
    }
}
