using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.DTOs.UserRequestDTO
{
    public class GetUserFromIdRequestDto
    {
        public Guid UserId { get; }

        public GetUserFromIdRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}