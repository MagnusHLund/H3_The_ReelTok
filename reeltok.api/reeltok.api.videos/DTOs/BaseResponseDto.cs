using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("BaseResponseDto")]
    public abstract class BaseResponseDto
    {
        [XmlElement(elementName: "Success")]
        public virtual bool Success { get; set; } = true;

        protected BaseResponseDto(bool success)
        {
            Success = success;
        }
        protected BaseResponseDto() { }
    }
}
