using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class BlockedUser
    {
        [Required]
        public uint BlockId { get; } = 0;

        [Required]
        public BlockedUserDetail Details { get; }
        public BlockedUser(uint blockId, BlockedUserDetail details)
        {
            BlockId = blockId;
            Details = details;
        }

    }
}