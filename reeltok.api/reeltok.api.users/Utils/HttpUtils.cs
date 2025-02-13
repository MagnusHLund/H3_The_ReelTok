namespace reeltok.api.users.Utils
{
    public static class HttpUtils // Change from 'public' to 'public'
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task<bool> ValidateVideoAsync(Guid likedVideoId)  // Change from 'public' to 'public'
        {
            var response = await _httpClient.GetAsync($"http://localhost:5002/videos/validate/{likedVideoId}");
            return response.IsSuccessStatusCode;
        }
    }
}
