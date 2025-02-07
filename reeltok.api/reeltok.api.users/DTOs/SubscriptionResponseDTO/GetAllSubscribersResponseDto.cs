using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs.SubscriptionResponseDTO
{
    internal class GetAllSubscribersResponseDto : BaseResponseDto
    {

        [Required]
        internal List<Subscription> AllSubscribers { get;}


        
        public GetAllSubscribersResponseDto(bool success, List<Subscription> allSubscribers) : base(success)
        {
            AllSubscribers = allSubscribers;
        }
    }
}