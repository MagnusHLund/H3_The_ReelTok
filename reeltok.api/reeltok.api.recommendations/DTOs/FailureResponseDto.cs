using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using reeltok.api.recommendations.DTOs;

namespace reeltok.api.auth.DTOs
{
    [XmlRoot("FailureResponseDto")]
    public class FailureResponseDto : BaseResponseDto
    {
        [Required]
        [XmlElement("Message")]
        public string Message { get; set; }
        [XmlElement("Success")]
        public override bool Success { get; set; }

        public FailureResponseDto(string message)
        {
            Message = message;
            Success = false;
        }

        public FailureResponseDto() { }
    }
}
