using reeltok.api.comments.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.Entities
{
    public class CommentEntity
    {
        [Key]
        public uint CommentId { get; private set; }

        [Required]
        public CommentDetails CommentDetails { get; set; }

        public CommentEntity(CommentDetails commentDetails)
        {
            CommentDetails = commentDetails;
        }

        private CommentEntity()
        {
        }
    }
}
