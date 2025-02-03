using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class LikedVideo
    {
        [Required]
        public uint LikedVideoId { get; } = 0;
        
        [Required]
        public LikedDetails Details { get; }
        public LikedVideo(uint likedVideoId, LikedDetails details)
        {
            LikedVideoId = likedVideoId;
            Details = details;
        }
    }
}