using Moq;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace reeltok.api.users.Tests.Factories
{
    public static class TestDataFactory
    {
        // Generates a new User ID
        public static Guid GenerateUserId()
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
    }
}
