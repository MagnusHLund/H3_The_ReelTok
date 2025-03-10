using reeltok.api.comments.Utils;
using reeltok.api.comments.Entities;
using reeltok.api.comments.ValueObjects;

namespace reeltok.api.comments.Factories
{
    internal static class CommentFactory
    {
        internal static CommentEntity CreateCommentEntity(Guid videoId, Guid userId, string commentText)
        {
            uint createdAt = DateTimeUtils.DateTimeToUnixTime(DateTime.Now);

            CommentDetails commentDetails = new CommentDetails(
                videoId: videoId,
                userId: userId,
                message: commentText,
                createdAt: createdAt
            );

            return new CommentEntity(commentDetails);
        }
    }
}