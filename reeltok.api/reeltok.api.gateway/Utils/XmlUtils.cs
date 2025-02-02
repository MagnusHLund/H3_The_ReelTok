using System.Xml.Serialization;

namespace reeltok.api.gateway.Utils
{
    internal static class XmlUtils
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
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xml))
            {
                return (T)xmlSerializer.Deserialize(stringReader);
            }
        }
    }
}