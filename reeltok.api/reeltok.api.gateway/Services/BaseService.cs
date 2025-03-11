using reeltok.api.gateway.DTOs;

namespace reeltok.api.gateway.Services
{
    internal abstract class BaseService
    {
        private protected static Exception HandleExceptions(BaseResponseDto response)
        {
            if (response is FailureResponseDto failureResponse)
            {
                return new InvalidOperationException(failureResponse.Message);
            }

            return new InvalidOperationException("An unknown error has occurred!");
        }
    }
}
