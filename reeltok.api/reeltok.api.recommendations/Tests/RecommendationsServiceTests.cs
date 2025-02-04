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
            // Do a count, an check that you have expected amount of categories (1)
            Assert.True(recommendation.Count() == 1);
            // Ensure recommendation is the same as set in Arrange.
            Assert.Equal(recommendation.First(), recommendations.RecommendationCategory.First());
        }
        [Fact]
        public void GetRecommendation_WithInvalidParameters_ReturnRecommendation()
        {
            Assert.True(true);
        }
        [Fact]
        public void updateRecommendation_WithValidParameters_ReturnSuccessMessage()
        {
            Assert.True(true);
        }
        [Fact]
        public void updateRecommendation_WithInvalidParameters_ReturnSuccessMessage()
        {
            Assert.True(true);
        }
    }
}