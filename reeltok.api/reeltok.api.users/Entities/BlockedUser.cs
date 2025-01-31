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
        public BlockedDetails Details { get; }
        public BlockedUser(uint blockId, BlockedDetails details)
        {
            BlockId = blockId;
            Details = details;
        }

    }
}