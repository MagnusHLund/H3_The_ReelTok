using System.Net;
using System.Text;
using Moq;
using Moq.Protected;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Services;
using Xunit;

namespace reeltok.api.gateway.Tests
{
    public class GatewayServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5003/auth/LogOut";
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly GatewayService _gatewayService;

        public GatewayServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _gatewayService = new GatewayService(_httpClient);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            LogOutUserRequestDto requestDto = new LogOutUserRequestDto();
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
            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
            LogOutUserResponseDto logOutResponse = response as LogOutUserResponseDto;
            Assert.NotNull(logOutResponse);
        }


        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            GetUserIdByTokenRequestDto requestDto = new GetUserIdByTokenRequestDto();
            string targetUrl = BaseTestUrl;
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
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
            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, FailureResponseDto>(requestDto, targetUrl, HttpMethod.Get);

            // Assert
            var failureResponse = response as FailureResponseDto;
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
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, LogOutUserResponseDto>(null, targetUrl, HttpMethod.Get));
        }


        [Fact]
        public async Task RouteRequestAsync_WithSuccessfulResponse_ReturnsCorrectResponse()
        {
            // Arrange
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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            // Act
            BaseResponseDto response = await _gatewayService.RouteRequestAsync<LogOutUserResponseDto>(request);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task RouteRequestAsync_WithFailedResponse_ReturnsErrorResponse()
        {
            // Arrange
            string targetUrl = BaseTestUrl;
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
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

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            // Act
            BaseResponseDto response = await _gatewayService.RouteRequestAsync<LogOutUserResponseDto>(request);

            // Assert
            FailureResponseDto failureResponse = response as FailureResponseDto;
            Assert.False(response.Success);
            Assert.NotNull(failureResponse);
        }

        [Fact]
        public async Task RouteRequestAsync_WithTimeout_ThrowsTaskCanceledException()
        {
            // Arrange
            string targetUrl = BaseTestUrl;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(() => _gatewayService.RouteRequestAsync<LogOutUserResponseDto>(request));
        }

    }
}