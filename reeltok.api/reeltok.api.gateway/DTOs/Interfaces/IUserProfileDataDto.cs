using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.DTOs.Interfaces
{
    public interface IUserProfileDataDto
    {
        Guid UserId { get; set; }
        string Email { get; set; }
        string Username { get; set; }
        string ProfileUrl { get; set; }
        string ProfilePictureUrl { get; set; }
    }
}