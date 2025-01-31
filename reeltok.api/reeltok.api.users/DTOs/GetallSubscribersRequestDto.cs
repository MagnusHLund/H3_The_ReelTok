using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs
{
    internal class GetallSubscribersRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal GetallSubscribersRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}