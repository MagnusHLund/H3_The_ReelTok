using reeltok.api.gateway.DTOs.Interfaces;
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

        internal static TResponseDto ConvertToResponseDto<TResponseDto>(CommentUsingDateTime commentToConvert) where TResponseDto : ICommentUsingDateTimeDto, new()
        {
            return new TResponseDto
            {
                CommentId = commentToConvert.CommentId,
                UserId = commentToConvert.CommentDetails.UserId,
                CommentText = commentToConvert.CommentDetails.CommentText,
                CreatedAt = commentToConvert.CommentDetails.CreatedAt
            };
        }

        internal static CommentUsingDateTime ConvertResponseDtoToCommentUsingDateTime<TResponseDto>(TResponseDto responseDto) where TResponseDto : ICommentUsingUnixTimeDto
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
