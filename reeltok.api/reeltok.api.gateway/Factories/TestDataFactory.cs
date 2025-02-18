using System.Net;
using System.Text;
using reeltok.api.gateway.DTOs;
using reeltok.api.gateway.DTOs.Auth;
using reeltok.api.gateway.DTOs.Comments;
using reeltok.api.gateway.DTOs.Recommendations;
using reeltok.api.gateway.DTOs.Users;
using reeltok.api.gateway.DTOs.Videos.DeleteVideo;
using reeltok.api.gateway.DTOs.Videos.GetVideosForFeed;
using reeltok.api.gateway.DTOs.Videos.LikeVideo;
using reeltok.api.gateway.DTOs.Videos.RemoveLike;
using reeltok.api.gateway.DTOs.Videos.UploadVideo;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Enums;
using reeltok.api.gateway.Utils;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Factories
{
    public static class TestDataFactory
    {
        public static ServiceLogOutUserRequestDto CreateLogOutUserRequest()
        {
            return new ServiceLogOutUserRequestDto();
        }

        public static ServiceLogOutUserResponseDto CreateLogOutUserResponse()
        {
            return new ServiceLogOutUserResponseDto
            (
                success: true
            );
        }

        public static FailureResponseDto CreateFailureResponse(string errorMessage)
        {
            return new FailureResponseDto
            (
                message: errorMessage
            );
        }

        public static ServiceGetUserIdByTokenRequestDto CreateGetUserIdByTokenRequest()
        {
            return new ServiceGetUserIdByTokenRequestDto();
        }

        public static ServiceGetUserIdByTokenResponseDto CreateGetUserIdByTokenResponse()
        {
            Guid userId = Guid.NewGuid();

            return new ServiceGetUserIdByTokenResponseDto
            (
                userId: userId,
                success: true
            );
        }

        public static ServiceAddCommentRequestDto CreateAddCommentRequest(Guid userId, Guid videoId, string commentText)
        {
            return new ServiceAddCommentRequestDto
            (
                userId: userId,
                videoId: videoId,
                commentText: commentText
            );
        }

        public static ServiceAddCommentResponseDto CreateAddCommentResponse()
        {
            Guid videoId = Guid.NewGuid();
            Guid commentId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            string commentText = "Amazing test!";
            uint createdAt = (uint) new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();

            return new ServiceAddCommentResponseDto
            (
                commentId: commentId,
                userId: userId,
                videoId: videoId,
                commentText: commentText,
                createdAt: createdAt,
                success: true
            );
        }

        public static ServiceLoadCommentsRequestDto CreateLoadCommentsRequest(Guid videoId, byte amount)
        {
            return new ServiceLoadCommentsRequestDto
            (
                videoId: videoId,
                amount: amount
            );
        }

        public static ServiceLoadCommentsResponseDto CreateLoadCommentsResponse(Guid videoId)
        {
            uint createdAt = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

            List<CommentUsingUnixTime> comments = new List<CommentUsingUnixTime>
            {
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), videoId, "Cool Test!", createdAt)),
                new CommentUsingUnixTime(Guid.NewGuid(), new CommentDetailsUsingUnixTime(Guid.NewGuid(), videoId, "I Agree with the previous comment", createdAt))
            };

            return new ServiceLoadCommentsResponseDto
            (
                comments: comments,
                success: true
            );
        }

        public static CommentUsingUnixTime CreateCommentUsingUnixTime(Guid commentId, Guid userId, Guid videoId, string commentText, uint createdAt)
        {
            return new CommentUsingUnixTime
            (
                commentId: commentId,
                commentDetails: new CommentDetailsUsingUnixTime(
                    userId: userId,
                    videoId: videoId,
                    commentText: commentText,
                    createdAt: createdAt
                )
            );
        }

        public static ServiceChangeRecommendedCategoryResponseDto CreateServiceChangeRecommendedCategoryResponseDto()
        {
            return new ServiceChangeRecommendedCategoryResponseDto
            (
                success: true
            );
        }

        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string responseContent)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(responseContent, Encoding.UTF8, "application/xml")
            };
        }

        public static Recommendations CreateVideoRecommendations()
        {
            Guid userId = Guid.NewGuid();
            List<RecommendedCategories> testRecommendations = new List<RecommendedCategories> { RecommendedCategories.Gaming };
            return new Recommendations(userId, testRecommendations);
        }

        public static ServiceLoginResponseDto CreateServiceLoginResponseDto()
        {
            Guid userId = Guid.NewGuid();
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            return new ServiceLoginResponseDto
            (
                userId: userId,
                email: email,
                username: username,
                profileUrl: profileUrl,
                profilePictureUrl: profilePictureUrl
            );
        }

        public static ServiceCreateUserResponseDto CreateServiceCreateUserResponseDto()
        {
            Guid userId = Guid.NewGuid();
            string username = "xX_TestName_Xx";
            string email = "test@reeltok.com";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            return new ServiceCreateUserResponseDto
            (
                userId: userId,
                username: username,
                email: email,
                profileUrl: profileUrl,
                profilePictureUrl: profilePictureUrl
            );
        }

        public static ServiceGetUserProfileDataResponseDto CreateGetUserProfileDataResponse()
        {
            Guid userId = Guid.NewGuid();
            string username = "xX_TestName_Xx";
            string profileUrl = "testUrl.com";
            string profilePictureUrl = "testurl.com";

            return new ServiceGetUserProfileDataResponseDto
            (
                userId: userId,
                username: username,
                profileUrl: profileUrl,
                profilePictureUrl: profilePictureUrl
            );
        }

        public static ServiceUpdateUserDetailsResponseDto CreateUpdateUserDetailsResponse()
        {
            string email = "test@reeltok.com";
            string username = "xX_TestName_Xx";

            return new ServiceUpdateUserDetailsResponseDto
            (
                username: username,
                email: email
            );
        }

        public static ServiceUpdateProfilePictureResponseDto CreateUpdateProfilePictureResponse()
        {
            string profilePictureUrl = "pictureUrl";

            return new ServiceUpdateProfilePictureResponseDto
            (
                profilePictureUrl: profilePictureUrl
            );
        }

        public static List<UserDetails> CreateUserDetailsList()
        {
            return new List<UserDetails>()
            {
                new UserDetails( "username1", "profilePictureUrl1", "profileUrl1"),
                new UserDetails( "username2", "profilePictureUrl2", "profileUrl2")
            };
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

        public static List<Video> CreateVideoList()
        {
            return new List<Video>
            {
                new Video(Guid.NewGuid(), new VideoDetails("Title1", "Description1", RecommendedCategories.Tech), 0, false, "url1", DateTime.UtcNow, new UserDetails("username1", "profilePictureUrl1", "profileUrl1")),
                new Video(Guid.NewGuid(), new VideoDetails("Title2", "Description2", RecommendedCategories.Gaming), 0, false, "url2", DateTime.UtcNow, new UserDetails("username2", "profilePictureUrl2", "profileUrl2")),
                new Video(Guid.NewGuid(), new VideoDetails("Title3", "Description3", RecommendedCategories.Sport), 0, false, "url3", DateTime.UtcNow, new UserDetails("username3", "profilePictureUrl3", "profileUrl3"))
            };
        }

        public static ServiceGetVideosForFeedResponseDto CreateGetVideosForFeedResponse(List<Video> videos)
        {
            return new ServiceGetVideosForFeedResponseDto
            (
                videos: videos
            );
        }

        public static ServiceGetAllSubscriptionsForUserResponseDto CreateGetAllSubscriptionsForUserResponse(List<UserDetails> userDetailsList)
        {
            return new ServiceGetAllSubscriptionsForUserResponseDto(userDetailsList);
        }

        public static ServiceGetAllSubscribingToUserResponseDto CreateGetAllSubscribingToUserResponse(List<UserDetails> userDetailsList)
        {
            return new ServiceGetAllSubscribingToUserResponseDto(userDetailsList);
        }

        public static VideoUpload CreateVideoUpload(IFormFile? videoFile)
        {
            VideoDetails videoDetails = new VideoDetails("test video", "description", RecommendedCategories.Comedy);
            return new VideoUpload
            (
                videoDetails: videoDetails,
                videoFile: videoFile
            );
        }

        public static Video CreateVideo()
        {
            return new Video(Guid.NewGuid(), new VideoDetails("Title1", "Description1", RecommendedCategories.Tech), 0, false, "url1", DateTime.UtcNow, new UserDetails("username1", "profilePictureUrl1", "profileUrl1"));
        }

        public static ServiceUploadVideoResponseDto CreateUploadVideoResponse(Video video)
        {
            return new ServiceUploadVideoResponseDto(video);
        }

        public static ServiceDeleteVideoResponseDto CreateDeleteVideoResponse()
        {
            return new ServiceDeleteVideoResponseDto();
        }

        public static Uri CreateUsersMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5001/api/users/{path}");
        }

        public static Uri CreateVideosMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5002/api/videos/{path}");
        }

        public static Uri CreateAuthMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5003/api/auth/{path}");
        }

        public static Uri CreateRecommendationsMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5004/api/recommendations/{path}");
        }

        public static Uri CreateCommentsMicroserviceTestUri(string path)
        {
            return new Uri($"http://localhost:5005/api/comments/{path}");
        }
    }
}
