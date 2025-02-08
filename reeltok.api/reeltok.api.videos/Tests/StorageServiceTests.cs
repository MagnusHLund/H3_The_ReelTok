using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.Interfaces;
using Xunit;

namespace reeltok.api.videos.Tests
{
    public class StorageServiceTests
    {

        [Fact]
        public Task UploadVideoToFileServer_UnableToConnect_ThrowIOException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServer_WithInvalidFileType_ThrowFormatException() {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServer_WithTooShortVideo_ThrowValidationException() {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServer_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoFromFileServer_UnableToFindVideo_ThrowFileNotFoundException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoFromFileServer_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }
    }
}
