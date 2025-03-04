using reeltok.api.videos.DTOs;

namespace reeltok.api.videos.Services
{
    public abstract class BaseService
    {
        private protected static Exception HandleNetworkResponseExceptions(BaseResponseDto response)
        {
            if (response is FailureResponseDto failureResponse)
            {
                return new InvalidOperationException(failureResponse.Message);
            }

            return new InvalidOperationException("An unknown error has occurred!");
        }
    }
}
