namespace reeltok.api.recommendations.DTOs.GetUserInterest
{
    public class GetUserInterestResponseDto : BaseResponseDto
    {
        public byte Interest { get; set; }

        public GetUserInterestResponseDto(byte interest, bool success = true) : base(success)
        {
            Interest = interest;
        }
    }
}