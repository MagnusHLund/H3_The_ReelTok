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

        public static UsersServiceAddLikeRequestDto CreateAddLikeRequest()
        {
            Guid userId = Guid.NewGuid();
            Guid videoId = Guid.NewGuid();

            return new UsersServiceAddLikeRequestDto
            (
                userId: userId,
                videoId: videoId
            );
        }

        public static UsersServiceAddLikeResponseDto CreateAddLikeResponse()
        {
            return new UsersServiceAddLikeResponseDto
            (
                success: true
            );
        }

        public static UsersServiceRemoveLikeResponseDto CreateRemoveLikeResponse()
        {
            return new UsersServiceRemoveLikeResponseDto
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
            // Mocking a valid file
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            byte[] fileContent = new byte[100]; // Arbitrary byte content (you can replace this with real file content if necessary)
            MemoryStream stream = new MemoryStream(fileContent);

            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.Length).Returns(fileContent.Length);
            fileMock.Setup(f => f.FileName).Returns("test_video.mp4");
            fileMock.Setup(f => f.ContentType).Returns("video/mp4");

            // Returning the VideoUpload with the mocked file
            return new VideoUpload(
                videoFile: fileMock.Object, // Injecting the mock file
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
            UserDetails userDetails = CreateUserDetails();

            return new UserEntity(
                userId: userId,
                userDetails: userDetails
            );
        }

        public static VideoCreatorEntity CreateVideoCreatorEntity()
        {
            Guid videoId = Guid.NewGuid();
            UserEntity userEntity = CreateUserEntity();

            return new VideoCreatorEntity(
                videoId: videoId,
                userId: userEntity.UserId,
                userDetails: userEntity.UserDetails
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

        public static UserDetails CreateUserDetails()
        {
            return new UserDetails(
                username: "Test user",
                profileUrlPath: "TestProfileUrlPath",
                profilePictureUrlPath: "TestProfilePicturePath"
            );
        }
    }
}
