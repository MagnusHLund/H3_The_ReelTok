using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.LikeVideoRequests
{
    public class DislikeVideoRequestDto
    {
        [Required]
        public Guid UserId { get; }

        [Required]
        public Guid VideoId { get; }

        public DislikeVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}