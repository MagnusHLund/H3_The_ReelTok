using System.ComponentModel.DataAnnotations;
using reeltok.api.comments.ValueObjects;

namespace reeltok.api.comments.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; private set; }

        [Required]
        public CommentDetails CommentDetails { get; set; }

        public Comment(CommentDetails commentDetails)
        {
            CommentDetails = commentDetails;
        }

        private Comment()
        {
        }
    }
}
