using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("FailureResponseDto")]
    internal class FailureResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Message")]
        public string Message { get; set; }
        [XmlElement(elementName: "Success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }
        public FailureResponseDto() { }
    }
}