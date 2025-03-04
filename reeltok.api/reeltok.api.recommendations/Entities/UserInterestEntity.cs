using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class UserInterestEntity
    {
        [Key]
        public uint UserInterestId { get; private set; }

        [Required]
        public UserInterestDetails UserInterestDetails { get; private set; }

        public List<CategoryEntity> Categories { get; private set; }

        public UserInterestEntity(UserInterestDetails userInterestDetails)
        {
            UserInterestDetails = userInterestDetails;
        }

        private UserInterestEntity() { }
    }
}
