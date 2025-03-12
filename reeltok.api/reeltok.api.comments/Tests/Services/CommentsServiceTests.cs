using Moq;
using Xunit;
using reeltok.api.comments.Services;
using reeltok.api.comments.Interfaces.Repositories;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.Entities;
using reeltok.api.comments.Exceptions;
using reeltok.api.comments.Tests.Factories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace reeltok.api.comments.Tests.Services
{
    public class CommentServiceTests
    {
        private readonly Mock<ICommentsRepository> _mockCommentsRepository;
        private readonly Mock<IExternalApiService> _mockExternalApiService;
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _mockCommentsRepository = new Mock<ICommentsRepository>();
            _mockExternalApiService = new Mock<IExternalApiService>();
            _commentService = new CommentService(_mockCommentsRepository.Object, _mockExternalApiService.Object);
        }

        #region Success Tests

        [Fact]
        public async Task GetTotalCommentsForVideoAsync_WithValidVideo_ReturnsTotalComments()
        {
            // Arrange
            Guid videoId = TestDataFactory.CreateGuid();
            int expectedTotalComments = TestDataFactory.CreateTotalCommentsCount();
            _mockCommentsRepository.Setup(x => x.GetTotalCommentsForVideoAsync(videoId))
                .ReturnsAsync(expectedTotalComments);

            // Act
            int result = await _commentService.GetTotalCommentsForVideoAsync(videoId);

            // Assert
            Assert.Equal(expectedTotalComments, result);
        }

        [Fact]
        public async Task GetCommentsByVideoIdAsync_WithValidData_ReturnsComments()
        {
            // Arrange
            Guid videoId = TestDataFactory.CreateGuid();
            var (pageNumber, pageSize) = TestDataFactory.CreatePaginationData();
            List<CommentEntity> expectedComments = TestDataFactory.CreateCommentEntities(5);
            _mockCommentsRepository.Setup(x => x.GetCommentsByVideoIdAsync(videoId, pageNumber, pageSize))
                .ReturnsAsync(expectedComments);

            // Act
            List<CommentEntity> result = await _commentService.GetCommentsByVideoIdAsync(videoId, pageNumber, pageSize);

            // Assert
            Assert.Equal(expectedComments.Count, result.Count);
            for (int i = 0; i < expectedComments.Count; i++)
            {
                Assert.Equal(expectedComments[i].CommentDetails.Message, result[i].CommentDetails.Message);
            }
        }

        [Fact]
        public async Task CreateCommentAsync_WithValidData_ReturnsCreatedComment()
        {
            // Arrange
            var (videoId, userId, commentText) = TestDataFactory.CreateCommentCreationData();
            CommentEntity newComment = TestDataFactory.CreateCommentEntity();
            _mockExternalApiService.Setup(x => x.EnsureVideoIdExistAsync(videoId)).Returns(Task.CompletedTask);
            _mockCommentsRepository.Setup(x => x.CreateCommentAsync(It.IsAny<CommentEntity>()))
                .ReturnsAsync(newComment);

            // Act
            CommentEntity result = await _commentService.CreateCommentAsync(videoId, userId, commentText);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newComment.CommentDetails.Message, result.CommentDetails.Message);
        }

        #endregion

        #region Failure Tests

        [Fact]
        public async Task GetTotalCommentsForVideoAsync_WithInvalidVideo_ReturnsZero()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            _mockCommentsRepository.Setup(x => x.GetTotalCommentsForVideoAsync(invalidVideoId))
                .ReturnsAsync(0);

            // Act
            int result = await _commentService.GetTotalCommentsForVideoAsync(invalidVideoId);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetCommentsByVideoIdAsync_WithInvalidVideoId_ReturnsEmptyList()
        {
            // Arrange
            Guid invalidVideoId = Guid.Empty;
            var (pageNumber, pageSize) = TestDataFactory.CreatePaginationData();
            _mockCommentsRepository.Setup(x => x.GetCommentsByVideoIdAsync(invalidVideoId, pageNumber, pageSize))
                .ReturnsAsync(new List<CommentEntity>());

            // Act
            List<CommentEntity> result = await _commentService.GetCommentsByVideoIdAsync(invalidVideoId, pageNumber, pageSize);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task CreateCommentAsync_WithInvalidVideoId_ThrowsException()
        {
            // Arrange
            var (videoId, userId, commentText) = TestDataFactory.CreateCommentCreationData();
            _mockExternalApiService.Setup(x => x.EnsureVideoIdExistAsync(videoId))
                .ThrowsAsync(new InvalidOperationException("Video not found"));

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _commentService.CreateCommentAsync(videoId, userId, commentText));
        }

        #endregion
    }
}
