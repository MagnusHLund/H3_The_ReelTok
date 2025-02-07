using System.Xml.Serialization;

namespace reeltok.api.videos.Utils
{
    public static class XmlUtils
    {
        public static string SerializeDtoToXml<T>(T dto)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using StringWriter stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, dto);
            return stringWriter.ToString();
        }

        public static T DeserializeFromXml<T>(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                throw new ArgumentException("Input XML cannot be null or whitespace.", nameof(xml));
            }

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}
