
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

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
