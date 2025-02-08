using reeltok.api.videos.DTOs.UserLikedVideo;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Services;
using reeltok.api.videos.DTOs;
using Xunit;
using Moq;
using reeltok.api.videos.DTOs.RemoveLike;

namespace reeltok.api.videos.Tests
{
    public class LikesServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5001/users";
        private readonly Mock<IHttpService> _httpService;
        private readonly Mock<ILikesRepository> _likesRepository;
        private readonly ILikesService _likesService;

        public LikesServiceTests()
        {
            _httpService = new Mock<IHttpService>();
            _likesRepository = new Mock<ILikesRepository>();
            _likesService = new LikesService(_httpService.Object, _likesRepository.Object);
        }

        [Fact]
        public async Task LikeVideo_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            ServiceAddLikeRequestDto requestDto = new ServiceAddLikeRequestDto(userId, videoId);
            FailureResponseDto response = new FailureResponseDto("Unable to like video!");
            Uri targetUrl = new Uri($"{BaseTestUrl}/AddLike");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.LikeVideo(userId, videoId));
            Assert.Equal("Unable to like video!", exception.Message);
        }

        [Fact]
        public async Task LikeVideo_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            Uri targetUrl = new Uri($"{BaseTestUrl}/AddLike");
            bool success = true;
            ServiceAddLikeResponseDto response = new ServiceAddLikeResponseDto(success);

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act
            bool result = await _likesService.LikeVideo(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveLikeFromVideo_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            FailureResponseDto response = new FailureResponseDto("Unable to remove like from video!");
            Uri targetUrl = new Uri($"{BaseTestUrl}/RemoveLike");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.RemoveLikeFromVideo(userId, videoId));
            Assert.Equal("Unable to remove like from video!", exception.Message);
        }

        [Fact]
        public async Task RemoveLikeFromVideo_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            Uri targetUrl = new Uri($"{BaseTestUrl}/RemoveLike");
            bool success = true;
            ServiceRemoveLikeResponseDto response = new ServiceRemoveLikeResponseDto(success);

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act
            bool result = await _likesService.RemoveLikeFromVideo(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetVideoLikes_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            ServiceUserLikedVideoRequestDto requestDto = new ServiceUserLikedVideoRequestDto(userId, videoId);
            Uri targetUrl = new Uri($"{BaseTestUrl}/userLikedVideo");
            FailureResponseDto response = new FailureResponseDto("Unable to get total video likes!");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(
                It.IsAny<ServiceUserLikedVideoRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.GetVideoLikes(userId, videoId));
            Assert.Equal("Unable to get total video likes!", exception.Message);
        }

        [Fact]
        public async Task GetVideoLikes_WithValidParameters_ReturnTotalVideoLikesAndIfUserLikedTheVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            uint expectedLikes = 10;
            bool expectedUserLiked = true;

            ServiceUserLikedVideoResponseDto response = new ServiceUserLikedVideoResponseDto(expectedUserLiked);

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(
                It.IsAny<ServiceUserLikedVideoRequestDto>(),
                It.IsAny<Uri>(),
                It.IsAny<HttpMethod>()))
                .ReturnsAsync(response);

            _likesRepository
                .Setup(x => x.GetTotalVideoLikesAsync(videoId))
                .ReturnsAsync(expectedLikes);

            // Act
            VideoLikes result = await _likesService.GetVideoLikes(userId, videoId);

            // Assert
            Assert.Equal(expectedLikes, result.TotalLikes);
            Assert.Equal(expectedUserLiked, result.UserHasLikedVideo);
        }
    }
}
