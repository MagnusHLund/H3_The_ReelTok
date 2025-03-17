using reeltok.api.videos.Interfaces.Services;
using reeltok.api.videos.Utils;
using Xabe.FFmpeg;

namespace reeltok.api.videos.Services
{
    public class ThumbnailService : IThumbnailService
    {
        public async Task<IFormFile> GenerateVideoThumbnailAsync(IFormFile video)
        {
            await VideoUtils.EnsureValidVideoFileAsync(video).ConfigureAwait(false);

            var mediaInfo = await VideoUtils.GetMediaInfoAsync(video).ConfigureAwait(false);
            TimeSpan thumbnailTime = TimeSpan.FromSeconds(mediaInfo.Duration.TotalSeconds * 0.25); // 25% point

            string temporaryVideoPath = Path.GetTempFileName();
            using (FileStream videoStream = new FileStream(temporaryVideoPath, FileMode.Create))
            {
                await video.CopyToAsync(videoStream).ConfigureAwait(false);
            }

            string thumbnailPath = Path.GetTempFileName() + ".JPG";
            try
            {
                // Extract the thumbnail using FFmpeg
                await FFmpeg.Conversions.New()
                    .AddParameter($"-i {temporaryVideoPath}")
                    .AddParameter($"-ss {thumbnailTime.TotalSeconds}")
                    .AddParameter($"-vframes 1 {thumbnailPath}")
                    .Start().ConfigureAwait(false);

                return new FormFile(
                    new MemoryStream(File.ReadAllBytes(thumbnailPath)),
                    0,
                    new FileInfo(thumbnailPath).Length,
                    "thumbnail",
                    "thumbnail.JPG"
                );
            }
            finally
            {
                File.Delete(temporaryVideoPath);
                if (File.Exists(thumbnailPath))
                {
                    File.Delete(thumbnailPath);
                }
            }
        }
    }
}
