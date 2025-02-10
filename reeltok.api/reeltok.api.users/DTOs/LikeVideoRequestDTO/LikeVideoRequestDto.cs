using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.LikeVideoRequestDTO
{
    public class LikeVideoRequestDto
    {
        [Required]
        public Guid UserId { get; }
        [Required]
        public Guid VideoId { get; }

        public LikeVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}