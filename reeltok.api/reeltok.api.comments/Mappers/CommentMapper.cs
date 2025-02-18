using reeltok.api.comments.DTOs;
using reeltok.api.comments.Entities;
using reeltok.api.comments.ValueObjects;

namespace reeltok.api.comments.Mappers
{
    public static class CommentMapper
    {
        public static CommentDetails ToCommentFromCreateDTO(this CreateDTO dto)
        {
            return new CommentDetails
            (
                dto.UserId,
                dto.VideoId,
                dto.Message,
                dto.CreatedAt
            );
        }

        public static ReadDTO ToDTOFromCommentEntity(this Comment comment)
        {
            return new ReadDTO(
                comment.CommentDetails.UserId,
                comment.CommentDetails.Message,
                comment.CommentDetails.CreatedAt
            );
        }

        public static ReturnCreateDTO ToReturnCreateCommentResponseDTO(this Comment comment)
        {
            return new ReturnCreateDTO
            {
                CommentId = comment.CommentId,
                UserId = comment.CommentDetails.UserId,
                VideoId = comment.CommentDetails.VideoId,
                Message = comment.CommentDetails.Message,
                CreatedAt = comment.CommentDetails.CreatedAt
            };
        }
    }
}
