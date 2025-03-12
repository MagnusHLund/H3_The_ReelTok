using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.Entities
{
    public class UserWithInterestEntity : ExternalUserEntity
    {
        public byte Interest { get; set; }
        public UserWithInterestEntity(ExternalUserEntity user, byte interest)
            : base(user.UserId, user.UserDetails)
        {
            Interest = interest;
        }
    }
}
