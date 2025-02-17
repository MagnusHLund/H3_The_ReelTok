namespace reeltok.api.gateway.DTOs.Videos.GetVideosForProfile
{
    internal class GatewayGetVideosForProfileRequestDto
    {
        internal Guid UserId { get; set; }
        internal byte Amount { get; set; }
        internal uint AmountReceived { get; set; }

        internal GatewayGetVideosForProfileRequestDto(Guid userId, byte amount, uint amountReceived)
        {
            UserId = userId;
            Amount = amount;
            AmountReceived = amountReceived;
        }
    }
}
