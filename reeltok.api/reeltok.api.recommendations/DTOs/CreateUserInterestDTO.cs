namespace reeltok.api.recommendations.DTOs
{
    public class CreateUserInterestDTO
    {
        public Guid UserId { get; set; }
        public int CategoryId { get; set; }

        public CreateUserInterestDTO(Guid userId, int categoryId)
        {
            UserId = userId;
            CategoryId = categoryId;
        }
    }
}
