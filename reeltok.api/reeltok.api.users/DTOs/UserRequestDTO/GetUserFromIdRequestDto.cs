using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    internal class GetUserFromIdRequestDto
    {
        internal Guid UserId { get;}

        internal GetUserFromIdRequestDto(Guid userId){
            UserId = userId;
        }
    }
}