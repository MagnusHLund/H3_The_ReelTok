using Moq;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using reeltok.api.users.Interfaces.Services;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Tests.Factories
{
    public static class TestDataFactory
    {
        // Generates a new User ID
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        // Creates a UserEntity with the specified details
        public static UserEntity CreateUserEntity(
            Guid userId,
            string username,
            string email,
            string profilePictureUrlPath = "ProfilePictureUrlPath"
        )
        {
            UserDetails userDetails = new UserDetails(username, "bio", profilePictureUrlPath);
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(email);
            return new UserEntity(userId, userDetails, hiddenUserDetails);
        }

        // Creates a list of UserEntities from a list of GUIDs
        public static List<UserEntity> CreateUserEntitiesList(List<Guid> userIds)
        {
            List<UserEntity> users = new List<UserEntity>();
            foreach (Guid userId in userIds)
            {
                users.Add(CreateUserEntity(userId, "testUser" + userId, "test@example.com"));
            }
            return users;
        }

        // Creates a UserWithSubscriptionCounts for a user with subscription details
        public static UserWithSubscriptionCounts CreateUserWithSubscriptionCounts(
            Guid userId,
            string username,
            int subscriptionCount,
            int otherCount
        )
        {
            UserDetails userDetails = new UserDetails(username, "bio", "ProfilePictureUrlPath");
            ExternalUserEntity externalUserEntity = new ExternalUserEntity(userId, userDetails);
            return new UserWithSubscriptionCounts(externalUserEntity, subscriptionCount, otherCount);
        }

        // Creates a LikedDetails for user-video interaction
        public static LikedDetails CreateLikedDetails(Guid userId, Guid videoId)
        {
            return new LikedDetails(userId, videoId);
        }

        public static UserEntity CreateUserEntity(Guid userId, string username, string email)
        {
            UserDetails userDetails = new UserDetails(username, "https://example.com/profile", "ProfilePictureUrlPath");
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(email);
            return new UserEntity(userId, userDetails, hiddenUserDetails);
        }

        public static UserWithInterestEntity CreateUserWithInterestEntity(Guid userId, byte interest)
        {
            UserDetails userDetails = new UserDetails("Test User", "https://example.com/profile", "ProfilePictureUrlPath");
            ExternalUserEntity externalUserEntity = new ExternalUserEntity(userId, userDetails);
            return new UserWithInterestEntity(externalUserEntity, interest);
        }

        public static Mock<IUsersService> CreateMockUsersService(UserEntity userEntity)
        {
            Mock<IUsersService> mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(userEntity);
            return mockUsersService;
        }

        public static Mock<IExternalApiService> CreateMockExternalApiService(Guid userId, byte userInterest, string password)
        {
            Mock<IExternalApiService> mockExternalApiService = new Mock<IExternalApiService>();
            mockExternalApiService.Setup(x => x.LoginUserInAuthApiAsync(userId, password)).Returns(Task.CompletedTask);
            mockExternalApiService.Setup(x => x.GetUserInterestFromRecommendationsApiAsync(userId)).ReturnsAsync(userInterest);
            return mockExternalApiService;
        }

        // Optionally, you can create mock failure cases as well.
        public static Mock<IExternalApiService> CreateMockExternalApiServiceWithFailure(Guid userId)
        {
            Mock<IExternalApiService> mockExternalApiService = new Mock<IExternalApiService>();
            mockExternalApiService.Setup(x => x.GetUserInterestFromRecommendationsApiAsync(userId))
                                   .ThrowsAsync(new Exception("Failed to retrieve interest"));
            return mockExternalApiService;
        }

        public static LikedVideoEntity CreateLikedVideoEntity(Guid userId, Guid videoId)
        {
            return new LikedVideoEntity(userId, videoId);
        }

        public static UserWithSubscriptionCounts CreateMockUserWithSubscriptionCounts(Guid userId, string username, string email, int subscriptionCount, int otherCount)
        {
            UserDetails userDetails = new UserDetails(username, email, "http://example.com/profile.jpg");
            HiddenUserDetails hiddenUserDetails = new HiddenUserDetails(email);
            ExternalUserEntity externalUserEntity = new ExternalUserEntity(userId, userDetails);
            return new UserWithSubscriptionCounts(externalUserEntity, subscriptionCount, otherCount);
        }

        public static List<HasUserLikedVideoEntity> CreateHasUserLikedVideoEntities(List<Guid> videoIds)
        {
            return videoIds.Select((videoId, index) => new HasUserLikedVideoEntity(videoId, index % 2 == 0)).ToList();
        }

        public static Mock<ILikesRepository> CreateMockLikesRepository()
        {
            var mockLikesRepository = new Mock<ILikesRepository>();
            return mockLikesRepository;
        }

        public static Mock<IUsersService> CreateMockUsersService(UserWithSubscriptionCounts mockUserWithSubscriptionCounts)
        {
            var mockUsersService = new Mock<IUsersService>();
            mockUsersService.Setup(x => x.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockUserWithSubscriptionCounts);
            return mockUsersService;
        }

    }
}
