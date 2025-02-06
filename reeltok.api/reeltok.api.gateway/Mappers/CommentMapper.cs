using reeltok.api.gateway.DTOs.Comments;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Mappers
{
    internal static class CommentMapper
    {
        internal static CommentUsingDateTime ConvertToDateTime(CommentUsingUnixTime commentToConvert)
        {
            return new CommentUsingDateTime(
                commentId: commentToConvert.CommentId,
                commentDetails: new CommentDetailsUsingDateTime(
                    userId: commentToConvert.CommentDetails.UserId,
                    videoId: commentToConvert.CommentDetails.VideoId,
                    commentText: commentToConvert.CommentDetails.CommentText,
                    createdAt: DateTimeUtils.UnixTimeToDateTime(commentToConvert.CommentDetails.CreatedAt)
                )
            );
        }

        internal static CommentUsingUnixTime ConvertToUnixTime(CommentUsingDateTime commentToConvert)
        {
            return new CommentUsingUnixTime(
                commentId: commentToConvert.CommentId,
                commentDetails: new CommentDetailsUsingUnixTime(
                    userId: commentToConvert.CommentDetails.UserId,
                    videoId: commentToConvert.CommentDetails.VideoId,
                    commentText: commentToConvert.CommentDetails.CommentText,
                    createdAt: DateTimeUtils.DateTimeToUnixTime(commentToConvert.CommentDetails.CreatedAt)
                )
            );
        }

        internal static GatewayAddCommentResponseDto ConvertToResponseDto(CommentUsingDateTime commentToConvert, bool success = true)
        {
            return new GatewayAddCommentResponseDto(
                commentId: commentToConvert.CommentId,
                userId: commentToConvert.CommentDetails.UserId,
                commentText: commentToConvert.CommentDetails.CommentText,
                createdAt: commentToConvert.CommentDetails.CreatedAt,
                success: success
            );
        }
    }
}