using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class VideoCategoryEntity
    {
        [Key]
        public int VideoCategoryId { get; private set; }

        public VideoCategoryDetails VideoCategoryDetails { get; private set; }

        public List<CategoryEntity> Categories { get; private set; }

        public VideoCategoryEntity(VideoCategoryDetails videoCategoryDetails)
        {
            VideoCategoryDetails = videoCategoryDetails;
        }

        private VideoCategoryEntity() { }
    }
}
