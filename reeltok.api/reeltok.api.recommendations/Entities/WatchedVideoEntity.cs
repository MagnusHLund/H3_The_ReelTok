using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideoEntity
    {
        [Key]
        public uint WatchedVideoId { get; private set; }

        public WatchedVideoDetails WatchedVideoDetails { get; private set; }

        public List<CategoryEntity> CategoryEntities { get;  private set;}

        public WatchedVideoEntity(WatchedVideoDetails watchedVideoDetails)
        {
            WatchedVideoDetails = watchedVideoDetails;
        }

        private WatchedVideoEntity()
        {

        }
    }
}
