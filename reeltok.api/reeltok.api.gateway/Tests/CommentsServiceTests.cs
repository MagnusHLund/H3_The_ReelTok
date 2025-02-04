using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Comments;

namespace reeltok.api.gateway.Tests
{
    public class CommentsServiceTests
    {
        private const string BaseTestUrl = "http://localhost:5005/comments";
        private readonly Mock<IGatewayService> _mockGatewayService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly ICommentsService _commentsService;

        public CommentsServiceTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockGatewayService = new Mock<IGatewayService>();
            _commentsService = new CommentsService(_mockAuthService.Object, _mockGatewayService.Object);
        }

        [Fact]
        public async Task AddComment_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            string commentText = "They better not delete this video!";
            FailureResponseDto failureResponseDto = new FailureResponseDto("Video does not exist!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(
                It.IsAny<ServiceAddCommentRequestDto>(), $"{BaseTestUrl}/Add", HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.AddComment(videoId, commentText));
            Assert.Equal("Video does not exist!", exception.Message);
        }

        [Fact]
        public async Task AddComment_WithValidParameters_ReturnNewlyCreatedComment()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            Guid commentId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string commentText = "Amazing test!";
            uint createdAt = (uint)new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            bool success = true;

            ServiceAddCommentResponseDto successResponse = new ServiceAddCommentResponseDto(commentId, userId, commentText, createdAt, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(
                It.IsAny<ServiceAddCommentRequestDto>(), $"{BaseTestUrl}/Add", HttpMethod.Post))
                .ReturnsAsync(successResponse);

            // Act
            CommentUsingDateTime result = await _commentsService.AddComment(videoId, "Valid Comment");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commentId, result.CommentId);
        }

        [Fact]
        public async Task LoadComments_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            byte amount = 2;
            FailureResponseDto failureResponseDto = new FailureResponseDto("Video does not exist!");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(
                It.IsAny<ServiceLoadCommentsRequestDto>(), $"{BaseTestUrl}/Load", HttpMethod.Get))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.LoadComments(videoId, amount));
            Assert.Equal("Video does not exist!", exception.Message);
        }

        [Fact]
        public async Task LoadComments_WithValidParameters_LoadListOfCommentsForVideo()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            uint createdAt = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);
            List<CommentUsingUnixTime> comments = new List<CommentUsingUnixTime>
            {
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), videoId, "Cool Test!", createdAt)),
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), videoId, "I Agree with the previous comment", createdAt))
            };
            bool success = true;
            ServiceLoadCommentsResponseDto successResponse = new ServiceLoadCommentsResponseDto(comments, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(
                It.IsAny<ServiceLoadCommentsRequestDto>(), $"{BaseTestUrl}/Load", HttpMethod.Get))
                .ReturnsAsync(successResponse);

            // Act
            List<CommentUsingDateTime> result = await _commentsService.LoadComments(videoId, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, comment => Assert.Equal(videoId, comment.CommentDetails.VideoId));
        }
    }
}