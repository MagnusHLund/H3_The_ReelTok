using reeltok.api.gateway.Utils;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Interfaces.DTOs;

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

        internal static CommentUsingDateTime ConvertResponseDtoToCommentUsingDateTime<TResponseDto>(TResponseDto responseDto)
            where TResponseDto : ICommentUsingUnixTimeDto
        {
            CommentDetailsUsingDateTime details = new CommentDetailsUsingDateTime(
                userId: responseDto.UserId,
                videoId: responseDto.VideoId,
                commentText: responseDto.CommentText,
                createdAt: DateTimeUtils.UnixTimeToDateTime(responseDto.CreatedAt)
            );

            return new CommentUsingDateTime(
                responseDto.CommentId,
                commentDetails: details
            );
        }
    }
}
