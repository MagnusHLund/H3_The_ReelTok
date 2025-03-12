using reeltok.api.gateway.Entities.comments;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.LoadComments
{
    public class GatewayLoadCommentsResponseDto : BaseResponseDto
    {
        [Required]
        public List<CommentUsingDateTime> Comments { get; set; }

        public GatewayLoadCommentsResponseDto(List<CommentUsingDateTime> comments, bool success = true) : base(success)
        {
            Comments = comments;
        }
    }
}
