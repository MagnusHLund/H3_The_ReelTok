using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.SubscriptionRequests
{
    public class GetAllSubscribersRequestDto
    {
        [Required]
        public Guid UserId { get; }

        public GetAllSubscribersRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}