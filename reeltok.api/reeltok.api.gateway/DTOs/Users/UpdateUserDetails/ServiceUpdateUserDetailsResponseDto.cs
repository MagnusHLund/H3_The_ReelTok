using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsResponseDto")]
    public class ServiceUpdateUserDetailsResponseDto : BaseResponseDto
    {
        public ServiceUpdateUserDetailsResponseDto(bool success) : base(success) { }
        public ServiceUpdateUserDetailsResponseDto() { }
    }
}