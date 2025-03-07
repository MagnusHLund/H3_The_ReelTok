using Xabe.FFmpeg;

namespace reeltok.api.videos.Utils
{
    public static class VideoUtils
    {
        public static async Task EnsureValidVideoFile(IFormFile video)
        {
            if (video == null || video.Length == 0)
            {
                throw new InvalidOperationException("No video file provided.");
            }

            if (!IsValidFileExtension(video))
            {
                throw new InvalidOperationException("Invalid video file extension.");
            }

            if (!await HasVideoStream(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("The video file does not contain a valid video stream.");
            }

            if (!await IsVideoMinimumLength(video).ConfigureAwait(false))
            {
                throw new InvalidOperationException("The video file is too short.");
            }
        }

        public static string CreateStreamPath(Guid userId, Guid videoId)
        {
            return $"{userId}/{videoId}";
        }

        private static async Task<bool> IsVideoMinimumLength(IFormFile video)
        {
            var mediaInfo = await GetMediaInfoAsync(video).ConfigureAwait(false);
            TimeSpan minimumDuration = TimeSpan.FromSeconds(1);
            return mediaInfo.Duration >= minimumDuration;
        }

        private static bool IsValidFileExtension(IFormFile video)
        {
            string[] allowedFileExtensions = { ".MP4", ".MKV", ".MOV" };
            string fileExtension = Path.GetExtension(video.FileName).ToUpperInvariant();
            return allowedFileExtensions.Contains(fileExtension);
        }

        private static async Task<bool> HasVideoStream(IFormFile video)
        {
            var mediaInfo = await GetMediaInfoAsync(video).ConfigureAwait(false);
            return mediaInfo.VideoStreams.Count() > 0;
        }

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
