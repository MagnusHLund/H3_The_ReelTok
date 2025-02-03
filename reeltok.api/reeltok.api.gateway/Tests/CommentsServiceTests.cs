using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.DTOs.Comments;
using Microsoft.IdentityModel.Tokens;
using reeltok.api.gateway.Utils;

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
        public async Task AddComment_WithInvalidVideoId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            bool success = false;
            Guid videoId = Guid.Empty;
            FailureResponseDto failureResponse = new FailureResponseDto("Video does not exist", success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestDto, AddCommentResponseDto>(
                It.IsAny<AddCommentRequestDto>(), $"{BaseTestUrl}/add", HttpMethod.Post))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.AddComment(videoId, "Invalid videoId test"));

            // Assert
            Assert.Equal("Video does not exist!", exception.Message);
        }

        [Fact]
        public async Task AddComment_WithEmptyMessage_ShouldThrowInvalidOperationException()
        {
            // Arrange
            bool success = false;
            Guid videoId = Guid.NewGuid();
            FailureResponseDto failureResponse = new FailureResponseDto("Invalid comment text test!", success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestDto, AddCommentResponseDto>(
                It.IsAny<AddCommentRequestDto>(), $"{BaseTestUrl}/Add", HttpMethod.Post))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.AddComment(videoId, ""));

            // Assert
            Assert.Equal("Comment cannot be empty!", exception.Message);
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

            AddCommentResponseCommentsServiceDto successResponse = new AddCommentResponseCommentsServiceDto(commentId, userId, commentText, createdAt, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<AddCommentRequestCommentsServiceDto, AddCommentResponseCommentsServiceDto>(
                It.IsAny<AddCommentRequestCommentsServiceDto>(), $"{BaseTestUrl}/Add", HttpMethod.Post))
                .ReturnsAsync(successResponse);

            // Act
            CommentUsingDateTime result = await _commentsService.AddComment(videoId, "Valid Comment");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(commentId, result.CommentId);
        }

        [Fact]
        public async Task LoadComments_WithInvalidParameters_ReturnInvalidParametersMessage()
        {
            // Arrange
            bool success = false;
            FailureResponseDto failureResponse = new FailureResponseDto("Invalid parameters!", success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoadCommentsRequestDto, LoadCommentsResponseDto>(
                It.IsAny<LoadCommentsRequestDto>(), $"{BaseTestUrl}/load", HttpMethod.Get))
                .ReturnsAsync(failureResponse);

            // Act
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.LoadComments(Guid.Empty, 0));

            // Assert
            Assert.Equal("Invalid parameters!", exception.Message);
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
            LoadCommentsResponseCommentsServiceDto successResponse = new LoadCommentsResponseCommentsServiceDto(comments, success);

            _mockGatewayService.Setup(x => x.ProcessRequestAsync<LoadCommentsRequestCommentsServiceDto, LoadCommentsResponseCommentsServiceDto>(
                It.IsAny<LoadCommentsRequestCommentsServiceDto>(), $"{BaseTestUrl}/Load", HttpMethod.Get))
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