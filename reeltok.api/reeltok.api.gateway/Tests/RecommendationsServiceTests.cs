using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Recommendations;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Tests
{
    public class RecommendationsServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5004/recommendations";
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly IRecommendationsService _recommendationsService;
        public RecommendationsServiceTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockHttpService = new Mock<IHttpService>();
            _recommendationsService = new RecommendationsService(_mockAuthService.Object, _mockHttpService.Object);
        }

        [Fact]
        public async Task ChangeRecommendedCategory_ValidParameters_ReturnSuccess()
        {
            // Arrange
            List<RecommendedCategories> testRecommendations = new List<RecommendedCategories> { RecommendedCategories.Gaming };
            Recommendations recommendations = new Recommendations(Guid.NewGuid(), testRecommendations);
            bool success = true;
            ServiceChangeRecommendedCategoryResponseDto successResponse = new ServiceChangeRecommendedCategoryResponseDto(success);

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(
                It.IsAny<ServiceChangeRecommendedCategoryRequestDto>(), $"{BaseTestUrl}/update", HttpMethod.Put))
                .ReturnsAsync(successResponse);

            // act
            bool response = await _recommendationsService.UpdateRecommendation(recommendations);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task ChangeRecommendedCategory_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            List<RecommendedCategories> testRecommendations = new List<RecommendedCategories> { RecommendedCategories.Gaming };
            Recommendations recommendations = new Recommendations(Guid.NewGuid(), testRecommendations);
            FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to update users recommendations!");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(
                It.IsAny<ServiceChangeRecommendedCategoryRequestDto>(), $"{BaseTestUrl}/update", HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _recommendationsService.UpdateRecommendation(recommendations));
            Assert.Equal("Unable to update users recommendations!", exception.Message);
        }
    }
}
