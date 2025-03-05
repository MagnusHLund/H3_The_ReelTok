using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class UserInterestEntity
    {
        [Key]
        public uint UserInterestId { get; set; }

        [Required]
        public UserInterestDetails UserInterestDetails { get; set; }

        public List<CategoryEntity>? Categories { get; set; }

        public UserInterestEntity(UserInterestDetails userInterestDetails)
        {
            UserInterestDetails = userInterestDetails;
        }

        private UserInterestEntity() { }
    }
}
