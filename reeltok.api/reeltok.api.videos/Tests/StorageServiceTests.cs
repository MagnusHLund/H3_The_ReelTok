using Xunit;

namespace reeltok.api.videos.Tests
{
    public class StorageServiceTests
    {
        [Fact]
        public Task UploadVideoToFileServerAsync_UnableToConnect_ThrowIOException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServerAsync_WithInvalidFileType_ThrowFormatException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServerAsync_WithTooShortVideo_ThrowValidationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoToFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoFromFileServerAsync_UnableToFindVideo_ThrowFileNotFoundException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoFromFileServerAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }
    }
}
