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
        private readonly Mock<IThumbnailService> _mockThumbnailService;
        private readonly Mock<IVideosRepository> _mockVideosRepository;
        private readonly Mock<IStorageService> _mockStorageService;
        private readonly Mock<ILikesService> _mockLikesService;
        private readonly VideosService _videosService;

        public VideosServiceTests()
        {
            _mockExternalApiService = new Mock<IExternalApiService>();
            _mockThumbnailService = new Mock<IThumbnailService>();
            _mockVideosRepository = new Mock<IVideosRepository>();
            _mockStorageService = new Mock<IStorageService>();
            _mockLikesService = new Mock<ILikesService>();
            _videosService = new VideosService(
                _mockExternalApiService.Object,
                _mockThumbnailService.Object,
                _mockVideosRepository.Object,
                _mockStorageService.Object,
                _mockLikesService.Object
            );
        }


        #region Success Tests

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

            List<UserEntity> videoCreatorDetails = new List<UserEntity>
            {
                TestDataFactory.CreateUserEntity(),
                TestDataFactory.CreateUserEntity()
            };

            List<VideoLikesEntity> videoLikes = new List<VideoLikesEntity>
            {
                TestDataFactory.CreateVideoLikesEntity(),
                TestDataFactory.CreateVideoLikesEntity()
            };

            List<Guid> videoCreatorIds = videosRepositoryResponse.ConvertAll(video => video.UserId);

            _mockExternalApiService.Setup(x => x.GetRecommendedVideoIdsAsync(userId, amount)).ReturnsAsync(videoIds);
            _mockVideosRepository.Setup(x => x.GetVideosForFeedAsync(videoIds, amount)).ReturnsAsync(videosRepositoryResponse);
            _mockExternalApiService.Setup(x => x.GetVideoCreatorDetailsAsync(videoCreatorIds)).ReturnsAsync(videoCreatorDetails);
            _mockLikesService.Setup(x => x.GetLikesForVideosAsync(userId, videoIds)).ReturnsAsync(videoLikes);

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
                Assert.Equal(expectedVideos[i].VideoCreator.UserDetails.Username, result[i].VideoCreator.UserDetails.Username);
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

        // TODO: Fix// system.io.ioexception: an error occured while getting media info!
        [Fact]
        public async Task UploadVideoAsync_WithValidParameters_SuccessfullyUploadVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            VideoUpload videoUpload = TestDataFactory.CreateVideoUpload();
            IFormFile thumbnailFile = TestDataFactory.CreateThumbnailFile();

            VideoEntity expectedVideo = TestDataFactory.CreateVideoEntity();

            // Ensure the videoUpload object and its VideoFile are not null
            Assert.NotNull(videoUpload);
            Assert.NotNull(videoUpload.VideoFile);

            // Mock repository to return the expected video entity when CreateVideoAsync is called
            _mockVideosRepository
                .Setup(x => x.CreateVideoAsync(It.IsAny<VideoEntity>()))
                .ReturnsAsync(expectedVideo);

            _mockThumbnailService
                .Setup(x => x.GenerateVideoThumbnailAsync(videoUpload.VideoFile))
                .ReturnsAsync(thumbnailFile);

            // Mock storage service to successfully upload the video file
            _mockStorageService
                .Setup(x => x.UploadVideoFilesUsingSftpAsync(videoUpload.VideoFile, thumbnailFile, expectedVideo.VideoId, userId))
                .Returns(Task.CompletedTask);

            // Act
            VideoEntity result = await _videosService.UploadVideoAsync(videoUpload, userId, 1);

            // Assert
            Assert.Equal(expectedVideo, result);
            Assert.NotNull(videoUpload);
            Assert.NotNull(videoUpload.VideoFile);
        }

        [Fact]
        public async Task DeleteVideoAsync_WithValidParameters_SuccessfullyDeleteVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            Mock<IFormFile> fileMock = TestDataFactory.CreateMockVideoFile();
            string streamPath = VideoUtils.CreateStreamPath(userId, videoId, (IFormFile)fileMock);

            _mockVideosRepository.Setup(x => x.DeleteVideoAsync(userId, videoId)).Returns(Task.FromResult(streamPath));
            _mockStorageService.Setup(x => x.DeleteVideoFilesUsingSftpAsync(streamPath)).Returns(Task.CompletedTask);

            // Act
            await _videosService.DeleteVideoAsync(userId, videoId);

            // Assert
            _mockVideosRepository.Verify(x => x.DeleteVideoAsync(userId, videoId), Times.Once);
            _mockStorageService.Verify(x => x.DeleteVideoFilesUsingSftpAsync(streamPath), Times.Once);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task GetVideosForFeedAsync_WithInvalidParameters_ReturnsEmptyList()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 2;

            // Mock external API service to return an empty list of video IDs
            _mockExternalApiService
                .Setup(x => x.GetRecommendedVideoIdsAsync(userId, amount))
                .ReturnsAsync(new List<Guid>());

            // Mock videos repository to return an empty list when called with empty video IDs list
            _mockVideosRepository
                .Setup(x => x.GetVideosForFeedAsync(It.IsAny<List<Guid>>(), amount))
                .ReturnsAsync(new List<VideoEntity>());

            // Mock other dependencies to return empty lists
            _mockExternalApiService
                .Setup(x => x.GetVideoCreatorDetailsAsync(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<UserEntity>());

            _mockLikesService
                .Setup(x => x.GetLikesForVideosAsync(userId, It.IsAny<List<Guid>>()))
                .ReturnsAsync(new List<VideoLikesEntity>());

            // Act
            List<VideoForFeedEntity> result = await _videosService.GetVideosForFeedAsync(userId, amount);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetVideosForProfileAsync_WithInvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            Guid invalidUserId = Guid.Empty;
            uint pageNumber = 1;
            byte pageSize = 10;
            _mockVideosRepository.Setup(x => x.GetVideosForProfileAsync(invalidUserId, pageNumber, pageSize)).ReturnsAsync(new List<VideoEntity>());

            // Act
            List<VideoEntity> result = await _videosService.GetVideosForProfileAsync(invalidUserId, pageNumber, pageSize);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task UploadVideoAsync_WithNullVideoUpload_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await _videosService.UploadVideoAsync(null, userId, 1).ConfigureAwait(false));

        }

        [Fact]
        public async Task DeleteVideoAsync_WithNonExistentVideo_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            _mockVideosRepository.Setup(x => x.DeleteVideoAsync(userId, videoId)).ThrowsAsync(new Exception("Video not found"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _videosService.DeleteVideoAsync(userId, videoId));
        }

        #endregion
    }
}
