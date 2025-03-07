using Moq;
using Xunit;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Services;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Tests.Factories;
using reeltok.api.videos.Interfaces.Factories;
using reeltok.api.videos.DTOs.GetRecommendedVideos;

namespace reeltok.api.videos.Tests.Services
{
    public class ExternalApiServiceTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IEndpointFactory> _mockEndpointFactory;
        private readonly ExternalApiService _externalApiService;

        public ExternalApiServiceTests()
        {
            _mockHttpService = new Mock<IHttpService>();
            _mockEndpointFactory = new Mock<IEndpointFactory>();
            _externalApiService = new ExternalApiService(_mockHttpService.Object, _mockEndpointFactory.Object);
        }

        // TODO: Make more tests. I refactored and some are gone now, because they became super whack.
        //!^^^^  Ensure that all tests are working. (Except for StorageServerTests)

        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            ServiceGetRecommendedVideosRequestDto requestDto = new ServiceGetRecommendedVideosRequestDto(userId, amount);
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");
            ServiceGetRecommendedVideosResponseDto responseDto = new ServiceGetRecommendedVideosResponseDto
            (
                videoIdList: new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                success: true
            );

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetRecommendedVideosRequestDto, ServiceGetRecommendedVideosResponseDto>(It.IsAny<ServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            List<Guid> result = await _externalApiService.GetRecommendedVideoIdsAsync(userId, amount);

            // Assert
            Assert.Equal(responseDto.VideoIdList, result);
        }

        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithInvalidRequest_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            ServiceGetRecommendedVideosRequestDto requestDto = new ServiceGetRecommendedVideosRequestDto(userId, amount);
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");
            FailureResponseDto responseDto = new FailureResponseDto("Test error");

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceGetRecommendedVideosRequestDto, FailureResponseDto>(
                It.IsAny<ServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _externalApiService
                .GetRecommendedVideoIdsAsync(userId, amount));
        }
    }
}
