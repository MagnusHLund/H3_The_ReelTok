using reeltok.api.comments.Entities;
using reeltok.api.comments.ValueObjects;

namespace reeltok.api.comments.Tests.Factories
{
    public static class TestDataFactory
    {
        public static Guid CreateGuid()
        {
            return Guid.NewGuid();
        }

        public static Uri CreateUri()
        {
            // Creating a sample URI, you can change this to fit your API endpoint
            return new Uri("https://api.reeltok.com/videos");
        }


        public static CommentEntity CreateCommentEntity()
        {
            Guid userId = CreateGuid();
            Guid videoId = CreateGuid();
            string message = "This is a test comment.";
            long createdAt =  DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var commentDetails = new CommentDetails(userId, videoId, message, createdAt);
            return new CommentEntity(commentDetails);
        }

        public static List<CommentEntity> CreateCommentEntities(int count)
        {
            List<CommentEntity> comments = new List<CommentEntity>();
            for (int i = 0; i < count; i++)
            {
                comments.Add(CreateCommentEntity());
            }
            return comments;
        }

        public static int CreateTotalCommentsCount()
        {
            return new Random().Next(1, 100);
        }

        public static (int pageNumber, byte pageSize) CreatePaginationData()
        {
            return (new Random().Next(1, 10), (byte) new Random().Next(1, 50));
        }

        public static List<Guid> CreateVideoIds(int count)
        {
            List<Guid> videoIds = new List<Guid>();
            for (int i = 0; i < count; i++)
            {
                videoIds.Add(CreateGuid());
            }
            return videoIds;
        }

        public static string CreateMessage()
        {
            return "This is a test comment message!";
        }

        public static (Guid videoId, Guid userId, string commentText) CreateCommentCreationData()
        {
            return (CreateGuid(), CreateGuid(), CreateMessage());
        }
    }
}
