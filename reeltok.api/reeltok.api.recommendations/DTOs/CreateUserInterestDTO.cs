using reeltok.api.auth.DTOs;

namespace reeltok.api.recommendations.DTOs
{
    public class CreateUserInterestDto : BaseResponseDto
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }

        public CreateUserInterestDto(Guid userId, int categoryId, bool success = true) : base(success)
        {
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
