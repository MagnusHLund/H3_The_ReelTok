using Moq;
using Xunit;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;

namespace reeltok.api.gateway.Tests
{
    public class VideosServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5002/videos";
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IVideosService _videosService;

        public VideosServiceTests()
        {
            _mockGatewayService = new Mock<IGatewayService>();
            _mockAuthService = new Mock<IAuthService>();
            _videosService = new VideosService(_mockAuthService.Object, _mockGatewayService.Object);
        }

        [Fact]
        public async Task LikeVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid VideoId!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), $"{BaseTestUrl}/AddLike", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.LikeVideo(invalidVideoId));
            Assert.Equal("Invalid VideoId!", exception.Message);
        }

        [Fact]
        public async Task LikeVideo_ValidParameters_ReturnSuccess()
        {
            // Arrange
            bool success = true;
            ServiceAddLikeResponseDto successResponse = new ServiceAddLikeResponseDto(success);
            Guid validVideoId = Guid.NewGuid();
            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), $"{BaseTestUrl}/AddLike", HttpMethod.Post)).ReturnsAsync(successResponse);

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
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid VideoId!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), $"{BaseTestUrl}/RemoveLike", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.RemoveLikeFromVideo(invalidVideoId));
            Assert.Equal("Invalid VideoId!", exception.Message);
        }

        [Fact]
        public async Task RemoveLikeFromVideo_ValidParameters_ReturnSuccess()
        {
            // Arrange
            bool success = true;
            ServiceRemoveLikeResponseDto successResponse = new ServiceRemoveLikeResponseDto(success);
            Guid validVideoId = Guid.NewGuid();
            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), $"{BaseTestUrl}/RemoveLike", HttpMethod.Post)).ReturnsAsync(successResponse);

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
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid amount of videos to fetch!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(
                It.IsAny<ServiceGetVideosForFeedRequestDto>(), $"{BaseTestUrl}/GetVideoFeed", HttpMethod.Get))
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
            var videos = new List<Video>
            {
                new Video(Guid.NewGuid(), new VideoDetails("Title1", "Description1", RecommendedCategories.Tech), 0, false, "url1", DateTime.UtcNow, new UserDetails("username1", "profilePictureUrl1", "profileUrl1")),
                new Video(Guid.NewGuid(), new VideoDetails("Title2", "Description2", RecommendedCategories.Gaming), 0, false, "url2", DateTime.UtcNow, new UserDetails("username2", "profilePictureUrl2", "profileUrl2")),
                new Video(Guid.NewGuid(), new VideoDetails("Title3", "Description3", RecommendedCategories.Sport), 0, false, "url3", DateTime.UtcNow, new UserDetails("username3", "profilePictureUrl3", "profileUrl3"))
            };
            ServiceGetVideosForFeedResponseDto successResponse = new ServiceGetVideosForFeedResponseDto(videos);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceGetVideosForFeedRequestDto, ServiceGetVideosForFeedResponseDto>(
                It.IsAny<ServiceGetVideosForFeedRequestDto>(), $"{BaseTestUrl}/GetVideoFeed", HttpMethod.Get)).ReturnsAsync(successResponse);

            // Act
            List<Video> result = await _videosService.GetVideosForFeed(amountOfVideos);

            // Assert
            Assert.Equal(amountOfVideos, (byte)result.Count);
            // TODO: Assert each value of video and compare with the result videos
        }

        [Fact]
        public async Task UploadVideo_BadRequest_ThrowInvalidOperationException()
        {
            // Arrange
            VideoDetails videoDetails = new VideoDetails("test video", "description", RecommendedCategories.Comedy);
            IFormFile videoFile = null;
            VideoUpload invalidVideoUpload = new VideoUpload(videoDetails, videoFile);

            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid video file!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(
                It.IsAny<ServiceUploadVideoRequestDto>(), $"{BaseTestUrl}/Upload", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _videosService.UploadVideo(invalidVideoUpload));
            Assert.Equal("Invalid video file!", exception.Message);
        }

        [Fact]
        public async Task UploadVideo_ValidParameters_ReturnVideo()
        {
            // Arrange
            VideoDetails videoDetails = new VideoDetails("Title", "Description", RecommendedCategories.Tech);
            IFormFile videoFile = new Mock<IFormFile>().Object;
            VideoUpload validVideoUpload = new VideoUpload(videoDetails, videoFile);
            Video video = new Video(Guid.NewGuid(), videoDetails, 0, false, "url", DateTime.UtcNow, new UserDetails("username", "profilePictureUrl", "profileUrl"));
            bool success = true;
            ServiceUploadVideoResponseDto successResponse = new ServiceUploadVideoResponseDto(video, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceUploadVideoRequestDto, ServiceUploadVideoResponseDto>(
                It.IsAny<ServiceUploadVideoRequestDto>(), $"{BaseTestUrl}/Upload", HttpMethod.Post)).ReturnsAsync(successResponse);

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
            FailureResponseDto failureResponseDto = new FailureResponseDto("Invalid videoId!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(
                It.IsAny<ServiceDeleteVideoRequestDto>(), $"{BaseTestUrl}/Delete", HttpMethod.Delete))
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

            bool success = true;
            ServiceDeleteVideoResponseDto successResponse = new ServiceDeleteVideoResponseDto(success);
            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceDeleteVideoRequestDto, ServiceDeleteVideoResponseDto>(It.IsAny<ServiceDeleteVideoRequestDto>(), $"{BaseTestUrl}/Delete", HttpMethod.Delete)).ReturnsAsync(successResponse);

            // Act
            bool result = await _videosService.DeleteVideo(videoId);

            // Assert
            Assert.True(result);
        }
    }
}
