using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.auth.DTOs
{
    public abstract class BaseResponseDto
    {
        [Required]
        [XmlElement("Success")]
        public virtual bool Success { get; set; }

        private protected BaseResponseDto(bool success)
        {
            Success = success;
        }

        private protected BaseResponseDto() { }
    }
}
