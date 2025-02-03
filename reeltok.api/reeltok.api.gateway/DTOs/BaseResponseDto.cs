using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs
{
    public abstract class BaseResponseDto
    {
        [XmlElement(elementName: "Success")]
        public virtual bool Success { get; set; }

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
        protected BaseResponseDto() { }
    }
}