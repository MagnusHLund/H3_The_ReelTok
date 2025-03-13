using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Recommendations;

namespace reeltok.api.gateway.Tests.Services
{
    public class RecommendationsServiceTests
    {/*
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
            Recommendations recommendations = TestDataFactory.CreateVideoRecommendations();
            ServiceChangeRecommendedCategoryResponseDto successResponse = TestDataFactory.CreateServiceChangeRecommendedCategoryResponseDto();
            Uri targetUrl = TestDataFactory.CreateRecommendationsMicroserviceTestUri("update");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(
                It.IsAny<ServiceChangeRecommendedCategoryRequestDto>(), targetUrl, HttpMethod.Put))
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
            Recommendations recommendations = TestDataFactory.CreateVideoRecommendations();
            FailureResponseDto failureResponseDto = new FailureResponseDto("Unable to update users recommendations!");
            Uri targetUrl = TestDataFactory.CreateRecommendationsMicroserviceTestUri("update");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceChangeRecommendedCategoryRequestDto, ServiceChangeRecommendedCategoryResponseDto>(
                It.IsAny<ServiceChangeRecommendedCategoryRequestDto>(), targetUrl, HttpMethod.Put))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _recommendationsService.UpdateRecommendation(recommendations));
            Assert.Equal("Unable to update users recommendations!", exception.Message);
        } */
    }
}
