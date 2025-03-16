using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;
using reeltok.api.gateway.Entities.comments;
using reeltok.api.gateway.Entities.Videos;

namespace reeltok.api.gateway.Mappers
{
    internal static class TimeMapper
    {
        internal static CommentUsingDateTime ConvertCommentToDateTime(CommentUsingUnixTime commentToConvert)
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

        internal static VideoForFeedUsingDateTimeEntity ConvertVideoForFeedToDateTime(
            VideoForFeedUsingUnixTimeEntity videoToConvert
        )
        {
            VideoDetails videoDetails = new VideoDetails(
                title: videoToConvert.VideoDetails.Title,
                description: videoToConvert.VideoDetails.Description,
                category: videoToConvert.VideoDetails.Category
            );

            VideoLikes videoLikes = new VideoLikes(
                totalLikes: videoToConvert.VideoLikes.TotalLikes,
                userHasLikedVideo: videoToConvert.VideoLikes.UserHasLikedVideo
            );

            return new VideoForFeedUsingDateTimeEntity(
                videoId: videoToConvert.VideoId,
                videoDetails: videoDetails,
                videoLikes: videoLikes,
                videoCreator: videoToConvert.VideoCreator,
                streamPath: videoToConvert.StreamPath,
                uploadedAt: DateTimeUtils.UnixTimeToDateTime(videoToConvert.UploadedAt)
            );
        }

        internal static BaseVideoUsingDateTimeEntity ConverBaseVideoToDateTime(
            BaseVideoUsingUnixTimeEntity videoToConvert
        )
        {
            return new BaseVideoUsingDateTimeEntity(
                videoId: videoToConvert.VideoId,
                streamPath: videoToConvert.StreamPath,
                createdAt: DateTimeUtils.UnixTimeToDateTime(videoToConvert.UploadedAt)
            );
        }
    }
}
