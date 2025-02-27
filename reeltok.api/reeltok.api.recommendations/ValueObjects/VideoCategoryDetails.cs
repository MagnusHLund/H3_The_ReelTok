using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.ValueObjects
{
    public class VideoCategoryDetails
    {
        public Guid VideoId { get; private set; }

        public VideoCategoryDetails(Guid videoId)
        {
            VideoId = videoId;
        }

        private VideoCategoryDetails() { }
    }
}
