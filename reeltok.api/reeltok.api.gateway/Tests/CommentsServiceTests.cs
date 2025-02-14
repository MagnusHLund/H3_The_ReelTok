using Moq;
using Xunit;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Services;
using reeltok.api.gateway.Factories;
using reeltok.api.gateway.Interfaces;
using reeltok.api.gateway.DTOs.Comments;

namespace reeltok.api.gateway.Tests
{
    public class CommentsServiceTests
    {
        private readonly Mock<IHttpService> _mockHttpService;
        private readonly Mock<IAuthService> _mockAuthService;
        private readonly ICommentsService _commentsService;

        public CommentsServiceTests()
        {
            _mockAuthService = new Mock<IAuthService>();
            _mockHttpService = new Mock<IHttpService>();
            _commentsService = new CommentsService(_mockAuthService.Object, _mockHttpService.Object);
        }

        [Fact]
        public async Task AddComment_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            string commentText = "They better not delete this video!";
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse(commentText);
            Uri targetUrl = TestDataFactory.CreateAuthMicroserviceTestUri("/add");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(
                It.IsAny<ServiceAddCommentRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(failureResponseDto);

            // Act & Assert
            InvalidOperationException exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _commentsService.AddComment(videoId, commentText));
            Assert.Equal("Video does not exist!", exception.Message);
        }

        [Fact]
        public async Task AddComment_WithValidParameters_ReturnNewlyCreatedComment()
        {
            // Arrange
            ServiceAddCommentResponseDto successResponse = TestDataFactory.CreateAddCommentResponse();
            Uri targetUrl = TestDataFactory.CreateCommentsMicroserviceTestUri("add");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceAddCommentRequestDto, ServiceAddCommentResponseDto>(
                It.IsAny<ServiceAddCommentRequestDto>(), targetUrl, HttpMethod.Post))
                .ReturnsAsync(successResponse);

            // Act
            CommentUsingDateTime result = await _commentsService.AddComment(successResponse.VideoId, "Valid Comment");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(successResponse.CommentId, result.CommentId);
            Assert.Equal(successResponse.VideoId, result.CommentDetails.VideoId);
            Assert.Equal(successResponse.CommentText, result.CommentDetails.CommentText);
            Assert.Equal(DateTimeUtils.UnixTimeToDateTime(successResponse.CreatedAt), result.CommentDetails.CreatedAt);
        }

        [Fact]
        public async Task LoadComments_WithBadResponse_ThrowInvalidOperationException()
        {
            // Arrange
            Guid videoId = Guid.NewGuid();
            byte amount = 2;
            FailureResponseDto failureResponseDto = TestDataFactory.CreateFailureResponse("Video does not exist!");
            Uri targetUrl = TestDataFactory.CreateCommentsMicroserviceTestUri("load");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(
                It.IsAny<ServiceLoadCommentsRequestDto>(), targetUrl, HttpMethod.Get))
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
            ServiceLoadCommentsResponseDto successResponse = TestDataFactory.CreateLoadCommentsResponse(videoId);
            Uri targetUrl = TestDataFactory.CreateCommentsMicroserviceTestUri("load");

            _mockHttpService.Setup(x => x.ProcessRequestAsync<ServiceLoadCommentsRequestDto, ServiceLoadCommentsResponseDto>(
                It.IsAny<ServiceLoadCommentsRequestDto>(), targetUrl, HttpMethod.Get))
                .ReturnsAsync(successResponse);

            // Act
            List<CommentUsingDateTime> result = await _commentsService.LoadComments(videoId, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, comment =>
            {
                Assert.Equal(videoId, comment.CommentDetails.VideoId);
                Assert.Equal("Cool Test!", comment.CommentDetails.CommentText);
                Assert.NotEqual(Guid.Empty, comment.CommentDetails.UserId);
            });
        }
    }
}
