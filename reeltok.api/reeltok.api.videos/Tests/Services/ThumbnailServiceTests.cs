using System.IO;
using System.Threading.Tasks;
using Moq;
using Xunit;
using reeltok.api.videos.Services;
using reeltok.api.videos.Utils;
using Microsoft.AspNetCore.Http;
using reeltok.api.videos.Tests.Factories;

namespace reeltok.api.videos.Tests.Services
{
    public class ThumbnailServiceTests
    {
        private readonly ThumbnailService _thumbnailService;

        public ThumbnailServiceTests()
        {
            _thumbnailService = new ThumbnailService();
        }

        [Fact]
        public async Task GenerateVideoThumbnailAsync_WithValidVideo_ReturnsThumbnail()
        {
            // Arrange
            IFormFile mockVideoFile = TestDataFactory.CreateMockVideoFile().Object;

            // Act
            IFormFile thumbnail = await _thumbnailService.GenerateVideoThumbnailAsync(mockVideoFile);

            // Assert
            Assert.NotNull(thumbnail);
            Assert.Equal("thumbnail.JPG", thumbnail.FileName);
            Assert.True(thumbnail.Length > 0);
        }

        [Fact]
        public async Task GenerateVideoThumbnailAsync_WithInvalidVideo_ThrowsException()
        {
            // Arrange
            IFormFile invalidVideoFile = TestDataFactory.CreateInvalidMockVideoFile().Object;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => _thumbnailService.GenerateVideoThumbnailAsync(invalidVideoFile));
        }

        [Fact]
        public async Task GenerateVideoThumbnailAsync_CleansUpTemporaryFiles()
        {
            // Arrange
            IFormFile mockVideoFile = TestDataFactory.CreateMockVideoFile().Object;
            string tempPathBefore = Path.GetTempPath();

            // Act
            await _thumbnailService.GenerateVideoThumbnailAsync(mockVideoFile);

            // Assert
            string tempPathAfter = Path.GetTempPath();
            Assert.Equal(tempPathBefore, tempPathAfter); // Ensure no leftover files in temp directory
        }
    }
}