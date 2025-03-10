namespace reeltok.api.comments.Utils
{
    internal static class DateTimeUtils
    {
        internal static uint DateTimeToUnixTime(DateTime timeToConvert)
        {
            return (uint)new DateTimeOffset(timeToConvert).ToUnixTimeSeconds();
        }
    }
}