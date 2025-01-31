using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.DTOs
{
    internal class GetUserResponseDto : BaseResponseDto
    {
        internal string Username { get; }
        internal UserDetails UserDetails { get; }
        internal GetUserResponseDto(bool success, String userName, UserDetails userDetails) : base(success)
        {
            Username = userName;
            UserDetails = userDetails;
        }
    }
}