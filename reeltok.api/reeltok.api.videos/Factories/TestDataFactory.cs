using System.Net;
using System.Text;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Enums;
using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;

namespace reeltok.api.videos.Factories
{
    public static class TestDataFactory
    {
        public static Uri CreateUsersMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5001/api/users/{path}");
        }

        public static FailureResponseDto CreateFailureResponse(string errorMessage)
        {
            return new FailureResponseDto
            (
                message: errorMessage
            );
        }

        public static ServiceAddLikeRequestDto CreateAddLikeRequest()
        {
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();

            return new ServiceAddLikeRequestDto
            (
                userId: userId,
                videoId: videoId
            );
        }

        public static ServiceAddLikeResponseDto CreateAddLikeResponse()
        {
            return new ServiceAddLikeResponseDto
            (
                success: true
            );
        }

        public static ServiceRemoveLikeResponseDto CreateRemoveLikeResponse()
        {
            return new ServiceRemoveLikeResponseDto
            (
                success: true
            );
        }
        /*
                public static ServiceUserLikedVideoResponseDto CreateUserLikedVideoResponse(bool userHasLikedVideo)
                {
                    return new ServiceUserLikedVideoResponseDto
                    (
                        userHasLikedVideo: userHasLikedVideo,
                        success: true
                    );
                }

        */
        public static VideoLikes CreateVideoLikes()
        {
            uint totalLikes = 10;
            bool userHasLikedVideo = true;

            return new VideoLikes
            (
                totalLikes: totalLikes,
                userHasLikedVideo: userHasLikedVideo
            );
        }

        public static VideoEntity CreateVideoEntity()
        {
            Guid videoId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string title = "Test video";
            string description = "Test description";
            RecommendedCategories tag = RecommendedCategories.Tech;
            string streamPath = "/some/test/uri.mp4";
            uint uploadedAt = 1739803271;

            return new VideoEntity
            (
                videoId: videoId,
                userId: userId,
                title: title,
                description: description,
                tag: tag,
                streamPath: streamPath,
                uploadedAt: uploadedAt
            );
        }

        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string responseContent)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };
        }
    }
}
