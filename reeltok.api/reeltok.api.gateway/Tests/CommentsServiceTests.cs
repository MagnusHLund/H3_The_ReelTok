using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
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
        public void AddComment_WithInvalidVideoId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            FailureResponseDto failureResponse = new FailureResponseDto(false, "Video does not exist");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestDto, AddCommentResponseDto>(
                It.IsAny<AddCommentRequestDto>(), $"{BaseTestUrl}/add", HttpMethod.Post))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => _commentsService.AddComment("Invalid Video"));

            // Assert
            Assert.Equal("Video does not exist", exception.Message);
        }

        [Fact]
        public void AddComment_WithEmptyMessage_ShouldThrowInvalidOperationException()
        {
            // Arrange
            FailureResponseDto failureResponse = new FailureResponseDto(false, "Comment cannot be empty");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestDto, AddCommentResponseDto>(
                It.IsAny<AddCommentRequestDto>(), $"{BaseTestUrl}/add", HttpMethod.Post))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => _commentsService.AddComment(""));

            // Assert
            Assert.Equal("Comment cannot be empty", exception.Message);
        }

        [Fact]
        public void AddComment_WithValidParameters_ReturnNewlyCreatedComment()
        {
            // Arrange
            Guid commentId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string commentText = "Amazing test!";
            uint createdAt = (uint)new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            bool success = true;

            AddCommentResponseCommentsServiceDto successResponse = new AddCommentResponseCommentsServiceDto(commentId, userId, commentText, createdAt, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestDto, AddCommentResponseDto>(
                It.IsAny<AddCommentRequestDto>(), $"{BaseTestUrl}/add", HttpMethod.Post))
                .ReturnsAsync(successResponse);

            // Act
            CommentUsingDateTime result = _commentsService.AddComment("Valid Comment");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commentId, result.CommentId);
        }

        [Fact]
        public void LoadComments_WithInvalidParameters_ReturnInvalidParametersMessage()
        {
            // Arrange
            FailureResponseDto failureResponse = new FailureResponseDto(false, "Invalid parameters");

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoadCommentsRequestDto, LoadCommentsResponseDto>(
                It.IsAny<LoadCommentsRequestDto>(), $"{BaseTestUrl}/load", HttpMethod.Get))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() => _commentsService.LoadComments(Guid.Empty, 0));

            // Assert
            Assert.Equal("Invalid parameters", exception.Message);
        }

        [Fact]
        public void LoadComments_WithValidParameters_LoadListOfCommentsForVideo()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            uint createdAt = (uint)new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            List<CommentUsingUnixTime> comments = new List<CommentUsingUnixTime>
            {
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), Guid.NewGuid(), "Cool Test!", createdAt)),
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), Guid.NewGuid(), "I Agree with the previous comment", createdAt))
            };
            bool success = true;
            LoadCommentsResponseCommentsServiceDto successResponse = new LoadCommentsResponseCommentsServiceDto(comments, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoadCommentsRequestDto, LoadCommentsResponseDto>(
                It.IsAny<LoadCommentsRequestDto>(), $"{BaseTestUrl}/load", HttpMethod.Get))
                .ReturnsAsync(successResponse);

            // Act
            List<CommentUsingDateTime> result = _commentsService.LoadComments(videoId, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, comment => Assert.Equal(videoId, comment.CommentDetails.VideoId));
        }
    }
}