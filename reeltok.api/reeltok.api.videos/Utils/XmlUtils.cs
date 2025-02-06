using System.Xml.Serialization;

namespace reeltok.api.videos.Utils
{
    internal class XmlUtils
    {
        internal static string SerializeDtoToXml<T>(T dto)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, dto);
                return stringWriter.ToString();
            }
        }

        internal static T DeserializeFromXml<T>(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentException("Input XML cannot be null or whitespace.", nameof(xml));
            }

            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}