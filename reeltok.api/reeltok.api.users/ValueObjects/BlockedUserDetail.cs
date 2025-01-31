using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.ValueObjects
{
    internal class BlockedUserDetail
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal UserDetails UserDetails { get; private set; }

        internal BlockedUserDetail(Guid userId, UserDetails userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
        }
    }
}