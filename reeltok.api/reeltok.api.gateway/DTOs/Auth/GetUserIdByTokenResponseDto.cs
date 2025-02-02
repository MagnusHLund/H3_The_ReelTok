using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Auth
{
    public class GetUserIdByTokenResponseDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public GetUserIdByTokenResponseDto(bool success, Guid userId) : base(success)
        {
            UserId = userId;
        }
    }
}