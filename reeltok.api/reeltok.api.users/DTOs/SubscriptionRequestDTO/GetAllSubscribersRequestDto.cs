using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.SubscriptionRequestDTO
{
    internal class GetAllSubscribersRequestDto
    {
        [Required]
        internal Guid UserId { get; }

        internal GetAllSubscribersRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}