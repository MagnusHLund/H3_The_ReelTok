using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs
{
    public abstract class BaseResponseDto
    {
        [XmlElement(elementName: "Success")]
        public bool Success { get; set; }

        protected BaseResponseDto() { }

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
    }
}