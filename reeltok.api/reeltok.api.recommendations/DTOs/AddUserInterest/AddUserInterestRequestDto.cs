using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs.AddUserInterest
{
    public class AddUserInterestRequestDto
    {
        public Guid UserId { get; set; }
        public CategoryType Interest { get; set; }

        public AddUserInterestRequestDto(Guid userId, CategoryType interest)
        {
            UserId = userId;
            Interest = interest;
        }
    }
}