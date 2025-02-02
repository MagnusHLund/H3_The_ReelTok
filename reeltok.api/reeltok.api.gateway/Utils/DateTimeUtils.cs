namespace reeltok.api.gateway.Utils
{
    internal static class DateTimeUtils
    {
        internal static uint DateTimeToUnixTime(DateTime timeToConvert)
        {
            return (uint)new DateTimeOffset(timeToConvert).ToUnixTimeSeconds();
        }

        internal static DateTime UnixTimeToDateTime(uint timeToConvert)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timeToConvert).UtcDateTime;
        }
    }
}