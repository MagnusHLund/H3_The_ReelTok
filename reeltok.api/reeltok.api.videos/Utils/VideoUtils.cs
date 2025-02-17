using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using Microsoft.AspNetCore.Http;

namespace reeltok.api.videos.Utils
{
    public static class VideoUtils
    {
        // Public method to validate video length
        public static async Task<bool> IsVideoMinimumLength(IFormFile video)
        {
            MediaFile media = await GetMediaFileAsync(video).ConfigureAwait(false);
            TimeSpan minimumDuration = TimeSpan.FromSeconds(1);
            return media.Metadata.Duration >= minimumDuration;
        }

        // Public method to validate file extension
        public static bool IsValidFileExtension(IFormFile video)
        {
            string[] allowedFileExtensions = { ".MP4", ".MKV" };
            string fileExtension = Path.GetExtension(video.FileName).ToUpperInvariant();
            return allowedFileExtensions.Contains(fileExtension);
        }

        // Public method to check if the file has a video stream
        public static async Task<bool> HasVideoStream(IFormFile video)
        {
            MediaFile media = await GetMediaFileAsync(video).ConfigureAwait(false);
            return media.Metadata.VideoData != null;
        }

        // Private helper method to get MediaFile object
        private static async Task<MediaFile> GetMediaFileAsync(IFormFile video)
        {
            string temporaryFilePath = Path.GetTempFileName();
            using (FileStream stream = new FileStream(temporaryFilePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            MediaFile media = new MediaFile { Filename = temporaryFilePath };
            using (Engine engine = new Engine())
            {
                engine.GetMetadata(media);
            }

            File.Delete(temporaryFilePath);
            return media;
        }
    }
}
