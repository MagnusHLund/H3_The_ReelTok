using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.Entities.ValueObjects;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class UserProfileData
    {
        
        public Guid UserId { get; } = Guid.Empty;
        public UserDetails Details { get; }
        public UserProfileData(Guid userId, UserDetails details)
        {
            UserId = userId;
            Details = details;
        }
    }
}