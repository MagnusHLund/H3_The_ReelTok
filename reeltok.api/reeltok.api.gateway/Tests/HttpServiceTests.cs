using Moq;
using Xunit;
using System.Net;
using Moq.Protected;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.Factories;

namespace reeltok.api.gateway.Tests
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
            ServiceLogOutUserRequestDto requestDto = TestDataFactory.CreateLogOutUserRequest();
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");
            string responseContent = "<LogOutUserResponseDto><Success>true</Success></LogOutUserResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.OK, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceLogOutUserRequestDto, ServiceLogOutUserResponseDto>(requestDto, targetUrl, HttpMethod.Post);

            // Assert
            Assert.True(response.Success);
        }


        [Fact]
        public async Task ProcessRequestAsync_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            ServiceGetUserIdByTokenRequestDto requestDto = new ServiceGetUserIdByTokenRequestDto();
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.BadRequest, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            BaseResponseDto response = await _httpService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceGetUserIdByTokenResponseDto>(requestDto, targetUrl, HttpMethod.Get);

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
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");

            // Act & Assert
            ArgumentNullException exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                _httpService.ProcessRequestAsync<ServiceGetUserIdByTokenRequestDto, ServiceLogOutUserResponseDto>(null, targetUrl, HttpMethod.Get));

            Assert.Equal("requestDto", exception.ParamName);
        }


        [Fact]
        public async Task RouteRequestAsync_WithSuccessfulResponse_ReturnsCorrectResponse()
        {
            // Arrange
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");
            string responseContent = "<LogOutUserResponseDto><Success>true</Success></LogOutUserResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.OK, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            // Act
            BaseResponseDto response = await _httpService.RouteRequestAsync<ServiceLogOutUserResponseDto>(request);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public async Task RouteRequestAsync_WithFailedResponse_ReturnsErrorResponse()
        {
            // Arrange
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");
            string responseContent = "<FailureResponseDto><Success>false</Success><Message>Test message</Message></FailureResponseDto>";
            HttpResponseMessage expectedResponse = TestDataFactory.CreateHttpResponseMessage(HttpStatusCode.BadRequest, responseContent);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            // Act
            BaseResponseDto response = await _httpService.RouteRequestAsync<ServiceLogOutUserResponseDto>(request);

            // Assert
            FailureResponseDto failureResponse = response as FailureResponseDto;
            Assert.False(response.Success);
            Assert.NotNull(failureResponse);
        }

        [Fact]
        public async Task RouteRequestAsync_WithTimeout_ThrowsTaskCanceledException()
        {
            // Arrange
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("logout");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, targetUrl);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new TaskCanceledException());

            // Act & Assert
            await Assert.ThrowsAsync<TaskCanceledException>(() => _httpService.RouteRequestAsync<ServiceLogOutUserResponseDto>(request));
        }
    }
}
