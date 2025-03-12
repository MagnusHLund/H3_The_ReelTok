using System.ComponentModel.DataAnnotations;
using reeltok.api.gateway.Entities.comments;

namespace reeltok.api.gateway.DTOs.Comments.LoadComments
{
    public class ServiceLoadCommentsResponseDto : BaseResponseDto
    {
        [Required]
        public List<CommentUsingUnixTime> Comments { get; set; }

        public ServiceLoadCommentsResponseDto(List<CommentUsingUnixTime> comments, bool success = true) : base(success)
        {
            Comments = comments;
        }
    }
}
