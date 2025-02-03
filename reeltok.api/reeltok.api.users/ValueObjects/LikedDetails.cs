using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.ValueObjects
{
    public class LikedDetails
    {

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; } = Guid.Empty;
        public UserProfileData? User { get; } = null;

        [Required]
        public Guid VideoId { get; } = Guid.Empty;
        public LikedDetails(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}