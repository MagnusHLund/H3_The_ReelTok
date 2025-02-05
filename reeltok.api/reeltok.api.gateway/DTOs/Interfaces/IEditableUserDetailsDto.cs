using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Interfaces
{
    public interface IEditableUserDetailsDto
    {
        string Username { get; set; }
        string Email { get; set; }
    }
}