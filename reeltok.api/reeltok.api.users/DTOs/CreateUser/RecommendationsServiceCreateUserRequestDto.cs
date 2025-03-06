namespace reeltok.api.users.DTOs.CreateUser
{
    public class RecommendationsServiceCreateUserRequestDto
    {
        public Guid UserId { get; set; }
        public byte Interests { get; set; }

        public RecommendationsServiceCreateUserRequestDto(Guid userId, byte interests)
        {
            UserId = userId;
            Interests = interests;
        }

        public RecommendationsServiceCreateUserRequestDto() { }
    }
}