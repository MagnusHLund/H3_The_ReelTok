using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs
{
    internal class GetAllSubscriptionsRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal GetAllSubscriptionsRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}