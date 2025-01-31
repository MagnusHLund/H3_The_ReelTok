using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class LikedDetails
    {

        [ForeignKey("User")]
        public Guid UserId { get; } = Guid.Empty;
        public UserProfileData? User { get; } = null;

        [ForeignKey("LikedUser")]
        public Guid LikedUserId { get; } = Guid.Empty;
        public UserProfileData? LikedUser { get; } = null;
        
        public LikedDetails(Guid userId, Guid likedUserId)
        {
            UserId = userId;
            LikedUserId = likedUserId;
        }
    }
}