using Moq;
using Xunit;
using reeltok.api.videos.Services;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Tests
{
    public class VideosServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5004/recommendations";
        private readonly Mock<IVideosRepository> _videosRepositoryMock;
        private readonly Mock<IStorageService> _storageServiceMock;
        private readonly Mock<IHttpService> _httpServiceMock;
        private readonly IVideosService _videosService;

        public VideosServiceTests()
        {
            _videosRepositoryMock = new Mock<IVideosRepository>();
            _storageServiceMock = new Mock<IStorageService>();
            _httpServiceMock = new Mock<IHttpService>();
            _videosService = new VideosService(_videosRepositoryMock.Object, _storageServiceMock.Object, _httpServiceMock.Object);
        }

        [Fact]
        public Task GetVideosForFeedAsync_WithBadResponse_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForFeedAsync_WithInvalidDatabaseResult_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForFeedAsync_WithValidParameters_ReturnSuccess()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForProfileAsync_WithInvalidDatabaseResult_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task GetVideosForProfileAsync_WithValidParameters_ReturnVideosForProfile()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoAsync_WithInvalidVideoFile_ThrowArgumentException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task UploadVideoAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoAsync_VideoIdNotInDatabase_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public Task DeleteVideoAsync_InvalidVideoId_ThrowInvalidOperationException()
        {
            throw new NotImplementedException();
        }
    }
}
