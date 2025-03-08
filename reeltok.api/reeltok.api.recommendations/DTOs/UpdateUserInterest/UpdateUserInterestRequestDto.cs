using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateUserInterestRequestDto
    {
        public Guid UserId { get; set; }
        public CategoryType Interest { get; set; }

        public UpdateUserInterestRequestDto(Guid userId, CategoryType interest)
        {
            UserId = userId;
            Interest = interest;
        }
    }
}
