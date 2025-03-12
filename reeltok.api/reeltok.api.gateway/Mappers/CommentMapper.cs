using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.comments;

namespace reeltok.api.gateway.Mappers
{
    internal static class CommentMapper
    {
        internal static CommentUsingDateTime ConvertToDateTime(CommentUsingUnixTime commentToConvert)
        {
            CommentDetailsUsingDateTime commentDetails = new CommentDetailsUsingDateTime(
                userId: commentToConvert.CommentDetails.UserId,
                videoId: commentToConvert.CommentDetails.VideoId,
                message: commentToConvert.CommentDetails.Message,
                createdAt: DateTimeUtils.UnixTimeToDateTime(commentToConvert.CommentDetails.CreatedAt)
            );

            return new CommentUsingDateTime(
                commentId: commentToConvert.CommentId,
                commentDetails: commentDetails
            );
        }
    }
}