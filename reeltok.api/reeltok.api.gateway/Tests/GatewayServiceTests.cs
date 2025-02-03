using System.Net;
using System.Text;
using AutoMapper;
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

            var mockMapper = new MapperConfiguration(cfg => { });

            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _gatewayService = new GatewayService(_httpClient);
        }

        [Fact]
        public async Task ProcessRequestAsync_WithValidRequest_ReturnsExpectedResponse()
        {
            // Arrange
            var requestDto = new LogOutUserRequestDto();
            string targetUri = BaseTestUrl;
            var responseContent = "<LogOutUserResponseDto><Success>true</Success></LogOutUserResponseDto>";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
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
            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<LogOutUserRequestDto, LogOutUserResponseDto>(requestDto, targetUri, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
            var logOutResponse = response as LogOutUserResponseDto;
            Assert.NotNull(logOutResponse);
        }


        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var requestDto = new GetUserIdByTokenRequestDto();
            string targetUri = BaseTestUrl;
            var responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
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
            BaseResponseDto response = await _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, FailureResponseDto>(requestDto, targetUri, HttpMethod.Get);

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
            string targetUri = BaseTestUrl;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _gatewayService.ProcessRequestAsync<GetUserIdByTokenRequestDto, LogOutUserResponseDto>(null, targetUri, HttpMethod.Get));
        }


        [Fact]
        public async Task RouteRequestAsync_WithSuccessfulResponse_ReturnsCorrectResponse()
        {
            // Arrange
            string targetUri = BaseTestUrl;
            var responseContent = "<LogOutUserResponseDto><Success>true</Success></LogOutUserResponseDto>";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var request = new HttpRequestMessage(HttpMethod.Post, targetUri);

            // Act
            BaseResponseDto response = await _gatewayService.RouteRequestAsync<LogOutUserResponseDto>(request);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task RouteRequestAsync_WithFailedResponse_ReturnsErrorResponse()
        {
            // Arrange
            string targetUri = BaseTestUrl;
            var responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            var request = new HttpRequestMessage(HttpMethod.Post, targetUri);

            // Act
            BaseResponseDto response = await _gatewayService.RouteRequestAsync<FailureResponseDto>(request);

            // Assert
            var failureResponse = response as FailureResponseDto;
            Assert.False(response.Success);
            Assert.NotNull(failureResponse);
        }

        [Fact]
        public async Task RouteRequestAsync_WithTimeout_ThrowsTaskCanceledException()
        {
            // Arrange
            string targetUri = BaseTestUrl;
            var request = new HttpRequestMessage(HttpMethod.Post, targetUri);

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