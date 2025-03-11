using Moq;
using Xunit;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Tests.Factories;

namespace reeltok.api.recommendations.Tests.Services
{
    public class UsersServiceTests
    {
        private readonly Mock<IUserInterestsRepository> _mockUserInterestsRepository;
        private readonly UsersService _usersService;

        public UsersServiceTests()
        {
            _mockUserInterestsRepository = new Mock<IUserInterestsRepository>();
            _usersService = new UsersService(_mockUserInterestsRepository.Object);
        }

        [Fact]
        public async Task GetUserInterestAsync_WithValidUserId_ReturnsUserInterest()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            CategoryEntity categoryEntity = TestDataFactory.CreateCategoryEntity(CategoryType.Tech);

            _mockUserInterestsRepository
                .Setup(repo => repo.GetUserInterestAsync(userId))
                .ReturnsAsync(categoryEntity);

            // Act
            CategoryType result = await _usersService.GetUserInterestAsync(userId);

            // Assert
            Assert.Equal(categoryEntity.Category, result);
            _mockUserInterestsRepository.Verify(repo => repo.GetUserInterestAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserInterestAsync_WithInvalidUserId_ThrowsException()
        {
            // Arrange
            Guid invalidUserId = Guid.Empty;

            _mockUserInterestsRepository
                .Setup(repo => repo.GetUserInterestAsync(invalidUserId))
                .ThrowsAsync(new ArgumentException("Invalid user ID."));

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
                await _usersService.GetUserInterestAsync(invalidUserId));
        }

        [Fact]
        public async Task AddInterestForUserAsync_WithValidParameters_ReturnsSavedUserInterest()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            CategoryType userInterest = CategoryType.Comedy;

            CategoryEntity categoryEntity = TestDataFactory.CreateCategoryEntity(userInterest);
            uint savedCategoryId = 1;

            _mockUserInterestsRepository
                .Setup(repo => repo.AddUserInterestAsync(It.IsAny<CategoryUserInterestEntity>()))
                .ReturnsAsync(savedCategoryId);

            // Act
            CategoryType result = await _usersService.AddInterestForUserAsync(userId, userInterest);

            // Assert
            Assert.Equal(userInterest, result);
            _mockUserInterestsRepository.Verify(repo => repo.AddUserInterestAsync(It.IsAny<CategoryUserInterestEntity>()), Times.Once);
        }

        [Fact]
        public async Task UpdateInterestForUserAsync_WithValidParameters_ReturnsUpdatedUserInterest()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            CategoryType newUserInterest = CategoryType.Sport;
            uint savedCategoryId = 2;

            _mockUserInterestsRepository
                .Setup(repo => repo.UpdateUserInterestAsync(It.IsAny<UserEntity>(), It.IsAny<uint>()))
                .ReturnsAsync(savedCategoryId);

            // Act
            CategoryType result = await _usersService.UpdateInterestForUserAsync(userId, newUserInterest);

            // Assert
            Assert.Equal(newUserInterest, result);
            _mockUserInterestsRepository.Verify(repo => repo.UpdateUserInterestAsync(It.IsAny<UserEntity>(), It.IsAny<uint>()), Times.Once);
        }
    }
}
