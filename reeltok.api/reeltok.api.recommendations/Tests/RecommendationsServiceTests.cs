using System;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Interfaces;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using Castle.Components.DictionaryAdapter.Xml;
using Xunit.Sdk;

namespace reeltok.api.recommendations.Tests
{
    public class RecommendationsServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5004/recommendation";
        private readonly Mock<IRecommendationsRepository> _MockRecommendationsRepository;
        private readonly IRecommendationsService _recommendationService;

        public RecommendationsServiceTests()
        {
            _MockRecommendationsRepository = new Mock<IRecommendationsRepository>();
            _recommendationService = new RecommendationsService(_MockRecommendationsRepository.Object);
        }


        [Fact]
        public async Task GetRecommendation_WithValidParameters_ReturnRecommendation()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Recommendations recommendations = new Recommendations(userId, new List<RecommendationsEnum>() {
                RecommendationsEnum.Gaming
            });

            // act
            List<RecommendationsEnum> recommendation = await _recommendationService.GetRecommendation(userId);

            // Assert
            Assert.True(recommendation.Count() == 1);

            Assert.Equal(recommendation.First(), recommendations.RecommendationCategory.First());
        }

        [Fact]
        public async Task GetRecommendation_WithInvalidParameters_ThrowException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Recommendations recommendations = new Recommendations(userId, new List<RecommendationsEnum>()
            {
                RecommendationsEnum.Gaming
            });

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _recommendationService.GetRecommendation(userId);
            });
        }
        [Fact]
        public async Task updateRecommendation_WithValidParameters_ReturnSuccessMessage()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<RecommendationsEnum> testRecommendations = new List<RecommendationsEnum> { RecommendationsEnum.Gaming };
            Recommendations recommendations = new Recommendations(userId, testRecommendations);
            // Act
            var result = await _recommendationService.UpdateRecommendation(recommendations);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task updateRecommendation_WithInvalidParameters_ReturnErrorMessage()
        {
            // Arrange
            Guid userId = Guid.Empty; // Invalid userId
            List<RecommendationsEnum> testRecommendations = null; // Invalid recommendations
            Recommendations recommendations = new Recommendations(userId, testRecommendations);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidOperationException>(() => _recommendationService.UpdateRecommendation(recommendations));
            Assert.Equal("Invalid parameters provided.", result.Message);
        }
    }
}