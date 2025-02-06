using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("BaseResponseDto")]
    internal abstract class BaseResponseDto
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