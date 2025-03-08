using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs.UpdateUserInterest
{
    public class UpdateUserInterestResponseDto : BaseResponseDto
    {
        public CategoryType Interest { get; set; }

        public UpdateUserInterestResponseDto(CategoryType interest, bool success) : base(success)
        {
            Interest = interest;
        }
    }
}