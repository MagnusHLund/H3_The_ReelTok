using Moq;
using Xunit;
using System.Net;
using Moq.Protected;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Services;
using reeltok.api.videos.Factories;
using reeltok.api.videos.DTOs.LikeVideo;

namespace reeltok.api.videos.Tests
{
    public class HttpServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly HttpService _httpService;

        public HttpServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _httpService = new HttpService(_httpClient);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            ServiceAddLikeRequestDto requestDto = TestDataFactory.CreateAddLikeRequest();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");
            string responseContent = "<AddLikeResponseDto><Success>true</Success></AddLikeResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.OK, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
            ServiceAddLikeResponseDto? logOutResponse = response as ServiceAddLikeResponseDto;
            Assert.NotNull(logOutResponse);
        }


        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            ServiceAddLikeRequestDto requestDto = TestDataFactory.CreateAddLikeRequest();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.BadRequest, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, FailureResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            // Assert
            FailureResponseDto failureResponse = response as FailureResponseDto;
            Assert.False(response.Success);
            Assert.NotNull(failureResponse);
            Assert.Equal("Test message", failureResponse.Message);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithNullRequestDto_ThrowsArgumentNullException()
        {
            // Arrange
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");

            // Act & Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _httpService.ProcessRequestAsync<ServiceAddLikeRequestDto, ServiceAddLikeResponseDto>(null, targetUrl, HttpMethod.Get));

            Assert.Equal("requestDto", exception.ParamName);
        }
    }
}
