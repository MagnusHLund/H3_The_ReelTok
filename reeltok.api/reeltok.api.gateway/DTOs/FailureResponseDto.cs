using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs
{
    [XmlRoot("FailureResponseDto")]
    public class FailureResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Message")]
        [Required]
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
