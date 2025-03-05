namespace reeltok.api.users.DTOs.SubscriptionRequests
{
    public class UnsubscribeResponseDto : BaseResponseDto
    {
        public UnsubscribeResponseDto(bool success = true) : base(success) { }
        public UnsubscribeResponseDto() { }
    }
}