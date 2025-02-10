using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.auth.Entities
{
    public class RefreshToken
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }


        public RefreshToken(Guid userId, string token, DateTime createDate, DateTime expireDate)
        {
            UserId = userId;
            Token = token;
            CreateDate = createDate;
            ExpireDate = expireDate;
        }
    }
}
