namespace reeltok.api.recommendations.DTOs
{
    public class UpdateUserInterestDto
    {
        public Guid UserId { get; set; }
        public int OldCategoryId { get; set; }
        public int NewCategoryId { get; set; }

        public UpdateUserInterestDto(Guid userId, int oldCategoryId, int newCategoryId)
        {
            UserId = userId;
            OldCategoryId = oldCategoryId;
            NewCategoryId = newCategoryId;
        }
    }
}
