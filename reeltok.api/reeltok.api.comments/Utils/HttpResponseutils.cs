using Newtonsoft.Json;
using reeltok.api.comments.DTOs;

namespace reeltok.api.comments.Utils
{
    public static class HttpResponseUtils
    {
        public static void HandleResponseCookies(HttpResponseMessage response, IHttpContextAccessor httpContextAccessor)
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookies))
            {
                foreach (var cookie in cookies)
                {
                    httpContextAccessor.HttpContext?.Response.Headers.Append("Set-Cookie", cookie);
                }
            }
        }

        public static async Task<TResponse> DeserializeResponseAsync<TResponse>(HttpResponseMessage response)
            where TResponse : BaseResponseDto
        {
            string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<TResponse>(responseContent)
                ?? throw new InvalidOperationException("Failed to deserialize response content.");
        }
    }
}