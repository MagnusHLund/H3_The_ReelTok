using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities;

namespace reeltok.api.users.DTOs
{
    internal class GetAllSubscriberResponseDto : BaseResponseDto
    {

        [Required]
        internal List<Subscription> AllSubscribers { get;}


        
        public GetAllSubscriberResponseDto(bool success, List<Subscription> allSubscribers) : base(success)
        {
            AllSubscribers = allSubscribers;
        }
    }
}