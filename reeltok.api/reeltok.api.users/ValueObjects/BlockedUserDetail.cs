using System.ComponentModel.DataAnnotations.Schema;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class BlockedUserDetail
    {
        [ForeignKey("User")]
        public Guid UserId { get; } = Guid.Empty;
        public UserProfileData? User { get; } = null;

        [ForeignKey("BlockUser")]
        public Guid BlockUserId { get; } = Guid.Empty;
        public UserProfileData? BlockUser { get; } = null;

        public BlockedUserDetail(Guid userId, Guid blockUserId)
        {
            UserId = userId;
            BlockUserId = blockUserId;
        }
    }
}