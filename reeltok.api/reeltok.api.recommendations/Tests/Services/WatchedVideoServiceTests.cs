using Moq;
using Xunit;
using reeltok.api.recommendations.Services;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Tests.Services
{
    public class WatchedVideoServiceTests
    {
        private readonly Mock<IWatchedVideosRepository> _mockWatchedVideosRepository;
        private readonly WatchedVideoService _watchedVideoService;

        public WatchedVideoServiceTests()
        {
            _mockWatchedVideosRepository = new Mock<IWatchedVideosRepository>();
            _watchedVideoService = new WatchedVideoService(_mockWatchedVideosRepository.Object);
        }

        #region Success Tests

        [Fact]
        public async Task UpdateTotalTimesUserWatchedVideosAsync_WithValidData_UpdatesExistingVideosAndAddsNew()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> watchedVideoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            List<WatchedVideoEntity> existingWatchedVideos = new List<WatchedVideoEntity>
            {
                new WatchedVideoEntity(userId, watchedVideoIds[0], 1, 123456),
                new WatchedVideoEntity(userId, watchedVideoIds[1], 1, 123456) 
            };

            _mockWatchedVideosRepository.Setup(repo => repo.GetExistingWatchedVideosAsync(userId, watchedVideoIds))
                .ReturnsAsync(existingWatchedVideos);

            _mockWatchedVideosRepository.Setup(repo => repo.UpdateWatchedVideosAsync(existingWatchedVideos))
                .Returns(Task.CompletedTask);

            _mockWatchedVideosRepository.Setup(repo => repo.AddNewWatchedVideosAsync(It.IsAny<List<WatchedVideoEntity>>()))
                .Returns(Task.CompletedTask);

            // Act
            await _watchedVideoService.UpdateTotalTimesUserWatchedVideosAsync(userId, watchedVideoIds);

            // Assert
            _mockWatchedVideosRepository.Verify(repo => repo.UpdateWatchedVideosAsync(It.IsAny<List<WatchedVideoEntity>>()), Times.Once);
            _mockWatchedVideosRepository.Verify(repo => repo.AddNewWatchedVideosAsync(It.IsAny<List<WatchedVideoEntity>>()), Times.Once);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task UpdateTotalTimesUserWatchedVideosAsync_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            List<Guid> watchedVideoIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            _mockWatchedVideosRepository.Setup(repo => repo.GetExistingWatchedVideosAsync(userId, watchedVideoIds))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _watchedVideoService.UpdateTotalTimesUserWatchedVideosAsync(userId, watchedVideoIds));
        }

        #endregion
    }
}
