using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class VideoCategoryEntity
    {
        [Key]
        public int VideoCategoryId { get; set; }

        public VideoCategoryDetails VideoCategoryDetails { get; set; }

        public List<CategoryEntity> Categories { get; set; }

        public VideoCategoryEntity(VideoCategoryDetails videoCategoryDetails)
        {
            VideoCategoryDetails = videoCategoryDetails;
        }

        private VideoCategoryEntity() { }
    }
}
