using Xabe.FFmpeg;

namespace reeltok.api.videos.Utils
{
    public static class VideoUtils
    {
        public static Uri GetStreamUrl(string streamPath)
        {
            string baseStreamUrl = "http://localhost:5006";
            return new Uri($"{baseStreamUrl}/{streamPath}");
        }

        public static string CreateStreamPath(Guid userId, Guid videoId)
        {
            return $"{userId}/{videoId}";
        }

        // Public method to validate video length
        public static async Task<bool> IsVideoMinimumLength(IFormFile video)
        {
            var mediaInfo = await GetMediaInfoAsync(video).ConfigureAwait(false);
            TimeSpan minimumDuration = TimeSpan.FromSeconds(1);
            return mediaInfo.Duration >= minimumDuration;
        }

        // Public method to validate file extension
        public static bool IsValidFileExtension(IFormFile video)
        {
            string[] allowedFileExtensions = { ".MP4", ".MKV", ".MOV" };
            string fileExtension = Path.GetExtension(video.FileName).ToUpperInvariant();
            return allowedFileExtensions.Contains(fileExtension);
        }

        // Public method to check if the file has a video stream
        public static async Task<bool> HasVideoStream(IFormFile video)
        {
            var mediaInfo = await GetMediaInfoAsync(video).ConfigureAwait(false);
            return mediaInfo.VideoStreams.Count() > 0;
        }

        // Private helper method to get MediaInfo object
        private static async Task<IMediaInfo> GetMediaInfoAsync(IFormFile video)
        {
            string temporaryFilePath = Path.GetTempFileName();
            using (FileStream stream = new FileStream(temporaryFilePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            IMediaInfo mediaInfo;

            try
            {
                mediaInfo = await FFmpeg.GetMediaInfo(temporaryFilePath);
            }
            catch (Exception ex)
            {
                throw new IOException("An error occurred while getting media info!", ex);
            }
            finally
            {
                File.Delete(temporaryFilePath);
            }

            return mediaInfo;
        }
    }
}
