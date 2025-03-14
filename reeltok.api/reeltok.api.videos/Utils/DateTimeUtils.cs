namespace reeltok.api.videos.Utils
{
    internal static class DateTimeUtils
    {
        internal static long DateTimeToUnixTime(DateTime timeToConvert)
        {
            return new DateTimeOffset(timeToConvert).ToUnixTimeSeconds();
        }
    }
}
