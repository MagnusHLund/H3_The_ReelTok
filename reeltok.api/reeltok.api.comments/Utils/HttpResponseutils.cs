using Newtonsoft.Json;
using reeltok.api.comments.DTOs;

namespace reeltok.api.comments.Utils
{
    public static class HttpResponseUtils
    {
        public static async Task<BaseResponseDto> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
            where TResponse : BaseResponseDto
        {
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                TResponse deserializedResponse = JsonConvert.DeserializeObject<TResponse>(responseContent);

                if (!deserializedResponse.Success)
                {
                    FailureResponseDto failureResponse = JsonConvert.DeserializeObject<FailureResponseDto>(responseContent);
                    return failureResponse;
                }

                return deserializedResponse;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to deserialize response.", ex);
            }
        }
    }
}