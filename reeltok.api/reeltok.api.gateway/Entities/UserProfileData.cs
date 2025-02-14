using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities
{
    public class UserProfileData
    {
        public Guid UserId { get; set; }
        public UserDetails UserDetails { get; set; }
        public HiddenUserDetails HiddenUserDetails { get; set; }

        public UserProfileData(Guid userId, UserDetails userDetails, HiddenUserDetails hiddenUserDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
            HiddenUserDetails = hiddenUserDetails;
        }
    }
}
