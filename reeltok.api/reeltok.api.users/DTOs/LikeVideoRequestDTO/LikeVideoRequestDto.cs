using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.LikeVideoRequestDTO
{
    internal class LikeVideoRequestDto
    {
        [Required]
        internal Guid UserId { get; }
        [Required]
        internal Guid VideoId { get; }

        internal LikeVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}