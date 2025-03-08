using Moq;
using Xunit;
using reeltok.api.videos.Utils;
using reeltok.api.videos.Services;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.Tests.Factories;
using reeltok.api.videos.Interfaces.Services;

namespace reeltok.api.videos.Tests.Services
{
    public class VideosServiceTests
    {
        private readonly Mock<IExternalApiService> _mockExternalApiService;
        private readonly Mock<IVideosRepository> _mockVideosRepository;
        private readonly Mock<IStorageService> _mockStorageService;
        private readonly Mock<ILikesService> _mockLikesService;
        private readonly VideosService _videosService;

        public VideosServiceTests()
        {
            _mockExternalApiService = new Mock<IExternalApiService>();
            _mockVideosRepository = new Mock<IVideosRepository>();
            _mockStorageService = new Mock<IStorageService>();
            _mockLikesService = new Mock<ILikesService>();
            _videosService = new VideosService(
                _mockExternalApiService.Object,
                _mockVideosRepository.Object,
                _mockStorageService.Object,
                _mockLikesService.Object
            );
        }

        // TODO: Create tests that fail as well

        [Fact]
        public async Task GetVideosForFeedAsync_WithValidParameters_ReturnVideosForFeed()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 2;
            List<Guid> videoIds = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            List<VideoEntity> videosRepositoryResponse = new List<VideoEntity>
            {
                TestDataFactory.CreateVideoEntity(),
                TestDataFactory.CreateVideoEntity()
            };

            List<VideoCreatorEntity> videoCreatorDetails = new List<VideoCreatorEntity>
            {
                TestDataFactory.CreateVideoCreatorEntity(),
                TestDataFactory.CreateVideoCreatorEntity()
            };

            List<VideoLikesEntity> videoLikes = new List<VideoLikesEntity>
            {
                TestDataFactory.CreateVideoLikesEntity(),
                TestDataFactory.CreateVideoLikesEntity()
            };

            _mockExternalApiService.Setup(x => x.GetRecommendedVideoIdsAsync(userId, amount)).ReturnsAsync(videoIds);
            _mockVideosRepository.Setup(x => x.GetVideosForFeedAsync(videoIds)).ReturnsAsync(videosRepositoryResponse);
            _mockExternalApiService.Setup(x => x.GetVideoCreatorDetailsAsync(videoIds)).ReturnsAsync(videoCreatorDetails);
            _mockLikesService.Setup(x => x.GetLikesForVideos(userId, videoIds)).ReturnsAsync(videoLikes);

            List<VideoForFeedEntity> expectedVideos = VideoFactory.CreateVideoForFeedEntityList(
                videoIds,
                videosRepositoryResponse,
                videoCreatorDetails,
                videoLikes
            );

            // Act
            List<VideoForFeedEntity> result = await _videosService.GetVideosForFeedAsync(userId, amount);

            // Assert
            Assert.Equal(expectedVideos.Count, result.Count);
            for (int i = 0; i < expectedVideos.Count; i++)
            {
                Assert.Equal(expectedVideos[i].VideoId, result[i].VideoId);
                Assert.Equal(expectedVideos[i].VideoDetails.Title, result[i].VideoDetails.Title);
                Assert.Equal(expectedVideos[i].VideoCreator.Username, result[i].VideoCreator.Username);
                Assert.Equal(expectedVideos[i].VideoLikes.TotalLikes, result[i].VideoLikes.TotalLikes);
            }
        }

        [Fact]
        public async Task GetVideosForProfileAsync_WithValidParameters_ReturnVideosForProfile()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            uint pageNumber = 1;
            byte pageSize = 10;
            List<VideoEntity> expectedVideos = new List<VideoEntity>
            {
               TestDataFactory.CreateVideoEntity(),
               TestDataFactory.CreateVideoEntity()
            };

            _mockVideosRepository.Setup(x => x.GetVideosForProfileAsync(userId, pageNumber, pageSize)).ReturnsAsync(expectedVideos);

            // Act
            List<VideoEntity> result = await _videosService.GetVideosForProfileAsync(userId, pageNumber, pageSize);

            // Assert
            Assert.Equal(expectedVideos, result);
        }

        [Fact]
        public async Task UploadVideoAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            VideoUpload videoUpload = TestDataFactory.CreateVideoUpload();
            VideoEntity expectedVideo = TestDataFactory.CreateVideoEntity();

            _mockVideosRepository.Setup(x => x.CreateVideoAsync(It.IsAny<VideoEntity>())).ReturnsAsync(expectedVideo);
            _mockStorageService.Setup(x => x.UploadVideoToFileServerAsync(videoUpload.VideoFile, expectedVideo.VideoId, userId)).Returns(Task.CompletedTask);

            // Act
            VideoEntity result = await _videosService.UploadVideoAsync(videoUpload, userId);

            // Assert
            Assert.Equal(expectedVideo, result);
        }

        [Fact]
        public async Task DeleteVideoAsync_WithValidParameters_SuccessfullyDeleteVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            string streamPath = VideoUtils.CreateStreamPath(userId, videoId);

            _mockVideosRepository.Setup(x => x.DeleteVideoAsync(userId, videoId)).Returns(Task.CompletedTask);
            _mockStorageService.Setup(x => x.RemoveVideoFromFileServerAsync(streamPath)).Returns(Task.CompletedTask);

            // Act
            await _videosService.DeleteVideoAsync(userId, videoId);

            // Assert
            _mockVideosRepository.Verify(x => x.DeleteVideoAsync(userId, videoId), Times.Once);
            _mockStorageService.Verify(x => x.RemoveVideoFromFileServerAsync(streamPath), Times.Once);
        }
    }
}
