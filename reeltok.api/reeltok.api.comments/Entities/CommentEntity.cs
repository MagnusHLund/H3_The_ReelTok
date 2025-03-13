using Newtonsoft.Json;
using reeltok.api.comments.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.Entities
{
    public class CommentEntity
    {
        [Key]
        [JsonProperty("CommentId")]
        public uint CommentId { get; private set; }

        [Required]
        [JsonProperty("CommentDetails")]
        public CommentDetails CommentDetails { get; set; }

        public CommentEntity(CommentDetails commentDetails)
        {
            CommentDetails = commentDetails;
        }

        private CommentEntity() { }
    }
}
