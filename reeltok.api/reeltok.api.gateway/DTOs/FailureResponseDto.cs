using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs
{
    public class FailureResponseDto : BaseResponseDto
    {
        public string Message { get; set; }
        public FailureResponseDto()
        {
        }
        public FailureResponseDto(bool success, string message) : base(success)
        {
            Message = message;
        }
    }
}