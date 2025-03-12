using Moq;
using Xunit;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Services;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.Tests.Factories;
using reeltok.api.videos.Interfaces.Factories;
using reeltok.api.videos.DTOs.GetRecommendedVideos;
using System.Net;
using reeltok.api.videos.Exceptions;

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

        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");
            RecommendedServiceGetRecommendedVideosResponseDto responseDto = new RecommendedServiceGetRecommendedVideosResponseDto
            (
                videoIdList: new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                success: true
            );

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceGetRecommendedVideosRequestDto, RecommendedServiceGetRecommendedVideosResponseDto>(
                It.IsAny<RecommendationsServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            List<Guid> result = await _externalApiService.GetRecommendedVideoIdsAsync(userId, amount);

            // Assert
            Assert.Equal(responseDto.VideoIdList, result);
        }

        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithInvalidRequest_ThrowsFailureNetworkResponseException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");
            FailureResponseDto responseDto = new FailureResponseDto("Test error");

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceGetRecommendedVideosRequestDto, BaseResponseDto>(
                It.IsAny<RecommendationsServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto); // Returning FailureResponseDto

            // Act & Assert
            FailureNetworkResponseException exception = await Assert.ThrowsAsync<FailureNetworkResponseException>(
                () => _externalApiService.GetRecommendedVideoIdsAsync(userId, amount)
            );

            Assert.Contains("Test error", exception.Message);
        }



        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithHttpError_ThrowsHttpRequestException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceGetRecommendedVideosRequestDto, RecommendedServiceGetRecommendedVideosResponseDto>(
                It.IsAny<RecommendationsServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ThrowsAsync(new HttpRequestException("Service unavailable", null, HttpStatusCode.ServiceUnavailable));

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _externalApiService.GetRecommendedVideoIdsAsync(userId, amount));
        }

        [Fact]
        public async Task GetRecommendedVideoIdsAsync_WithEmptyResponse_ReturnsEmptyList()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            byte amount = 5;
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("recommendations");
            RecommendedServiceGetRecommendedVideosResponseDto responseDto = new RecommendedServiceGetRecommendedVideosResponseDto
            (
                videoIdList: new List<Guid>(),
                success: true
            );

            _mockEndpointFactory.Setup(x => x.GetRecommendationsApiUrl(It.IsAny<string>())).Returns(targetUrl);
            _mockHttpService.Setup(x => x.ProcessRequestAsync<RecommendationsServiceGetRecommendedVideosRequestDto, RecommendedServiceGetRecommendedVideosResponseDto>(
                It.IsAny<RecommendationsServiceGetRecommendedVideosRequestDto>(), It.IsAny<Uri>(), It.IsAny<HttpMethod>()))
                .ReturnsAsync(responseDto);

            // Act
            List<Guid> result = await _externalApiService.GetRecommendedVideoIdsAsync(userId, amount);

            // Assert
            Assert.Empty(result);
        }
    }
}
