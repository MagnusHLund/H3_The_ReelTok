using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class BlockedDetails
    {
        [ForeignKey("User")]
        public Guid UserId { get; } = Guid.Empty;
        public UserProfileData? User { get; } = null;

        [ForeignKey("BlockUser")]
        public Guid BlockUserId { get; } = Guid.Empty;
        public UserProfileData? BlockUser { get; } = null;
        
        public BlockedDetails(Guid userId, Guid blockUserId)
        {
            UserId = userId;
            BlockUserId = blockUserId;
        }
    }
}