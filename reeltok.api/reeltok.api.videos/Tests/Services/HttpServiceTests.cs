using Moq;
using Xunit;
using System.Net;
using Moq.Protected;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Services;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.Tests.Factories;

namespace reeltok.api.videos.Tests.Services
{
    public class HttpServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly HttpService _httpService;

        public HttpServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _httpService = new HttpService(_httpClient, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            UsersServiceAddLikeRequestDto requestDto = TestDataFactory.CreateAddLikeRequest();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");
            string responseContent = "{\"Success\":true}";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.OK, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<UsersServiceAddLikeRequestDto, UsersServiceAddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
            UsersServiceAddLikeResponseDto? logOutResponse = response as UsersServiceAddLikeResponseDto;
            Assert.NotNull(logOutResponse);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            UsersServiceAddLikeRequestDto requestDto = TestDataFactory.CreateAddLikeRequest();
            Uri targetUrl = TestDataFactory.CreateUsersMicroserviceTestUri("addLike");
            string responseContent = "{\"Success\":false,\"Message\":\"Test message\"}";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(
                HttpStatusCode.BadRequest, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<UsersServiceAddLikeRequestDto, FailureResponseDto>(requestDto, targetUrl, HttpMethod.Get);

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
                _httpService.ProcessRequestAsync<UsersServiceAddLikeRequestDto, UsersServiceAddLikeResponseDto>(null, targetUrl, HttpMethod.Get));

            Assert.Equal("requestDto", exception.ParamName);
        }
    }
}
