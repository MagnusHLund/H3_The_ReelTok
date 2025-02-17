using System.Net;
using System.Text;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;
using reeltok.api.videos.DTOs.UserLikedVideo;

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

        public static ServiceUserLikedVideoResponseDto CreateUserLikedVideoResponse(bool userHasLikedVideo)
        {
            return new ServiceUserLikedVideoResponseDto
            (
                userHasLikedVideo: userHasLikedVideo,
                success: true
            );
        }

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

        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string responseContent)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };
        }
    }
}
