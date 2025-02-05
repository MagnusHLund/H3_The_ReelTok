namespace reeltok.api.gateway.Interfaces
{
    public interface IRecommendationsService
    {
        public Task<bool> ChangeRecommendedCategory(string category);
    }
}