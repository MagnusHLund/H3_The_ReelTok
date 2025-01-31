using System.ComponentModel.DataAnnotations;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class Subscriber
    {
        [Required]
        internal Guid UserId { get; private set; }
        [Required]
        internal UserDetails UserDetails { get; private set; }

        public Subscriber(Guid userId, UserDetails userDetails)
        {
            UserId = userId;
            UserDetails = userDetails;

        }
    }
}