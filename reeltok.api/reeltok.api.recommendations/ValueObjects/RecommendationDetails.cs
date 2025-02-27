using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.ValueObjects
{
    public class UserInterestDetails
    {
        [Required]
        public Guid UserId { get; private set; }

        public UserInterestDetails(Guid userId)
        {
            UserId = userId;
        }

        private UserInterestDetails()
        {

        }

    }
}
