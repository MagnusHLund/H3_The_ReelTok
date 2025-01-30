using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs
{
    public abstract class BaseResponseDto
    {
        public bool Success { get; set; }

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
    }
}