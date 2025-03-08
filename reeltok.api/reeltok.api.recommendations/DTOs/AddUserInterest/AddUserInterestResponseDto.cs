using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs.AddUserInterest
{
    public class AddUserInterestResponseDto : BaseResponseDto
    {
        public CategoryType Interest { get; set; }

        public AddUserInterestResponseDto(CategoryType interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}