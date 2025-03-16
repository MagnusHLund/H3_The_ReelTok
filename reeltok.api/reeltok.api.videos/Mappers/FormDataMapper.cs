namespace reeltok.api.videos.Mappers
{
    internal class FormDataMapper
    {
        internal static int ConvertStringToint(string stringNumber)
        {
            if (int.TryParse(stringNumber, out int number))
            {
                return number;
            }

            throw new ArgumentException("Provided string is not a valid number!");
        }

        internal static Guid ConvertStringToGuid(string stringGuid)
        {
            if (Guid.TryParse(stringGuid, out Guid guid))
            {
                return guid;
            }

            throw new ArgumentException("Provided string is not a valid GUID!");
        }
    }
}