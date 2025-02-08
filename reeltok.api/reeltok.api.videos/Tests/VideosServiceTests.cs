using Moq;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Services;
using Xunit;

namespace reeltok.api.videos.Tests
{
    public class VideosServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5004/recommendations";
        private readonly Mock<IStorageService> _storageService;
        private readonly Mock<IHttpService> _httpService;
        private readonly IVideosService _videosService;

        public VideosServiceTests() {
            _storageService = new Mock<IStorageService>();
            _httpService = new Mock<IHttpService>();
            _videosService = new VideosService(_storageService.Object, _httpService.Object);
        }

        [Fact]
        public Task GetVideosForFeed_WithBadResponse_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForFeed_WithInvalidDatabaseResult_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForFeed_WithValidParameters_ReturnSuccess()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForProfile_WithInvalidDatabaseResult_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForProfile_WithValidParameters_ReturnVideosForProfile()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideo_WithInvalidVideoFile_ThrowArgumentException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideo_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideo_VideoIdNotInDatabase_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideo_InvalidVideoId_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }
    }
}
