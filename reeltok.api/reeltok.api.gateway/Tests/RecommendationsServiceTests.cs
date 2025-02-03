using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Recommendations;

namespace reeltok.api.gateway.Tests
{
    public class RecommendationsServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5004/recommendations";
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IRecommendationsService _recommendationsService;
        public RecommendationsServiceTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockGatewayService = new Mock<IGatewayService>();
            _recommendationsService = new RecommendationsService(_mockAuthService.Object, _mockGatewayService.Object);
        }

        [Fact]
        public async Task ChangeRecommendedCategory_ValidParameters_ReturnSuccess()
        {
            // Arrange
            string category = "Gaming";
            bool success = true;
            ChangeRecommendationsCategoryResponseRecommendationsService successResponse = new ChangeRecommendationsCategoryResponseRecommendationsService(success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ChangeRecommendationsCategoryRequestRecommendationsService, ChangeRecommendationsCategoryResponseRecommendationsService>(
                It.IsAny<ChangeRecommendationsCategoryRequestRecommendationsService>(), $"{BaseTestUrl}/update", HttpMethod.Put))
                .ReturnsAsync(successResponse);

            // act
            bool response = await _recommendationsService.ChangeRecommendedCategory(category);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task ChangeRecommendedCategory_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            string category = "Gaming";
            FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to update users recommendations!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ChangeRecommendationsCategoryRequestRecommendationsService, ChangeRecommendationsCategoryResponseRecommendationsService>(
                It.IsAny<ChangeRecommendationsCategoryRequestRecommendationsService>(), $"{BaseTestUrl}/update", HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _recommendationsService.ChangeRecommendedCategory(category));
            Assert.Equal("Unable to update users recommendations!", exception.Message);
        }
    }
}