using Moq;
using reeltok.api.users.Entities;
using reeltok.api.users.ValueObjects;
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

        public static UserWithSubscriptionCounts CreateMockUserWithSubscriptionCounts(Guid userId, string username, string email, int subscriptionCount, int otherCount)
        {
            UserDetails userDetails = new UserDetails(username, email, "http://example.com/profile.jpg");
            return new UserWithSubscriptionCounts(userId, userDetails, subscriptionCount, otherCount);
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
    }
}
