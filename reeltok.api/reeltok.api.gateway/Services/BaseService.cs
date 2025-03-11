using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Exceptions;

namespace reeltok.api.gateway.Services
{
    internal abstract class BaseService
    {
        private protected static Exception HandleNetworkResponseExceptions(BaseResponseDto response)
        {
            if (response is FailureResponseDto failureResponse)
            {
                return new FailureNetworkResponseException(failureResponse.Message);
            }

            return new FailureNetworkResponseException("An unknown error has occurred!");
        }
    }
}
