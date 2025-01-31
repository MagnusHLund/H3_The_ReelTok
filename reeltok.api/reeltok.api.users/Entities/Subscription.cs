

using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    internal class Subscription
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal UserDetails UserDetails { get; private set; }

        internal Subscription(Guid userId, UserDetails userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;
        }
    }
}