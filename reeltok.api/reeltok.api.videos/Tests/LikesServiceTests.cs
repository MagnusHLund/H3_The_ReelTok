using Moq;
using Xunit;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Services;
using reeltok.api.videos.Factories;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.DTOs.UserLikedVideo;

namespace reeltok.api.videos.Tests
{
    public class LikesServiceTests
    {
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
        public async Task LikeVideoAsync_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            ServiceAddLikeRequestDto requestDto = TestDataFactory.CreateAddLikeRequest();
            FailureResponseDto response = TestDataFactory.CreateFailureResponse("Unable to like video!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.LikeVideoAsync(requestDto.UserId, requestDto.VideoId));
            Assert.Equal(response.Message, exception.Message);
        }

        [Fact]
        public async Task LikeVideoAsync_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");
            ServiceAddLikeResponseDto response = TestDataFactory.CreateAddLikeResponse();

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(
                It.IsAny<ServiceAddLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act
            bool result = await _likesService.LikeVideoAsync(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveLikeFromVideoAsync_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            FailureResponseDto response = TestDataFactory.CreateFailureResponse("Unable to remove like from video!");
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("removeLike");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.RemoveLikeFromVideoAsync(userId, videoId));
            Assert.Equal(response.Message, exception.Message);
        }

        [Fact]
        public async Task RemoveLikeFromVideoAsync_WithValidParameters_ReturnSuccess()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("removeLike");
            ServiceRemoveLikeResponseDto response = TestDataFactory.CreateRemoveLikeResponse();

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceRemoveLikeRequestDto, ServiceRemoveLikeResponseDto>(
                It.IsAny<ServiceRemoveLikeRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(response);

            // Act
            bool result = await _likesService.RemoveLikeFromVideoAsync(userId, videoId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetVideoLikesAsync_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            ServiceUserLikedVideoRequestDto requestDto = new ServiceUserLikedVideoRequestDto(userId, videoId);
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("userLikedVideo");
            FailureResponseDto response = TestDataFactory.CreateFailureResponse("Unable to get total video likes!");

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(
                It.IsAny<ServiceUserLikedVideoRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(response);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _likesService.GetVideoLikesAsync(userId, videoId));
            Assert.Equal(response.Message, exception.Message);
        }

        [Fact]
        public async Task GetVideoLikesAsync_WithValidParameters_ReturnTotalVideoLikesAndIfUserLikedTheVideo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();

            VideoLikes expectedResult = TestDataFactory.CreateVideoLikes();
            ServiceUserLikedVideoResponseDto response = TestDataFactory.CreateUserLikedVideoResponse(expectedResult.UserHasLikedVideo);

            _httpService.Setup(x => x.ProcessRequestAsync<ServiceUserLikedVideoRequestDto, ServiceUserLikedVideoResponseDto>(
                It.IsAny<ServiceUserLikedVideoRequestDto>(),
                It.IsAny<Uri>(),
                It.IsAny<HttpMethod>()))
                .ReturnsAsync(response);

            _likesRepository
                .Setup(x => x.GetTotalVideoLikesAsync(videoId))
                .ReturnsAsync(expectedResult.TotalLikes);

            // Act
            VideoLikes result = await _likesService.GetVideoLikesAsync(userId, videoId);

            // Assert
            Assert.Equal(expectedResult.TotalLikes, result.TotalLikes);
            Assert.Equal(expectedResult.UserHasLikedVideo, result.UserHasLikedVideo);
        }
    }
}
