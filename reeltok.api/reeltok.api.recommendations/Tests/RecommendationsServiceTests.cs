// using Moq;
// using Xunit;
// using reeltok.api.recommendations.Interfaces;
// using reeltok.api.recommendations.Services;
// using reeltok.api.recommendations.Entities;
// using reeltok.api.recommendations.Enums;


// namespace reeltok.api.recommendations.Tests
// {
//     public class RecommendationsServiceTests
//     {
//         private const string BaseTestUrl = "http://localhost:5004/recommendation";
//         private readonly Mock<IRecommendationsRepository> _MockRecommendationsRepository;
//         private readonly IRecommendationsService _recommendationService;

//         public RecommendationsServiceTests()
//         {
//             _MockRecommendationsRepository = new Mock<IRecommendationsRepository>();
//             _recommendationService = new RecommendationsService(_MockRecommendationsRepository.Object);
//         }


//         [Fact]
//         public async Task GetRecommendation_WithValidParameters_ReturnRecommendation()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();

//             List<RecommendedCategories> recommendation = new List<RecommendedCategories>() {
//                 RecommendedCategories.Gaming
//             };

//             // Mock the repo
//             _MockRecommendationsRepository
//                 .Setup(r => r.GetRecommendationAsync(userId))
//                 .ReturnsAsync(recommendation);

//             // act
//             List<RecommendedCategories> recommendationReturn = await _recommendationService.GetRecommendation(userId);

//             // Assert
//             Assert.NotNull(recommendation);
//             Assert.True(recommendation.Count() == 1);
//             Assert.Equal(recommendation.First(), recommendationReturn.First());
//         }

//         [Fact]
//         public async Task GetRecommendation_WithInvalidParameters_ThrowException()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             List<RecommendedCategories> recommendations = new List<RecommendedCategories>()
//             {

//             };

//             _MockRecommendationsRepository
//                 .Setup(r => r.GetRecommendationAsync(userId))
//                 .ReturnsAsync(recommendations);


//             // Act and Assert
//             await Assert.ThrowsAsync<InvalidOperationException>(async () =>
//             {
//                 await _recommendationService.GetRecommendation(userId);
//             });
//         }
//         [Fact]
//         public async Task updateRecommendation_WithValidParameters_ReturnSuccessMessage()
//         {
//             // Arrange
//             Guid userId = Guid.NewGuid();
//             List<RecommendedCategories> testRecommendations = new List<RecommendedCategories> { RecommendedCategories.Gaming };
//             Recommendations recommendations = new Recommendations(userId, testRecommendations);


//             _MockRecommendationsRepository
//                 .Setup(r => r.UpdateRecommendationAsync(recommendations))
//                 .Returns(Task.CompletedTask);

//             // Act
//             bool result = await _recommendationService.UpdateRecommendation(recommendations);

//             // Assert
//             Assert.True(result);
//         }

//         [Fact]
//         public async Task updateRecommendation_WithInvalidParameters_ReturnErrorMessage()
//         {
//             // Arrange
//             Guid userId = Guid.Empty; // Invalid userId
//             List<RecommendedCategories> testRecommendations = new List<RecommendedCategories>(); // Invalid recommendations
//             Recommendations recommendations = new Recommendations(userId, testRecommendations);

//             // Act & Assert
//             InvalidOperationException result = await Assert.ThrowsAsync<InvalidOperationException>(() => _recommendationService.UpdateRecommendation(recommendations));
//             Assert.Equal("Invalid parameters provided.", result.Message);
//         }
//     }
// }
