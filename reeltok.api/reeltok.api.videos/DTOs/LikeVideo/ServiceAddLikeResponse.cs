using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeResponseDto")]
    public class ServiceAddLikeResponseDto : BaseResponseDto
    {
        public ServiceAddLikeResponseDto(bool success) : base(success) { }
        public ServiceAddLikeResponseDto() { }
    }
}
