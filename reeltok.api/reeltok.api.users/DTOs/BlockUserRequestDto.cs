using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs
{
    internal class BlockUserRequestDto
    {
        [Required]
        internal Guid UserId { get; }
        [Required]
        internal Guid BlockUserId { get; }

        internal BlockUserRequestDto(Guid userId, Guid blockUserId)
        {
            UserId = userId;
            BlockUserId = blockUserId;
        }
    }
}