using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.DTOs.SubscriptionResponses
{
    [XmlRoot("GetAllSubscribingToUserResponseDto")]
    public class ServiceGetAllSubscribingToUserResponseDto : BaseResponseDto
    {
        [XmlElement("Users")]
        [XmlArray("Users")]
        [XmlArrayItem("UserDetails")]
        public List<UserDetails> Users { get; set; }

        public ServiceGetAllSubscribingToUserResponseDto(List<UserDetails> users, bool success = true) : base(success)
        {
            Users = users;
        }

        public ServiceGetAllSubscribingToUserResponseDto(bool success = true) : base(success)
        {
        }

    }
}
