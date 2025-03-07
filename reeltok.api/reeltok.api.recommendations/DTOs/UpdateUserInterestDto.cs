using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class UpdateUserInterestDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public int OldCategoryId { get; set; }
        public int NewCategoryId { get; set; }

        public UpdateUserInterestDto(Guid userId, int oldCategoryId, int newCategoryId, bool success = true) : base(success)
        {
            UserId = userId;
            OldCategoryId = oldCategoryId;
            NewCategoryId = newCategoryId;
        }
    }
}
