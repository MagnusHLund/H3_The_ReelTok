using Moq;
using Xunit;
using reeltok.api.comments.Services;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.DTOs.DoesVideoIdExist;
using reeltok.api.comments.Exceptions;
using reeltok.api.comments.Tests.Factories;
using reeltok.api.comments.Interfaces.Factories;
using reeltok.api.comments.DTOs;

namespace reeltok.api.comments.Tests.Services
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

        #region Success Tests

        [Fact]
        public async Task EnsureVideoIdExistAsync_WithValidVideoId_ReturnsSuccess()
        {
            // Arrange
            Guid validVideoId = TestDataFactory.CreateGuid();
            VideosServiceDoesVideoIdExistRequestDto requestDto = new VideosServiceDoesVideoIdExistRequestDto(validVideoId);
            Uri targetUrl = TestDataFactory.CreateUri();
            _mockEndpointFactory.Setup(x => x.GetVideosApiUrl("videos")).Returns(targetUrl);

            VideosServiceDoesVideoIdExistResponseDto successResponse = new VideosServiceDoesVideoIdExistResponseDto(true); // Correct initialization with constructor

            _mockHttpService.Setup(x => x.ProcessRequestAsync<VideosServiceDoesVideoIdExistRequestDto, VideosServiceDoesVideoIdExistResponseDto>(
                    It.IsAny<VideosServiceDoesVideoIdExistRequestDto>(), targetUrl, HttpMethod.Get, false))
                .ReturnsAsync(successResponse);

            // Act
            await _externalApiService.EnsureVideoIdExistAsync(validVideoId);

            // Assert
            _mockHttpService.Verify(x => x.ProcessRequestAsync<VideosServiceDoesVideoIdExistRequestDto, VideosServiceDoesVideoIdExistResponseDto>(
                It.IsAny<VideosServiceDoesVideoIdExistRequestDto>(), targetUrl, HttpMethod.Get, false), Times.Once);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task EnsureVideoIdExistAsync_WithInvalidVideoId_ThrowsFailureNetworkResponseException()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            VideosServiceDoesVideoIdExistRequestDto requestDto = new VideosServiceDoesVideoIdExistRequestDto(invalidVideoId);
            Uri targetUrl = TestDataFactory.CreateUri();
            _mockEndpointFactory.Setup(x => x.GetVideosApiUrl("videos")).Returns(targetUrl);

            FailureResponseDto failureResponse = new FailureResponseDto("Video not found");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<VideosServiceDoesVideoIdExistRequestDto, VideosServiceDoesVideoIdExistResponseDto>(
                    It.IsAny<VideosServiceDoesVideoIdExistRequestDto>(), targetUrl, HttpMethod.Get, false))
                .ReturnsAsync(failureResponse);

            // Act & Assert
            await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.EnsureVideoIdExistAsync(invalidVideoId));
        }

        [Fact]
        public async Task EnsureVideoIdExistAsync_WithUnexpectedResponse_ThrowsFailureNetworkResponseException()
        {
            // Arrange
            Guid invalidVideoId = Guid.NewGuid();
            VideosServiceDoesVideoIdExistRequestDto requestDto = new VideosServiceDoesVideoIdExistRequestDto(invalidVideoId);
            Uri targetUrl = TestDataFactory.CreateUri();
            _mockEndpointFactory.Setup(x => x.GetVideosApiUrl("videos")).Returns(targetUrl);

            FailureResponseDto unexpectedResponse = new FailureResponseDto("Unexpected response");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<VideosServiceDoesVideoIdExistRequestDto, VideosServiceDoesVideoIdExistResponseDto>(
                    It.IsAny<VideosServiceDoesVideoIdExistRequestDto>(), targetUrl, HttpMethod.Get, false))
                .ReturnsAsync(unexpectedResponse);

            // Act & Assert
            await Assert.ThrowsAsync<FailureNetworkResponseException>(() => _externalApiService.EnsureVideoIdExistAsync(invalidVideoId));
        }

        #endregion
    }
}
