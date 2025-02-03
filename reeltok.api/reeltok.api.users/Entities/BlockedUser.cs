using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class BlockedUser
    {
        public uint BlockId { get; } = 0;
        public BlockedUserDetail Details { get; }
        public BlockedUser(uint blockId, BlockedUserDetail details)
        {
            BlockId = blockId;
            Details = details;
        }

    }
}