using Xunit;
using Moq;
using reeltok.api.videos.Services;
using Moq.Protected;
using reeltok.api.videos.DTOs;
using System.Text;
using reeltok.api.videos.DTOs.LikeVideo;
using System.Net;

namespace reeltok.api.videos.Tests
{
    public class HttpServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5001/users/addLike";
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
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            AddLikeRequestDto requestDto = new AddLikeRequestDto(userId, videoId);
            string targetUrl = BaseTestUrl;
            string responseContent = "<LogOutUserResponseDto><Success>true</Success></LogOutUserResponseDto>";
            HttpResponseMessage expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<AddLikeRequestDto, AddLikeResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
            AddLikeResponseDto logOutResponse = response as AddLikeResponseDto;
            Assert.NotNull(logOutResponse);
        }


        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();
            AddLikeRequestDto requestDto = new AddLikeRequestDto(userId, videoId);
            string targetUrl = BaseTestUrl;
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>"; // TODO: Correct this!
            HttpResponseMessage expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<AddLikeRequestDto, FailureResponseDto>(requestDto, targetUrl, HttpMethod.Get);

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
            string targetUrl = BaseTestUrl;

            // Act & Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _httpService.ProcessRequestAsync<AddLikeRequestDto, AddLikeResponseDto>(null, targetUrl, HttpMethod.Get));

            Assert.Equal("requestDto", exception.ParamName);
        }
    }
}
