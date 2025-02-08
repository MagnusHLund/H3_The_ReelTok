using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.RemoveLike
{
    [XmlRoot("RemoveLikeResponseDto")]
    public class ServiceRemoveLikeResponseDto : BaseResponseDto
    {
        public ServiceRemoveLikeResponseDto(bool success = true) : base(success) { }
    }
}
