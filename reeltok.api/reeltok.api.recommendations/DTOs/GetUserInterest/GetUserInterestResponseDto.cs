using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.DTOs.GetUserInterest
{
    public class GetUserInterestResponseDto : BaseResponseDto
    {
        public CategoryType Interest { get; set; }

        public GetUserInterestResponseDto(CategoryType interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}