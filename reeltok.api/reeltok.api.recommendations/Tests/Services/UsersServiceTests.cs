using Moq;
using Xunit;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Tests.Factories;
using reeltok.api.recommendations.Interfaces.Repositories;

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

        #region Success Tests

        [Fact]
        public async Task GetUserInterestAsync_WithValidUser_ReturnsCategoryType()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            CategoryEntity categoryEntity = TestDataFactory.CreateCategoryEntity(CategoryType.Gaming);
            _mockUserInterestsRepository.Setup(repo => repo.GetUserInterestAsync(userId))
                .ReturnsAsync(categoryEntity);

            // Act
            CategoryType result = await _usersService.GetUserInterestAsync(userId);

            // Assert
            Assert.Equal(CategoryType.Gaming, result);
        }

        [Fact]
        public async Task AddInterestForUserAsync_WithValidData_ReturnsSavedCategoryType()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            CategoryType categoryType = CategoryType.Gaming;
            CategoryUserInterestEntity categoryUserInterestEntity = TestDataFactory.CreateCategoryUserInterestEntity();
            uint savedCategoryId = 1;

            _mockUserInterestsRepository.Setup(repo => repo.AddUserInterestAsync(It.IsAny<CategoryUserInterestEntity>()))
                .ReturnsAsync(savedCategoryId);

            // Act
            CategoryType result = await _usersService.AddInterestForUserAsync(userId, categoryType);

            // Assert
            Assert.Equal(CategoryType.Gaming, result);
        }

        [Fact]
        public async Task UpdateInterestForUserAsync_WithValidData_ReturnsUpdatedCategoryType()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            CategoryType newCategoryType = CategoryType.Gaming;
            uint newCategoryId = 1;

            _mockUserInterestsRepository.Setup(repo => repo.UpdateUserInterestAsync(It.IsAny<UserEntity>(), It.IsAny<uint>()))
                .ReturnsAsync(newCategoryId);

            // Act
            CategoryType result = await _usersService.UpdateInterestForUserAsync(userId, newCategoryType);

            // Assert
            Assert.Equal(CategoryType.Gaming, result);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task GetUserInterestAsync_WithInvalidUser_ThrowsException()
        {
            // Arrange
            Guid invalidUserId = TestDataFactory.CreateGuid();
            _mockUserInterestsRepository.Setup(repo => repo.GetUserInterestAsync(invalidUserId))
                .ThrowsAsync(new KeyNotFoundException("User interest not found"));

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _usersService.GetUserInterestAsync(invalidUserId));
        }

        [Fact]
        public async Task AddInterestForUserAsync_WithInvalidCategory_ThrowsException()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            CategoryType invalidCategory = (CategoryType) 999;
            _mockUserInterestsRepository.Setup(repo => repo.AddUserInterestAsync(It.IsAny<CategoryUserInterestEntity>()))
                .ThrowsAsync(new InvalidOperationException("Invalid category"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _usersService.AddInterestForUserAsync(userId, invalidCategory));
        }

        [Fact]
        public async Task UpdateInterestForUserAsync_WithInvalidUser_ThrowsException()
        {
            // Arrange
            Guid userId = TestDataFactory.CreateGuid();
            CategoryType newCategoryType = CategoryType.Gaming;
            _mockUserInterestsRepository.Setup(repo => repo.UpdateUserInterestAsync(It.IsAny<UserEntity>(), It.IsAny<uint>()))
                .ThrowsAsync(new KeyNotFoundException("User not found"));

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _usersService.UpdateInterestForUserAsync(userId, newCategoryType));
        }

        #endregion
    }
}
