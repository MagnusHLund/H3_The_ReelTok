using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;

namespace reeltok.api.gateway.Tests
{
    public class VideosServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5002/videos";
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IVideosService _videosService;

        public VideosServiceTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _mockAuthService = new Mock<IAuthService>();
            _videosService = new VideosService(_mockAuthService.Object, _mockHttpService.Object);
        }

        [Fact]
        public async Task LikeVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid VideoId!");
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("AddLike");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.LikeVideo(invalidVideoId));
            Assert.Equal("Invalid VideoId!", exception.Message);
        }

        [Fact]
        public async Task LikeVideo_ValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid validVideoId = Guid.NewGuid();
            ServiceAddLikeResponseDto successResponse = TestDataFactory.CreateAddLikeResponse();
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("AddLike");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post)).ReturnsAsync(successResponse);

            // Act
            bool result = await _videosService.LikeVideo(validVideoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveLikeFromVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid VideoId!");
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("RemoveLike");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.RemoveLikeFromVideo(invalidVideoId));
            Assert.Equal("Invalid VideoId!", exception.Message);
        }

        [Fact]
        public async Task RemoveLikeFromVideo_ValidParameters_ReturnSuccess()
        {
            // Arrange
            ServiceRemoveLikeResponseDto successResponse = TestDataFactory.CreateRemoveLikeResponse();
            Guid validVideoId = Guid.NewGuid();
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("RemoveLike");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post)).ReturnsAsync(successResponse);

            // Act
            bool result = await _videosService.RemoveLikeFromVideo(validVideoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetVideos_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            byte invalidAmount = 0;
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid amount of videos to fetch!");
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("GetVideoFeed");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(
                It.IsAny<ServiceGetVideosForFeedRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.GetVideosForFeed(invalidAmount));
            Assert.Equal("Invalid amount of videos to fetch!", exception.Message);
        }

        [Fact]
        public async Task GetVideos_ValidParameters_ReturnRequestedAmountOfVideos()
        {
            // Arrange
            byte amountOfVideos = 3;
            List<Video> videos = TestDataFactory.CreateVideoList();
            ServiceGetVideosForFeedResponseDto successResponse = TestDataFactory.CreateGetVideosForFeedResponse(videos);
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("GetVideoFeed");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(
                It.IsAny<ServiceGetVideosForFeedRequestDto>(), targetUrl, HttpMethod.Get)).ReturnsAsync(successResponse);

            // Act
            List<Video> result = await _videosService.GetVideosForFeed(amountOfVideos);

            // Assert
            Assert.Equal(amountOfVideos, (byte) result.Count);

            for (int i = 0; i < amountOfVideos; i++)
            {
                Assert.Equal(videos[i].VideoId, result[i].VideoId);
                Assert.Equal(videos[i].VideoDetails, result[i].VideoDetails);
                Assert.Equal(videos[i].Likes, result[i].Likes);
                Assert.Equal(videos[i].HasLiked, result[i].HasLiked);
                Assert.Equal(videos[i].CreatorDetails, result[i].CreatorDetails);
                Assert.Equal(videos[i].StreamUrl, result[i].StreamUrl);
            }
        }

        [Fact]
        public async Task UploadVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            VideoDetails videoDetails = new VideoDetails("test video", "description", RecommendedCategories.Comedy);
            IFormFile videoFile = null;
            VideoUpload invalidVideoUpload = TestDataFactory.CreateVideoUpload(videoFile);
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid video file!");
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("Upload");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(
                It.IsAny<ServiceUploadVideoRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.UploadVideo(invalidVideoUpload));
            Assert.Equal("Invalid video file!", exception.Message);
        }

        [Fact]
        public async Task UploadVideo_ValidParameters_ReturnVideo()
        {
            // Arrange
            IFormFile videoFile = new Mock<IFormFile>().Object;
            VideoUpload validVideoUpload = TestDataFactory.CreateVideoUpload(videoFile);
            Video video = TestDataFactory.CreateVideo();
            ServiceUploadVideoResponseDto successResponse = TestDataFactory.CreateUploadVideoResponse(video);
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("Upload");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(
                It.IsAny<ServiceUploadVideoRequestDto>(), targetUrl, HttpMethod.Post)).ReturnsAsync(successResponse);

            // Act
            Video result = await _videosService.UploadVideo(validVideoUpload);

            // Assert
            Assert.Equal(video.VideoId, result.VideoId);
            // TODO: Check additional properties
        }

        [Fact]
        public async Task DeleteVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Invalid videoId!");
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("Delete");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(
                It.IsAny<ServiceDeleteVideoRequestDto>(), targetUrl, HttpMethod.Delete))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.DeleteVideo(invalidVideoId));
            Assert.Equal("Invalid videoId!", exception.Message);
        }

        [Fact]
        public async Task DeleteVideo_ValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            ServiceDeleteVideoResponseDto successResponse = TestDataFactory.CreateDeleteVideoResponse();
            Uri targetUrl = TestDataFactory.CreateVideosMicroserviceTestUri("Delete");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(
                It.IsAny<ServiceDeleteVideoRequestDto>(), targetUrl, HttpMethod.Delete)).ReturnsAsync(successResponse);

            // Act
            bool result = await _videosService.DeleteVideo(videoId);

            // Assert
            Assert.True(result);
        }
    }
}
