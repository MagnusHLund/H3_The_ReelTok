namespace reeltok.api.gateway.Utils
{
    internal static class DateTimeUtils
    {
        internal static long DateTimeToUnixTime(DateTime timeToConvert)
        {
            return new DateTimeOffset(timeToConvert).ToUnixTimeSeconds();
        }

        internal static DateTime UnixTimeToDateTime(long timeToConvert)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timeToConvert).UtcDateTime;
        }
    }
}