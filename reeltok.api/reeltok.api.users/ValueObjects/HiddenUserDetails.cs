using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.users.Entities.ValueObjects
{
    public class HiddenUserDetails
    {
        [Required]
        [Column("nvarchar(100)")]
        public string Email { get; } = string.Empty;

        public HiddenUserDetails(string email)
        {
            Email = email;
        }
    }
}