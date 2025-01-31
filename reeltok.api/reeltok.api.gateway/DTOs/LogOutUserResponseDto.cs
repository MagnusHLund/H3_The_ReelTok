using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs
{
    public class LogOutUserResponseDto : BaseResponseDto
    {
        public LogOutUserResponseDto(bool success) : base(success)
        {
        }
    }
}