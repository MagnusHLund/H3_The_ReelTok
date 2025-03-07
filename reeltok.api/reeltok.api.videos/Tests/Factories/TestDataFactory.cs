using Moq;
using System.Net;
using System.Text;
using reeltok.api.videos.DTOs;
using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;
using reeltok.api.videos.DTOs.LikeVideo;
using reeltok.api.videos.DTOs.RemoveLike;

namespace reeltok.api.videos.Tests.Factories
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
            string streamPath = "/some/test/uri.mp4";
            uint uploadedAt = 1739803271;

            return new VideoEntity
            (
                videoId: videoId,
                userId: userId,
                title: title,
                description: description,
                streamPath: streamPath,
                uploadedAt: uploadedAt
            );
        }

        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string responseContent)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/json")
            };
        }

        public static VideoUpload CreateVideoUpload()
        {
            return new VideoUpload(
                videoFile: new Mock<IFormFile>().Object,
                videoDetails: CreateVideoDetails()
            );
        }

        public static VideoForFeedEntity CreateVideoForFeedEntity()
        {
            Guid videoId = Guid.NewGuid();
            string streamPath = "/some/test/uri.mp4";
            uint uploadedAt = 1739803271;

            return new VideoForFeedEntity(
                videoId: videoId,
                videoDetails: CreateVideoDetails(),
                videoLikes: CreateVideoLikes(),
                videoCreator: CreateUserEntity(),
                streamPath: streamPath,
                uploadedAt: uploadedAt
            );
        }

        public static VideoDetails CreateVideoDetails()
        {
            return new VideoDetails(
                title: "Test video",
                description: "Test description"
            );
        }

        public static UserEntity CreateUserEntity()
        {
            Guid userId = Guid.NewGuid();

            return new UserEntity(
                userId: userId,
                username: "Test user",
                profileUrlPath: userId.ToString(),
                profilePictureUrlPath: "Test profile picture"
            );
        }

        public static VideoCreatorEntity CreateVideoCreatorEntity()
        {
            Guid videoId = Guid.NewGuid();
            UserEntity userEntity = CreateUserEntity();

            return new VideoCreatorEntity(
                videoId: videoId,
                userId: userEntity.UserId,
                username: userEntity.Username,
                profileUrlPath: userEntity.ProfileUrlPath,
                profilePictureUrlPath: userEntity.ProfilePictureUrlPath
            );
        }

        public static VideoLikesEntity CreateVideoLikesEntity()
        {
            Guid videoId = Guid.NewGuid();
            VideoLikes videoLikes = CreateVideoLikes();

            return new VideoLikesEntity(
                videoId: videoId,
                videoLikes: videoLikes
            );
        }

        public static HasUserLikedVideoEntity CreateHasUserLikedVideoEntity(bool hasLiked)
        {
            Guid videoId = Guid.NewGuid();

            return new HasUserLikedVideoEntity(videoId, hasLiked);
        }

        public static TotalVideoLikesEntity CreateTotalVideoLikesEntity(uint totalLikes)
        {
            Guid videoId = Guid.NewGuid();

            return new TotalVideoLikesEntity(videoId, totalLikes);
        }
    }
}