using Newtonsoft.Json;
using reeltok.api.comments.Entities;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.comments.DTOs.GetCommentsByVideoId
{
    public class GetCommentsByVideoIdResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("TotalComments")]
        public uint TotalVideoComments { get; set; }

        [Required]
        [JsonProperty("Comments")]
        public List<CommentEntity> Comments { get; set; }

        public GetCommentsByVideoIdResponseDto(uint totalVideoComments, List<CommentEntity> comments, bool success = true)
            : base(success)
        {
            TotalVideoComments = totalVideoComments;
            Comments = comments;
        }
    }
}